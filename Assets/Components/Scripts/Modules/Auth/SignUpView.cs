using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Firebase.Auth;
using RoboFactory.General.Localization;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Auth
{
    public class SignUpView : MonoBehaviour
    {
        private const string HeaderTitleKey = "auth_sign_up_title";
        private const string EmailKey = "auth_email";
        private const string PasswordKey = "auth_password";
        private const string ConfirmKey = "auth_confirm";
        
        #region Zenject
        
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly AuthService authService;

        [Inject(Id = Constants.SignInKey)] private readonly SignInView _signInForm;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text _headerText;
        
        [Space]
        [SerializeField] private TMP_InputField _emailField;
        [SerializeField] private TMP_Text _emailPlaceholder;
        [SerializeField] private TMP_InputField _passwordField;
        [SerializeField] private TMP_Text _passwordPlaceholder;
        [SerializeField] private TMP_InputField _confirmField;
        [SerializeField] private TMP_Text _confirmPlaceholder;
        
        [Space]
        [SerializeField] private Button _signUpButton;
        [SerializeField] private Button _googlePlayButton;
        [SerializeField] private Button _backButton;
        
        [Header("Error")]
        [SerializeField] private GameObject _errorWrapper;
        [SerializeField] private TMP_Text _errorText;

        #endregion
        
        #region Variables

        private string _authCode;
        private string _email;
        private string _password;
        private string _confirm;

        private HashSet<AuthError> errors;
        
        private readonly CompositeDisposable _disposable = new();

        #endregion

        #region Unity Methods

        private void Awake()
        {
            errors = new HashSet<AuthError>();

            _signUpButton.interactable = false;
            _errorWrapper.SetActive(false);
            
            _signUpButton.onClick.AddListener(OnSignInClick);
            _googlePlayButton.onClick.AddListener(OnGooglePlayClick);
            _backButton.onClick.AddListener(OnBackClick);
            _emailField.onValueChanged.AddListener(ReadEmailField);
            _passwordField.onValueChanged.AddListener(ReadFirstPasswordField);
            _confirmField.onValueChanged.AddListener(ReadSecondPasswordField);
            
            authService.EventSignUpError += AddError;
            authService.EventGooglePlayError += AddError;
        }

        private void Start()
        { 
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            
            _headerText.text = _localizationService.GetLanguageValue(HeaderTitleKey);
            _emailPlaceholder.text = _localizationService.GetLanguageValue(EmailKey);
            _passwordPlaceholder.text = _localizationService.GetLanguageValue(PasswordKey);
            _confirmPlaceholder.text = _localizationService.GetLanguageValue(ConfirmKey);
        }

        private void OnDestroy()
        {
            authService.EventSignUpError -= AddError;
            authService.EventGooglePlayError -= AddError;
            
            _disposable.Dispose();
        }

        #endregion
        
        private void OnSignInClick()
        {
            authService.SignUp(_email, _password);
        }
        
        private void OnGooglePlayClick()
        {
            authService.GooglePlaySignManually();
        }

        private void ReadEmailField(string text)
        {
            _email = text;
            var isMatch = Regex.IsMatch(text, AuthService.MatchEmailPattern);
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
            
            if (text?.Length < AuthService.MinPasswordLength)
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
            
            if (text?.Length < AuthService.MinPasswordLength)
                AddError(AuthError.WeakPassword);
            else
                RemoveError(AuthError.WeakPassword);
        }

        private void OnBackClick()
        {
            gameObject.SetActive(false);
            _signInForm.gameObject.SetActive(true);
        }

        private void ShowError()
        {
            SetCreateState();
            if (errors.Count == 0)
            {
                _errorWrapper.SetActive(false);
                return;
            }
            
            _errorWrapper.SetActive(true);
            var localKey = authService.GetErrorLocalizationKey(errors.First());
            _errorText.text = _localizationService.GetLanguageValue(localKey);
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
            
            _signUpButton.interactable = condition;
        }
    }
}
