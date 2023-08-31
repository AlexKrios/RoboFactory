using Cysharp.Threading.Tasks;
using RoboFactory.General.Localization;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Auth
{
    public class VerificationView : MonoBehaviour
    {
        private const string HeaderTitleKey = "auth_email";
        private const string VerificationTitleKey = "auth_email_verification_title";
        
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly AuthService authService;

        [Inject(Id = Constants.SignInKey)] private readonly SignInView _signInForm;

        [SerializeField] private TMP_Text _headerText;
        [SerializeField] private TMP_Text _verificationText;

        [Space]
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _resendButton;
        
        private readonly CompositeDisposable _disposable = new();

        private void Awake()
        {
            _backButton.OnClickAsObservable().Subscribe(_ => OnBackClick()).AddTo(_disposable);
            _resendButton.OnClickAsObservable().Subscribe(_ => OnResendClick()).AddTo(_disposable);
        }

        private void Start()
        { 
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            
            _headerText.text = _localizationService.GetLanguageValue(HeaderTitleKey);
            _verificationText.text = _localizationService.GetLanguageValue(VerificationTitleKey);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
        
        private void OnBackClick()
        {
            gameObject.SetActive(false);
            _signInForm.gameObject.SetActive(true);
        }
        
        private void OnResendClick()
        {
            authService.EmailVerification().Forget();
        }
    }
}
