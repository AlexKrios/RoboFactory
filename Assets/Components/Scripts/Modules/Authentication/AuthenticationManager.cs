using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using JetBrains.Annotations;
using UnityEngine;

namespace RoboFactory.Authentication
{
    [UsedImplicitly]
    public class AuthenticationManager
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
        
        public Action EventSignInSuccess { get; set; }
        public Action EventSignInFailure { get; set; }
        public Action<AuthError, bool> EventSignInError { get; set; }
        public Action EventSignUpSuccess { get; set; }
        public Action<AuthError, bool> EventSignUpError { get; set; }
        public Action<AuthError, bool> EventGooglePlayError { get; set; }
        
        private readonly Dictionary<AuthError, string> errorsList;

        private FirebaseUser _user;

        private static FirebaseAuth Auth => FirebaseAuth.DefaultInstance;

        public AuthenticationManager()
        {
            errorsList = new Dictionary<AuthError, string>
            {
                { AuthError.EmailAlreadyInUse, "auth_email_already_in_use" },
                { AuthError.InvalidEmail, "auth_invalid_email" },
                { AuthError.WeakPassword, "auth_weak_password" },
                { AuthError.MissingPassword, "auth_missing_password" },
                { AuthError.UserNotFound, "auth_user_not_found" },
                { AuthError.UnverifiedEmail, "auth_unverified_email" }
            };
        }

        public string GetErrorLocalizationKey(AuthError error) => errorsList[error];

        public void Initialize()
        {
            InitializeFirebase();
            
#if UNITY_EDITOR
            SignIn(Constants.TestEmail, Constants.TestPassword);
#endif
            
            if (_user == null)
            {
                EventSignInFailure?.Invoke();
                return;
            }

            if (IsProviderUsed(PasswordProvider))
            {
                if (!_user.IsEmailVerified && !_user.Email.Contains(Constants.TestEmail))
                    EventSignInFailure?.Invoke();
                else
                    EventSignInSuccess?.Invoke();
            }
            else if (IsProviderUsed(GooglePlayProvider))
            {
                InitializeGooglePlay();
            }
        }

        private void InitializeFirebase()
        {
            Auth.StateChanged += FirebaseAuthStateChanged;
            FirebaseAuthStateChanged(this, null);
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

        public async void SignIn(string email, string password)
        {
            var task = Auth.SignInWithEmailAndPasswordAsync(email, password);
            await UniTask.WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) 
            {
                if (task.Exception?.Flatten().InnerExceptions[0] is not FirebaseException exception)
                    return;

                Debug.LogWarning((AuthError)exception.ErrorCode);
                EventSignInError?.Invoke((AuthError)exception.ErrorCode, true);
                return;
            }
            
            if (!_user.IsEmailVerified)
            {
                EventSignInError?.Invoke(AuthError.UnverifiedEmail, true);
                return;
            }

            EventSignInSuccess?.Invoke();
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
                EventSignUpError?.Invoke((AuthError)exception.ErrorCode, true);
                return;
            }

            Debug.Log("User sign up successfully");
            
            await EmailVerification();
            EventSignUpSuccess?.Invoke();
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
                EventSignUpError?.Invoke((AuthError)exception.ErrorCode, true);
                return;
            }
            
            Debug.Log("Email sent successfully");
        }

        public void SignOut()
        {
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
                    EventSignInFailure?.Invoke();
                    Debug.LogWarning("Cancel Google Play Sign In");
                    break;

                case SignInStatus.InternalError:
                    EventSignInFailure?.Invoke();
                    Debug.LogWarning("Internal Error Google Play Sign In");
                    break;

                default:
                    EventSignInFailure?.Invoke();
                    break;
            }
        }
        
        public void GooglePlaySignIn()
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
                EventGooglePlayError?.Invoke((AuthError)exception.ErrorCode, true);
            }
                        
            EventSignInSuccess?.Invoke();
            Debug.LogWarning("Success Google Play Sign In");
        }

        #endregion
    }
}
