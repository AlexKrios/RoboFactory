using UnityEngine;
using Zenject;

namespace RoboFactory.Authentication
{
    [AddComponentMenu("Scripts/Authentication/Authentication Initialization", 0)]
    public class AuthenticationInitialization : MonoBehaviour
    {
        [Inject] private readonly AuthenticationService authenticationService;

        [Inject(Id = "SignUp")] private readonly SignUpView _signUpView;
        [Inject(Id = "Verification")] private readonly VerificationView _verificationView;

        private void Awake()
        {
            authenticationService.EventSignUpSuccess += SignUpSuccess;
        }

        private void OnDestroy()
        {
            authenticationService.EventSignUpSuccess -= SignUpSuccess;
        }

        private void SignUpSuccess()
        {
            _signUpView.gameObject.SetActive(false);
            _verificationView.gameObject.SetActive(true);
        }
    }
}