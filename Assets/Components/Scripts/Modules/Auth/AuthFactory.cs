using System;
using JetBrains.Annotations;
using RoboFactory.Auth;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    [UsedImplicitly]
    public class AuthFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject(Id = Constants.PopupsParentKey)] private readonly Transform _popupParent;

        public SignInView CreateSignInForm()
        {
            return _container.InstantiatePrefabForComponent<SignInView>(_settings.SignInPrefab, _popupParent);
        }
        
        public SignUpView CreateSignUpForm()
        {
            return _container.InstantiatePrefabForComponent<SignUpView>(_settings.SignUpPrefab, _popupParent);
        }
        
        public VerificationView CreateVerificationForm()
        {
            return _container.InstantiatePrefabForComponent<VerificationView>(_settings.VerificationPrefab, _popupParent);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private SignInView _signInPrefab;
            [SerializeField] private SignUpView _signUpPrefab;
            [SerializeField] private VerificationView _verificationPrefab;

            public SignInView SignInPrefab => _signInPrefab;
            public SignUpView SignUpPrefab => _signUpPrefab;
            public VerificationView VerificationPrefab => _verificationPrefab;
        }
    }
}
