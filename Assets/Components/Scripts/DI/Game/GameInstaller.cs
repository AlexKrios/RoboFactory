using Components.Scripts.Modules.Authentication;
using Components.Scripts.Modules.Factory.Api;
using Components.Scripts.Modules.General.Asset;
using Components.Scripts.Modules.General.Audio;
using Components.Scripts.Modules.General.Expedition;
using Components.Scripts.Modules.General.Item;
using Components.Scripts.Modules.General.Item.Production;
using Components.Scripts.Modules.General.Item.Products;
using Components.Scripts.Modules.General.Item.Raw;
using Components.Scripts.Modules.General.Item.Raw.Convert;
using Components.Scripts.Modules.General.Level;
using Components.Scripts.Modules.General.Localisation;
using Components.Scripts.Modules.General.Location;
using Components.Scripts.Modules.General.Money;
using Components.Scripts.Modules.General.Order;
using Components.Scripts.Modules.General.Scene;
using Components.Scripts.Modules.General.Settings;
using Components.Scripts.Modules.General.Ui;
using Components.Scripts.Modules.General.Ui.Popup;
using Components.Scripts.Modules.General.Unit;
using Components.Scripts.Modules.General.Unit.Object;
using Components.Scripts.Modules.General.User;
using UnityEngine;
using Zenject;

namespace Components.Scripts.DI.Game
{
    [AddComponentMenu("Scripts/General/Di/Game Installer")]
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().AsSingle().NonLazy();
            Container.Bind<AssetsManager>().AsSingle().NonLazy();
            Container.Bind<LocalisationManager>().AsSingle().NonLazy();
            Container.Bind<ApiManager>().AsSingle().NonLazy();
            
            InstallControllers();
            InstallUserManager();

            InstallUi();
            InstallHelpers();
        }

        private void InstallUserManager()
        {
            Container.Bind<AuthenticationManager>().AsSingle().NonLazy();
            
            Container.BindFactory<UserProfile, UserProfile.Factory>();
            Container.Bind<UserManager>().AsSingle().NonLazy();
        }

        private void InstallControllers()
        {
            Container.Bind<SettingsManager>().AsSingle().NonLazy();
            Container.Bind<AudioManager>().AsSingle().NonLazy();

            Container.Bind<SceneController>().AsSingle().NonLazy();

            Container.Bind<MoneyManager>().AsSingle().NonLazy();
            Container.Bind<LevelManager>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<RawManager>().AsSingle().NonLazy();
            Container.Bind<ConvertRawController>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<ProductsManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LocationManager>().AsSingle().NonLazy();

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