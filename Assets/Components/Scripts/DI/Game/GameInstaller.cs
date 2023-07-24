using RoboFactory.Auth;
using RoboFactory.General;
using RoboFactory.General.Api;
using RoboFactory.General.Asset;
using RoboFactory.General.Audio;
using RoboFactory.General.Expedition;
using RoboFactory.General.Item;
using RoboFactory.General.Item.Production;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Item.Raw.Convert;
using RoboFactory.General.Level;
using RoboFactory.General.Localization;
using RoboFactory.General.Location;
using RoboFactory.General.Money;
using RoboFactory.General.Order;
using RoboFactory.General.Profile;
using RoboFactory.General.Scene;
using RoboFactory.General.Settings;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Popup;
using RoboFactory.General.Unit;
using RoboFactory.General.User;
using UnityEngine;
using Zenject;

namespace RoboFactory.DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Transform _popupParent;
        [SerializeField] private Transform _screenParent;
        
        [SerializeField] private SignInView _signInForm;
        [SerializeField] private SignUpView _signUpForm;
        [SerializeField] private VerificationView _verificationForm;
        
        public override void InstallBindings()
        {
            Container.Bind<Transform>().WithId(Constants.ScreenParentKey).FromInstance(_popupParent);
            Container.Bind<Transform>().WithId(Constants.PopupParentKey).FromInstance(_screenParent);
            
            Container.Bind<SignInView>().WithId(Constants.SignInKey).FromInstance(_signInForm);
            Container.Bind<SignUpView>().WithId(Constants.SignUpKey).FromInstance(_signUpForm);
            Container.Bind<VerificationView>().WithId(Constants.VerificationKey).FromInstance(_verificationForm);
            
            InstallProfiles();
            
            Container.BindService<SettingsService>();
            Container.BindService<LocalizationService>();
            Container.BindService<ApiService>();
            
            Container.BindService<AuthService>();
            Container.BindFactory<UserProfile, UserProfile.Factory>();
            
            Container.BindService<AddressableService>();

            InstallControllers();

            InstallUi();
            InstallHelpers();
        }

        private void InstallProfiles()
        {
            Container.Bind<CommonProfile>().AsSingle().NonLazy();
        }

        private void InstallControllers()
        {
            Container.Bind<AudioManager>().AsSingle().NonLazy();
            
            Container.BindService<SceneService>();

            Container.Bind<MoneyManager>().AsSingle().NonLazy();
            Container.Bind<LevelManager>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<RawManager>().AsSingle().NonLazy();
            Container.Bind<ConvertRawController>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<ProductsManager>().AsSingle().NonLazy();
            Container.Bind<LocationManager>().AsSingle().NonLazy();

            Container.BindFactory<UnitObject, UnitObject.Factory>();
            Container.Bind<UnitsManager>().AsSingle().NonLazy();

            Container.Bind<ExpeditionManager>().AsSingle().NonLazy();
            Container.Bind<ProductionManager>().AsSingle().NonLazy();

            Container.Bind<OrderManager>().AsSingle().NonLazy();
        }

        private void InstallHelpers()
        {
            Container.Bind<ManagersResolver>().AsSingle().NonLazy();
        }

        private void InstallUi()
        {
            Container.BindInterfacesTo<UiController>().AsSingle().NonLazy();
            Container.Bind<PopupFactory>().AsSingle().NonLazy();
        }
    }
}