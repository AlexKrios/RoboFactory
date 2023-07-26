using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Firebase.Auth;
using RoboFactory.Factory.Menu.Production;
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
        
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly AuthFactory _authFactory;
        [Inject] private readonly AuthService _authService;
        
        [SerializeField] private TMP_Text _headerText;
        
        [Space]
        [SerializeField] private TMP_InputField _emailField;
        [SerializeField] private TMP_Text _emailPlaceholder;
        [SerializeField] private TMP_InputField _passwordField;
        [SerializeField] private TMP_Text _passwordPlaceholder;
        
        [Space]
        [SerializeField] private Button _signInButton; 
        [SerializeField] private Button _signInTestButton; 
        [SerializeField] private Button _signUpButton;

        [Header("Error")]
        [SerializeField] private GameObject _errorWrapper;
        [SerializeField] private TMP_Text _errorText;
        
        private string _authCode;
        private string _email;
        private string _password;

        private bool _isError;

        private readonly HashSet<AuthError> _errors = new();
        private readonly CompositeDisposable _disposable = new();

        private void Awake()
        {
            _signInButton.interactable = false;
            _errorWrapper.SetActive(false);
            
            _signInButton.onClick.AddListener(OnSignInClick);
            _signInTestButton.onClick.AddListener(OnSignInTestClick);
            _signUpButton.onClick.AddListener(OnSignUpClick);
            _emailField.onValueChanged.AddListener(ReadEmailField);
            _passwordField.onValueChanged.AddListener(ReadPasswordField);
            
            _authService.ErrorCode.Subscribe(x => AddError(x, true)).AddTo(_disposable);
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
            _disposable.Dispose();
        }
        
        private async void OnSignInClick()
        {
            await _authService.SignIn(_email, _password);
        }
        
        private async void OnSignInTestClick()
        {
            await _authService.SignIn(Constants.TestEmail, Constants.TestPassword);
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

        private void OnSignUpClick()
        {
            var auth = FirebaseAuth.DefaultInstance;
            if (auth.CurrentUser != null)
            {
                auth.SignOut();
                return;
            }
            
            Destroy(gameObject);
            _authFactory.CreateSignUpForm();
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
            var localKey = _authService.GetErrorLocalizationKey(_errors.First());
            _errorText.text = _localizationService.GetLanguageValue(localKey);
        }

        private void AddError(AuthError error, bool isError = false)
        {
            if (error == AuthError.None) return;
            
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
