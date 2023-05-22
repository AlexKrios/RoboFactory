using Components.Scripts.Modules.Factory.Cameras;
using Components.Scripts.Modules.Factory.Menu;
using Components.Scripts.Modules.Factory.Menu.Conversion;
using Components.Scripts.Modules.Factory.Menu.Expedition;
using Components.Scripts.Modules.Factory.Menu.Expedition.Queue.Cell;
using Components.Scripts.Modules.Factory.Menu.Expedition.Queue.Cell.State;
using Components.Scripts.Modules.Factory.Menu.Order;
using Components.Scripts.Modules.Factory.Menu.Production;
using Components.Scripts.Modules.Factory.Menu.Production.Queue.Cell;
using Components.Scripts.Modules.Factory.Menu.Production.Queue.Cell.State;
using Components.Scripts.Modules.Factory.Menu.Settings;
using Components.Scripts.Modules.Factory.Menu.Storage;
using Components.Scripts.Modules.Factory.Menu.Units;
using UnityEngine;
using Zenject;

namespace Components.Scripts.DI.Scene.Factory
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