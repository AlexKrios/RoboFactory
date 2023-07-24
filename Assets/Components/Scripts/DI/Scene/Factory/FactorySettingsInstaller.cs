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
    [CreateAssetMenu(menuName = "Scriptable/Factory/Settings", order = 1)]
    public class FactorySettingsInstaller : ScriptableObjectInstaller<FactorySettingsInstaller>
    {
        [SerializeField] private SettingsMenuFactory.Settings _settingsMenu;
        [SerializeField] private ProductionMenuFactory.Settings _productionMenu;
        [SerializeField] private StorageMenuFactory.Settings _storageMenu;
        [SerializeField] private ConversionMenuFactory.Settings _conversionMenu;
        [SerializeField] private OrderMenuFactory.Settings _orderMenu;
        [SerializeField] private UnitsMenuFactory.Settings _unitsMenu;
        [SerializeField] private ExpeditionMenuFactory.Settings _expeditionMenu;

        public override void InstallBindings()
        {
            Container.BindInstance(_settingsMenu);
            Container.BindInstance(_productionMenu);
            Container.BindInstance(_storageMenu);
            Container.BindInstance(_conversionMenu);
            Container.BindInstance(_orderMenu);
            Container.BindInstance(_unitsMenu);
            Container.BindInstance(_expeditionMenu);
        }
    }
}