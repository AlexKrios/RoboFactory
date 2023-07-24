using UnityEngine;
using Zenject;

namespace RoboFactory.Auth
{
    public class AuthInitialization : MonoBehaviour
    {
        [Inject] private readonly AuthService authService;

        [Inject(Id = "SignUp")] private readonly SignUpView _signUpView;
        [Inject(Id = "Verification")] private readonly VerificationView _verificationView;

        private void Awake()
        {
            authService.EventSignUpSuccess += SignUpSuccess;
        }

        private void OnDestroy()
        {
            authService.EventSignUpSuccess -= SignUpSuccess;
        }

        private void SignUpSuccess()
        {
            _signUpView.gameObject.SetActive(false);
            _verificationView.gameObject.SetActive(true);
        }
    }
}