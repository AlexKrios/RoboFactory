using Cysharp.Threading.Tasks;
using RoboFactory.General.Localisation;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Authentication
{
    [AddComponentMenu("Scripts/Authentication/Verification View")]
    public class VerificationView : MonoBehaviour
    {
        private const string HeaderTitleKey = "auth_email";
        private const string VerificationTitleKey = "auth_email_verification_title";
        
        #region Zenject
        
        [Inject] private readonly LocalizationService localizationController;
        [Inject] private readonly AuthenticationService authenticationService;

        [Inject(Id = "SignIn")] private readonly SignInView signInForm;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text _headerText;
        [SerializeField] private TMP_Text _verificationText;

        [Space]
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _resendButton;

        #endregion
        
        #region Variables
        
        private CompositeDisposable _disposable;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _disposable = new CompositeDisposable();
            
            _backButton.OnClickAsObservable().Subscribe(_ => OnBackClick()).AddTo(_disposable);
            _resendButton.OnClickAsObservable().Subscribe(_ => OnResendClick()).AddTo(_disposable);
        }

        private void Start()
        { 
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            
            _headerText.text = localizationController.GetLanguageValue(HeaderTitleKey);
            _verificationText.text = localizationController.GetLanguageValue(VerificationTitleKey);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        #endregion
        
        private void OnBackClick()
        {
            gameObject.SetActive(false);
            signInForm.gameObject.SetActive(true);
        }
        
        private void OnResendClick()
        {
            authenticationService.EmailVerification().Forget();
        }
    }
}
