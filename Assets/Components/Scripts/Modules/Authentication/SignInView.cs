using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Components.Scripts.Modules.General.Localisation;
using Firebase.Auth;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.Authentication
{
    public class SignInView : MonoBehaviour
    {
        private const string HeaderTitleKey = "auth_sign_in_title";
        private const string EmailKey = "auth_email";
        private const string PasswordKey = "auth_password";
        
        #region Zenject
        
        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly AuthenticationManager _authenticationManager;

        [Inject(Id = "SignUp")] private readonly SignUpView signUpForm;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text headerText;
        
        [Space]
        [SerializeField] private TMP_InputField emailField;
        [SerializeField] private TMP_Text emailPlaceholder;
        [SerializeField] private TMP_InputField passwordField;
        [SerializeField] private TMP_Text passwordPlaceholder;
        
        [Space]
        [SerializeField] private Button signInButton; 
        [SerializeField] private Button signUpButton;

        [Header("Error")]
        [SerializeField] private GameObject errorWrapper;
        [SerializeField] private TMP_Text errorText;

        #endregion
        
        #region Variables

        private string _authCode;
        private string _email;
        private string _password;

        private bool _isError;

        private HashSet<AuthError> errors;
        
        private CompositeDisposable _disposable;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _disposable = new CompositeDisposable();
            errors = new HashSet<AuthError>();

            signInButton.interactable = false;
            errorWrapper.SetActive(false);
            
            signInButton.onClick.AddListener(OnLogInClick);
            signUpButton.onClick.AddListener(OnSignInClick);
            emailField.onValueChanged.AddListener(ReadEmailField);
            passwordField.onValueChanged.AddListener(ReadPasswordField);

            _authenticationManager.EventSignInError += AddError;
        }

        private void Start()
        { 
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            
            headerText.text = _localisationController.GetLanguageValue(HeaderTitleKey);
            emailPlaceholder.text = _localisationController.GetLanguageValue(EmailKey);
            passwordPlaceholder.text = _localisationController.GetLanguageValue(PasswordKey);
        }

        private void OnDestroy()
        {
            _authenticationManager.EventSignInError -= AddError;
            
            _disposable.Dispose();
        }

        #endregion
        
        private void OnLogInClick()
        {
            _authenticationManager.SignIn(_email, _password);
        }

        private void ReadEmailField(string text)
        {
            _email = text;
            var isMatch = Regex.IsMatch(text, AuthenticationManager.MatchEmailPattern);
            if (!string.IsNullOrEmpty(text) && !isMatch)
                AddError(AuthError.InvalidEmail);
            else
                RemoveError(AuthError.InvalidEmail);
        }

        private void ReadPasswordField(string text)
        {
            _password = text;
            if (text?.Length < AuthenticationManager.MinPasswordLength)
                AddError(AuthError.WeakPassword);
            else
                RemoveError(AuthError.WeakPassword);
        }

        private void OnSignInClick()
        {
            var auth = FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser != null)
            {
                auth.SignOut();
                return;
            }
            
            gameObject.SetActive(false);
            signUpForm.gameObject.SetActive(true);
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
            var localKey = _authenticationManager.GetErrorLocalizationKey(errors.First());
            errorText.text = _localisationController.GetLanguageValue(localKey);
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
                            && !string.IsNullOrEmpty(_password);

            signInButton.interactable = condition;
        }
    }
}
