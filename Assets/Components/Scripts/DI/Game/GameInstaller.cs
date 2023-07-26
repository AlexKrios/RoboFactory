using RoboFactory.Auth;
using RoboFactory.Factory.Menu.Production;
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
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private Transform _popupParent;
        [SerializeField] private Transform _screenParent;

        public override void InstallBindings()
        {
            Container.Bind<AudioSource>().WithId(Constants.MusicSourceKey).FromInstance(_musicSource);
            Container.Bind<Transform>().WithId(Constants.PopupParentKey).FromInstance(_popupParent);
            Container.Bind<Transform>().WithId(Constants.ScreenParentKey).FromInstance(_screenParent);

            InstallProfiles();
            
            Container.BindService<SettingsService>();
            Container.BindService<LocalizationService>();
            Container.BindService<AudioService>();
            Container.BindService<ApiService>();
            Container.BindService<SceneService>();

            InstallAuthComponents();

            Container.BindService<AddressableService>();

            Container.BindService<MoneyService>();
            Container.BindService<ExperienceService>();

            Container.BindService<RawService>();
            Container.BindService<ConvertRawService>();
            
            Container.BindService<ProductsService>();
            Container.BindService<LocationsService>();

            Container.BindFactory<UnitObject, UnitObject.Factory>();
            Container.BindService<UnitsService>();

            Container.BindService<ExpeditionService>();
            Container.BindService<ProductionService>();

            Container.BindService<OrderService>();

            InstallUi();
            InstallHelpers();
        }

        private void InstallProfiles()
        {
            Container.Bind<CommonProfile>().AsSingle().NonLazy();
        }

        private void InstallAuthComponents()
        {
            Container.Bind<AuthFactory>().AsSingle().NonLazy();
            Container.BindService<AuthService>();
            Container.BindFactory<UserProfile, UserProfile.Factory>();
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