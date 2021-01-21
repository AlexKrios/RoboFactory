using Modules.Factory.Menu.Conversion;
using Modules.Factory.Menu.Expedition;
using Modules.Factory.Menu.Order;
using Modules.Factory.Menu.Production;
using Modules.Factory.Menu.Settings;
using Modules.Factory.Menu.Storage;
using Modules.Factory.Menu.Units;
using UnityEngine;
using Zenject;

namespace Di.Scene.Factory
{
    [CreateAssetMenu(menuName = "Scriptable/Factory/Settings", order = 1)]
    public class FactorySettingsInstaller : ScriptableObjectInstaller<FactorySettingsInstaller>
    {
        public SettingsMenuFactory.Settings settingsMenu;
        public ProductionMenuFactory.Settings productionMenu;
        public StorageMenuFactory.Settings storageMenu;
        public ConversionMenuFactory.Settings conversionMenu;
        public OrderMenuFactory.Settings orderMenu;
        public UnitsMenuFactory.Settings unitsMenu;
        public ExpeditionMenuFactory.Settings expeditionMenu;

        public override void InstallBindings()
        {
            Container.BindInstance(settingsMenu);
            Container.BindInstance(productionMenu);
            Container.BindInstance(storageMenu);
            Container.BindInstance(conversionMenu);
            Container.BindInstance(orderMenu);
            Container.BindInstance(unitsMenu);
            Container.BindInstance(expeditionMenu);
        }
    }
}