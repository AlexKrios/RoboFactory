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
    [AddComponentMenu("Scripts/Factory/Di/Factory Installer")]
    public class FactoryInstaller : MonoInstaller
    {
        [SerializeField] private FactoryUi factoryUi;

        public override void InstallBindings()
        {
            Container.Bind<FactoryCameraController>().AsSingle().NonLazy();
            Container.Bind<FactoryUi>().FromInstance(factoryUi).AsSingle().NonLazy();

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