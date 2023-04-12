using Modules.Factory.Api;
using Modules.Factory.Menu.Conversion;
using Modules.Factory.Menu.Expedition;
using Modules.Factory.Menu.Order;
using Modules.Factory.Menu.Storage;
using Modules.Factory.Menu.Units;
using Modules.General.Audio;
using Modules.General.Coroutines;
using Modules.General.Item;
using Modules.General.Item.Production;
using Modules.General.Item.Products;
using Modules.General.Item.Raw;
using Modules.General.Item.Raw.Convert;
using Modules.General.Level;
using Modules.General.Localisation;
using Modules.General.Location;
using Modules.General.Money;
using Modules.General.Order;
using Modules.General.Save;
using Modules.General.Scene;
using Modules.General.Settings;
using Modules.General.Ui;
using Modules.General.Ui.Popup;
using Modules.General.Unit;
using UnityEngine;
using Zenject;

namespace Di.Game
{
    [AddComponentMenu("Scripts/General/Di/Game Installer")]
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().AsSingle().NonLazy();

            Container.BindInterfacesTo<LocalisationController>().AsSingle().NonLazy();

            InitCoroutines();
            InstallUi();
            InstallControllers();
            InstallHelpers();
            InstallApi();
            InstallFactoryMenu();

            Container.BindInterfacesTo<SaveController>().AsSingle().NonLazy();
        }

        private void InitCoroutines()
        {
            var coroutinesGameObject = new GameObject("----- Coroutines -----");
            Container
                .BindInterfacesTo<CoroutinesController>()
                .FromNewComponentOn(coroutinesGameObject)
                .AsSingle()
                .NonLazy();
            DontDestroyOnLoad(coroutinesGameObject);
        }

        private void InstallControllers()
        {
            Container.BindInterfacesTo<SettingsController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<AudioController>().AsSingle().NonLazy();

            Container.Bind<SceneController>().AsSingle().NonLazy();

            Container.BindInterfacesTo<MoneyController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LevelController>().AsSingle().NonLazy();

            Container.BindInterfacesTo<RawController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ProductsController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ConvertRawController>().AsSingle().NonLazy();

            Container.BindInterfacesTo<UnitsController>().AsSingle().NonLazy();

            Container.BindInterfacesTo<ExpeditionController>().AsSingle().NonLazy();

            Container.BindInterfacesTo<ProductionController>().AsSingle().NonLazy();

            Container.BindInterfacesTo<OrderController>().AsSingle().NonLazy();
        }

        private void InstallHelpers()
        {
            Container.Bind<ControllersResolver>().AsSingle().NonLazy();
        }

        private void InstallUi()
        {
            Container.BindInterfacesTo<UiController>().AsSingle().NonLazy();
            Container.Bind<PopupFactory>().AsSingle().NonLazy();
        }

        private void InstallApi()
        {
            Container.BindInterfacesTo<ApiControllers>().AsSingle().NonLazy();
        }

        private void InstallFactoryMenu()
        {
            Container.Bind<StorageMenuManager>().AsSingle().NonLazy();
            Container.Bind<UnitsMenuManager>().AsSingle().NonLazy();
            Container.Bind<ExpeditionMenuManager>().AsSingle().NonLazy();
            Container.Bind<ConversionMenuManager>().AsSingle().NonLazy();
            Container.Bind<OrderMenuManager>().AsSingle().NonLazy();
        }
    }
}