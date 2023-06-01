using RoboFactory.Authentication;
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
using RoboFactory.General.Localisation;
using RoboFactory.General.Location;
using RoboFactory.General.Money;
using RoboFactory.General.Order;
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