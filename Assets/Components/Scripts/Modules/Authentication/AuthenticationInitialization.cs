using UnityEngine;
using Zenject;

namespace RoboFactory.Authentication
{
    [AddComponentMenu("Scripts/Authentication/Authentication Initialization", 0)]
    public class AuthenticationInitialization : MonoBehaviour
    {
        [Inject] private readonly AuthenticationManager _authenticationManager;

        [Inject(Id = "SignUp")] private readonly SignUpView _signUpView;
        [Inject(Id = "Verification")] private readonly VerificationView _verificationView;

        private void Awake()
        {
            _authenticationManager.EventSignUpSuccess += SignUpSuccess;
        }

        private void OnDestroy()
        {
            _authenticationManager.EventSignUpSuccess -= SignUpSuccess;
        }

        private void SignUpSuccess()
        {
            _signUpView.gameObject.SetActive(false);
            _verificationView.gameObject.SetActive(true);
        }
    }
}