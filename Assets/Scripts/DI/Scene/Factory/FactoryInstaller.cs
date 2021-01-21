using Modules.Factory.Building;
using Modules.Factory.Cameras;
using Modules.Factory.Menu;
using Modules.Factory.Menu.Conversion;
using Modules.Factory.Menu.Expedition;
using Modules.Factory.Menu.Expedition.Queue.Cell;
using Modules.Factory.Menu.Expedition.Queue.Cell.State;
using Modules.Factory.Menu.Order;
using Modules.Factory.Menu.Production;
using Modules.Factory.Menu.Production.Queue.Cell;
using Modules.Factory.Menu.Production.Queue.Cell.State;
using Modules.Factory.Menu.Production.Queue.Result;
using Modules.Factory.Menu.Settings;
using Modules.Factory.Menu.Storage;
using Modules.Factory.Menu.Units;
using UnityEngine;
using Zenject;

namespace Di.Scene.Factory
{
    [AddComponentMenu("Scripts/Factory/Di/Factory Installer")]
    public class FactoryInstaller : MonoInstaller
    {
        [SerializeField] private FactoryUi factoryUi;

        public override void InstallBindings()
        {
            Container.Bind<FactoryUi>().FromInstance(factoryUi).AsSingle().NonLazy();
            
            InstallCameraComponents();
            InstallUiFactory();
            InstallMenu();
        }

        private void InstallCameraComponents()
        {
            Container.BindInterfacesTo<FactoryBuildingController>().AsSingle().NonLazy();
            
            Container.Bind<CameraController>().AsSingle().NonLazy();
        }

        private void InstallUiFactory()
        {
            Container.BindFactory<ProductionCell, ProductionCellEmpty, ProductionCellEmpty.Factory>();
            Container.BindFactory<ProductionCell, ProductionCellBusy, ProductionCellBusy.Factory>();
            Container.BindFactory<ProductionCell, ProductionCellFinish, ProductionCellFinish.Factory>();

            //Container.BindFactory<CraftObject, ResultCanvas, ResultCanvas.Factory>().FromFactory<ResultCanvasFactory>();
            Container.BindFactory<GameObject, ResultModel, ResultModel.Factory>().FromFactory<ResultModelFactory>();
            
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