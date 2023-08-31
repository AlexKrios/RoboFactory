using RoboFactory.Factory.Cameras;
using RoboFactory.Factory.Menu;
using RoboFactory.Factory.Menu.Conversion;
using RoboFactory.Factory.Menu.Expedition;
using RoboFactory.Factory.Menu.Order;
using RoboFactory.Factory.Menu.Production;
using RoboFactory.Factory.Menu.Settings;
using RoboFactory.Factory.Menu.Storage;
using UnityEngine;
using Zenject;

namespace RoboFactory.DI
{
    public class FactoryInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private FactoryHud _factoryHud;
        [SerializeField] private MenuButtonsView _menuButtons;
        [Inject(Id = Constants.HudParentKey)] private readonly Transform _hudParent;

        public override void InstallBindings()
        {
            Container.Bind<Camera>().WithId(Constants.MainCameraKey).FromInstance(_mainCamera);
            Container.Bind<FactoryCameraController>().AsSingle().NonLazy();
            Container.Bind<FactoryHud>().FromInstance(_factoryHud).AsSingle().NonLazy();
            _factoryHud.transform.SetParent(_hudParent);
            
            Container.Bind<MenuButtonsView>().FromInstance(_menuButtons).AsSingle().NonLazy();

            InstallUiFactory();
            InstallMenu();
        }
        
        private void InstallUiFactory()
        {
            Container.BindFactory<ProductionCell, ProductionCellEmpty, ProductionCellEmpty.Factory>();
            Container.BindFactory<ProductionCell, ProductionCellBusy, ProductionCellBusy.Factory>();
            Container.BindFactory<ProductionCell, ProductionCellFinish, ProductionCellFinish.Factory>();

            Container.BindFactory<ExpeditionCell, ExpeditionCellEmpty, ExpeditionCellEmpty.Factory>();
            Container.BindFactory<ExpeditionCell, ExpeditionCellBusy, ExpeditionCellBusy.Factory>();
            Container.BindFactory<ExpeditionCell, ExpeditionCellFinish, ExpeditionCellFinish.Factory>();
        }

        private void InstallMenu()
        {
            Container.Bind<SettingsMenuFactory>().AsSingle().NonLazy();
            Container.Bind<ProductionMenuFactory>().AsSingle().NonLazy();
            Container.Bind<StorageMenuFactory>().AsSingle().NonLazy();
            Container.Bind<UnitsMenuFactory>().AsSingle().NonLazy();
            Container.Bind<ExpeditionMenuFactory>().AsSingle().NonLazy();
            Container.Bind<ConversionMenuFactory>().AsSingle().NonLazy();
            Container.Bind<OrderMenuFactory>().AsSingle().NonLazy();
        }
    }
}