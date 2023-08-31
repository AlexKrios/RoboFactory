using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using JetBrains.Annotations;
using RoboFactory.General.Api;
using RoboFactory.General.Profile;
using RoboFactory.General.Services;
using UniRx;
using UnityEngine;
using Zenject;
using UserProfile = RoboFactory.General.User.UserProfile;

namespace RoboFactory.Auth
{
    [UsedImplicitly]
    public class AuthService : Service
    {
        //private const string FirebaseProvider = "firebase";
        private const string PasswordProvider = "password";
        private const string GooglePlayProvider = "playgames.google.com";
        
        public const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
        public const float MinPasswordLength = 6;

        [Inject] private readonly CommonProfile _commonProfile;
        [Inject] private readonly ApiService _apiService;
        [Inject] private readonly UserProfile.Factory _userFactory;
        
        public override ServiceTypeEnum ServiceType => ServiceTypeEnum.NeedAuth;

        public IReactiveProperty<AuthError> ErrorCode { get; } 
            = new ReactiveProperty<AuthError>(AuthError.None);
        
        public IReactiveProperty<AuthStatusEnum> AuthStatus { get; }
            = new ReactiveProperty<AuthStatusEnum>(AuthStatusEnum.None);

        private readonly Dictionary<AuthError, string> _errorsList;
        private FirebaseUser _user;

        private static FirebaseAuth Auth => FirebaseAuth.DefaultInstance;

        public AuthService()
        {
            _errorsList = new Dictionary<AuthError, string>
            {
                { AuthError.EmailAlreadyInUse, "auth_email_already_in_use" },
                { AuthError.InvalidEmail, "auth_invalid_email" },
                { AuthError.WeakPassword, "auth_weak_password" },
                { AuthError.MissingPassword, "auth_missing_password" },
                { AuthError.UserNotFound, "auth_user_not_found" },
                { AuthError.UnverifiedEmail, "auth_unverified_email" }
            };
        }

        public string GetErrorLocalizationKey(AuthError error) => _errorsList[error];

        protected override async UniTask InitializeAsync()
        {
            Auth.StateChanged += FirebaseAuthStateChanged;
            FirebaseAuthStateChanged(this, null);
            
            if (_user == null)
            {
                AuthStatus.Value = AuthStatusEnum.Failure;
                await UniTask.WaitUntil(() => AuthStatus.Value == AuthStatusEnum.Success);
                return;
            }

            if (IsProviderUsed(PasswordProvider))
            {
                if (!_user.IsEmailVerified && !_user.Email.Contains(Constants.TestEmail))
                    AuthStatus.Value = AuthStatusEnum.Failure;
                else
                    await SignInSuccess();
            }
            else if (IsProviderUsed(GooglePlayProvider))
            {
                InitializeGooglePlay();
            }
        }

        private void FirebaseAuthStateChanged(object sender, EventArgs eventArgs) 
        {
            _user = Auth.CurrentUser;
        }
        
        private bool IsProviderUsed(string providerKey)
        {
            foreach (var provider in _user.ProviderData)
            {
                if (provider.ProviderId.Equals(providerKey))
                    return true;
            }
            
            return false;
        }

        public async UniTask SignIn(string email, string password)
        {
            var task = Auth.SignInWithEmailAndPasswordAsync(email, password);
            await UniTask.WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) 
            {
                if (task.Exception?.Flatten().InnerExceptions[0] is not FirebaseException exception)
                    return;

                Debug.LogWarning((AuthError)exception.ErrorCode);
                ErrorCode.Value = (AuthError)exception.ErrorCode;
                return;
            }
            
            if (!_user.IsEmailVerified && !_user.Email.Contains(Constants.TestEmail))
            {
                ErrorCode.Value = AuthError.UnverifiedEmail;
                return;
            }

            await SignInSuccess();
            Debug.Log("User sign in successfully");
        }

        public async void SignUp(string email, string password)
        {
            var task = Auth.CreateUserWithEmailAndPasswordAsync(email, password);
            await UniTask.WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) 
            {
                if (task.Exception?.Flatten().InnerExceptions[0] is not FirebaseException exception)
                    return;

                Debug.LogWarning((AuthError)exception.ErrorCode);
                ErrorCode.Value = (AuthError)exception.ErrorCode;
                return;
            }

            Debug.Log("User sign up successfully");
            
            await EmailVerification();
        }

        public async UniTask EmailVerification()
        {
            var task = _user.SendEmailVerificationAsync();
            await UniTask.WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) 
            {
                if (task.Exception?.Flatten().InnerExceptions[0] is not FirebaseException exception)
                    return;

                Debug.LogWarning((AuthError)exception.ErrorCode);
                ErrorCode.Value = (AuthError)exception.ErrorCode;
                return;
            }
            
            Debug.Log("Email sent successfully");
        }

        public void SignOut()
        {
            AuthStatus.Value = AuthStatusEnum.None;
            Auth.SignOut();
        }

        #region Google Play

        public bool IsGooglePlayConnected()
        {
            return IsProviderUsed(GooglePlayProvider);
        }
        
        private void InitializeGooglePlay()
        {
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.Authenticate(InitializeGooglePlayCallback);
        }

        private async void InitializeGooglePlayCallback(SignInStatus status)
        {
            switch (status)
            {
                case SignInStatus.Success:
                    await UniTask.RunOnThreadPool(GooglePlaySignIn);
                    break;

                case SignInStatus.Canceled:
                    Auth.SignOut();
                    AuthStatus.Value = AuthStatusEnum.Failure;
                    Debug.LogWarning("Cancel Google Play Sign In");
                    break;

                case SignInStatus.InternalError:
                    AuthStatus.Value = AuthStatusEnum.Failure;
                    Debug.LogWarning("Internal Error Google Play Sign In");
                    break;

                default:
                    AuthStatus.Value = AuthStatusEnum.Failure;
                    break;
            }
        }
        
        public void GooglePlaySignIn()
        {
            PlayGamesPlatform.Instance.RequestServerSideAccess(true, SignInWithCredential);
        }
        
        public void GooglePlaySignManually()
        {
            PlayGamesPlatform.Instance.ManuallyAuthenticate(_ =>
            {
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, SignInWithCredential);
            });
        }

        private async void SignInWithCredential(string authCode)
        {
            var credential = PlayGamesAuthProvider.GetCredential(authCode);
            var task = Auth.SignInWithCredentialAsync(credential);
            await UniTask.WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) 
            {
                if (task.Exception?.Flatten().InnerExceptions[0] is not FirebaseException exception)
                    return;

                Debug.LogWarning((AuthError)exception.ErrorCode);
                ErrorCode.Value = (AuthError)exception.ErrorCode;
            }

            await SignInSuccess();
            Debug.LogWarning("Success Google Play Sign In");
        }

        #endregion

        private async UniTask SignInSuccess()
        {
            var userProfile = await _apiService.GetUserProfile();
            if (userProfile == null)
            {
                userProfile = _userFactory.Create().GetStartUserProfile();
                await _apiService.SetStartUserProfile(userProfile);
            }

            _commonProfile.UserProfile = userProfile;
            
            AuthStatus.Value = AuthStatusEnum.Success;
        }
    }
}
