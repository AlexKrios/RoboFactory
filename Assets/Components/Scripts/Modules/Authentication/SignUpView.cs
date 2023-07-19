using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Firebase.Auth;
using RoboFactory.General.Localisation;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Authentication
{
    [AddComponentMenu("Scripts/Authentication/Sign Up View")]
    public class SignUpView : MonoBehaviour
    {
        private const string HeaderTitleKey = "auth_sign_up_title";
        private const string EmailKey = "auth_email";
        private const string PasswordKey = "auth_password";
        private const string ConfirmKey = "auth_confirm";
        
        #region Zenject
        
        [Inject] private readonly LocalizationService localizationController;
        [Inject] private readonly AuthenticationService authenticationService;

        [Inject(Id = "SignIn")] private readonly SignInView signInForm;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text headerText;
        
        [Space]
        [SerializeField] private TMP_InputField emailField;
        [SerializeField] private TMP_Text emailPlaceholder;
        [SerializeField] private TMP_InputField passwordField;
        [SerializeField] private TMP_Text passwordPlaceholder;
        [SerializeField] private TMP_InputField confirmField;
        [SerializeField] private TMP_Text confirmPlaceholder;
        
        [Space]
        [SerializeField] private Button singInButton;
        [SerializeField] private Button googlePlayButton;
        [SerializeField] private Button logInButton;
        
        [Header("Error")]
        [SerializeField] private GameObject errorWrapper;
        [SerializeField] private TMP_Text errorText;

        #endregion
        
        #region Variables

        private string _authCode;
        private string _email;
        private string _password;
        private string _confirm;

        private HashSet<AuthError> errors;
        
        private CompositeDisposable _disposable;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _disposable = new CompositeDisposable();
            errors = new HashSet<AuthError>();

            singInButton.interactable = false;
            errorWrapper.SetActive(false);
            
            singInButton.onClick.AddListener(OnSignInClick);
            googlePlayButton.onClick.AddListener(OnGooglePlayClick);
            logInButton.onClick.AddListener(OnBackClick);
            emailField.onValueChanged.AddListener(ReadEmailField);
            passwordField.onValueChanged.AddListener(ReadFirstPasswordField);
            confirmField.onValueChanged.AddListener(ReadSecondPasswordField);
            
            authenticationService.EventSignUpError += AddError;
            authenticationService.EventGooglePlayError += AddError;
        }

        private void Start()
        { 
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            
            headerText.text = localizationController.GetLanguageValue(HeaderTitleKey);
            emailPlaceholder.text = localizationController.GetLanguageValue(EmailKey);
            passwordPlaceholder.text = localizationController.GetLanguageValue(PasswordKey);
            confirmPlaceholder.text = localizationController.GetLanguageValue(ConfirmKey);
        }

        private void OnDestroy()
        {
            authenticationService.EventSignUpError -= AddError;
            authenticationService.EventGooglePlayError -= AddError;
            
            _disposable.Dispose();
        }

        #endregion
        
        private void OnSignInClick()
        {
            authenticationService.SignUp(_email, _password);
        }
        
        private void OnGooglePlayClick()
        {
            authenticationService.GooglePlaySignManually();
        }

        private void ReadEmailField(string text)
        {
            _email = text;
            var isMatch = Regex.IsMatch(text, AuthenticationService.MatchEmailPattern);
            if (!string.IsNullOrEmpty(text) && !isMatch)
                AddError(AuthError.InvalidEmail);
            else
                RemoveError(AuthError.InvalidEmail);
        }

        private void ReadFirstPasswordField(string text)
        {
            _password = text;
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(_confirm) && text != _confirm)
                AddError(AuthError.MissingPassword);
            else
                RemoveError(AuthError.MissingPassword);
            
            if (text?.Length < AuthenticationService.MinPasswordLength)
                AddError(AuthError.WeakPassword);
            else
                RemoveError(AuthError.WeakPassword);
        }

        private void ReadSecondPasswordField(string text)
        {
            _confirm = text;
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(_password) && text != _password)
                AddError(AuthError.MissingPassword);
            else
                RemoveError(AuthError.MissingPassword);
            
            if (text?.Length < AuthenticationService.MinPasswordLength)
                AddError(AuthError.WeakPassword);
            else
                RemoveError(AuthError.WeakPassword);
        }

        private void OnBackClick()
        {
            gameObject.SetActive(false);
            signInForm.gameObject.SetActive(true);
        }

        private void ShowError()
        {
            SetCreateState();
            if (errors.Count == 0)
            {
                errorWrapper.SetActive(false);
                return;
            }
            
            errorWrapper.SetActive(true);
            var localKey = authenticationService.GetErrorLocalizationKey(errors.First());
            errorText.text = localizationController.GetLanguageValue(localKey);
        }

        private void AddError(AuthError error, bool isError = false)
        {
            errors.Add(error);
            ShowError();

            if (isError)
                errors.Remove(error);
        }

        private void RemoveError(AuthError error)
        {
            errors.Remove(error);
            ShowError();
        }

        private void SetCreateState()
        {
            var condition = errors.Count == 0
                            && !string.IsNullOrEmpty(_email)
                            && !string.IsNullOrEmpty(_password)
                            && !string.IsNullOrEmpty(_confirm);
            
            singInButton.interactable = condition;
        }
    }
}
