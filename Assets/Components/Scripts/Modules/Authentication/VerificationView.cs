using Components.Scripts.Modules.General.Localisation;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.Authentication
{
    public class VerificationView : MonoBehaviour
    {
        private const string HeaderTitleKey = "auth_email";
        private const string VerificationTitleKey = "auth_email_verification_title";
        
        #region Zenject
        
        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly AuthenticationManager _authenticationManager;

        [Inject(Id = "SignIn")] private readonly SignInView signInForm;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text headerText;
        [SerializeField] private TMP_Text verificationText;

        [Space]
        [SerializeField] private Button backButton;
        [SerializeField] private Button resendButton;

        #endregion
        
        #region Variables
        
        private CompositeDisposable _disposable;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _disposable = new CompositeDisposable();
            
            backButton.OnClickAsObservable().Subscribe(_ => OnBackClick()).AddTo(_disposable);
            resendButton.OnClickAsObservable().Subscribe(_ => OnResendClick()).AddTo(_disposable);
        }

        private void Start()
        { 
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            
            headerText.text = _localisationController.GetLanguageValue(HeaderTitleKey);
            verificationText.text = _localisationController.GetLanguageValue(VerificationTitleKey);
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
            _authenticationManager.EmailVerification().Forget();
        }
    }
}
