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
    public class SignInView : MonoBehaviour
    {
        private const string HeaderTitleKey = "auth_sign_in_title";
        private const string EmailKey = "auth_email";
        private const string PasswordKey = "auth_password";
        
        #region Zenject
        
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly AuthService authService;

        [Inject(Id = Constants.SignUpKey)] private readonly SignUpView _signUpForm;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text _headerText;
        
        [Space]
        [SerializeField] private TMP_InputField _emailField;
        [SerializeField] private TMP_Text _emailPlaceholder;
        [SerializeField] private TMP_InputField _passwordField;
        [SerializeField] private TMP_Text _passwordPlaceholder;
        
        [Space]
        [SerializeField] private Button _signInButton; 
        [SerializeField] private Button _signUpButton;

        [Header("Error")]
        [SerializeField] private GameObject _errorWrapper;
        [SerializeField] private TMP_Text _errorText;

        #endregion
        
        #region Variables

        private string _authCode;
        private string _email;
        private string _password;

        private bool _isError;

        private HashSet<AuthError> _errors;
        
        private readonly CompositeDisposable _disposable = new();

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _errors = new HashSet<AuthError>();

            _signInButton.interactable = false;
            _errorWrapper.SetActive(false);
            
            _signInButton.onClick.AddListener(OnLogInClick);
            _signUpButton.onClick.AddListener(OnSignInClick);
            _emailField.onValueChanged.AddListener(ReadEmailField);
            _passwordField.onValueChanged.AddListener(ReadPasswordField);

            authService.EventSignInError += AddError;
        }

        private void Start()
        { 
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            
            _headerText.text = _localizationService.GetLanguageValue(HeaderTitleKey);
            _emailPlaceholder.text = _localizationService.GetLanguageValue(EmailKey);
            _passwordPlaceholder.text = _localizationService.GetLanguageValue(PasswordKey);
        }

        private void OnDestroy()
        {
            authService.EventSignInError -= AddError;
            
            _disposable.Dispose();
        }

        #endregion
        
        private async void OnLogInClick()
        {
            await authService.SignIn(_email, _password);
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

        private void ReadPasswordField(string text)
        {
            _password = text;
            if (text?.Length < AuthService.MinPasswordLength)
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
            _signUpForm.gameObject.SetActive(true);
        }

        private void ShowError()
        {
            SetCreateState();
            if (_errors.Count == 0)
            {
                _errorWrapper.SetActive(false);
                return;
            }
            
            _errorWrapper.SetActive(true);
            var localKey = authService.GetErrorLocalizationKey(_errors.First());
            _errorText.text = _localizationService.GetLanguageValue(localKey);
        }

        private void AddError(AuthError error, bool isError = false)
        {
            _errors.Add(error);
            ShowError();

            if (isError)
                _errors.Remove(error);
        }

        private void RemoveError(AuthError error)
        {
            _errors.Remove(error);
            ShowError();
        }

        private void SetCreateState()
        {
            var condition = _errors.Count == 0
                            && !string.IsNullOrEmpty(_email)
                            && !string.IsNullOrEmpty(_password);

            _signInButton.interactable = condition;
        }
    }
}
