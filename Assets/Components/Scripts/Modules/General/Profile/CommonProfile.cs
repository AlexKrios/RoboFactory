using JetBrains.Annotations;
using RoboFactory.Auth;
using UniRx;

namespace RoboFactory.General.Profile
{
    [UsedImplicitly]
    public class CommonProfile
    {
        public IReactiveProperty<AuthStatus> AuthStatus { get; set; }
            = new ReactiveProperty<AuthStatus>(Auth.AuthStatus.None);
    }
}