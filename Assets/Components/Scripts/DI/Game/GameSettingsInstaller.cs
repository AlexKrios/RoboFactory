using Components.Scripts.Modules.General.Audio;
using Components.Scripts.Modules.General.Expedition;
using Components.Scripts.Modules.General.Item.Production;
using Components.Scripts.Modules.General.Item.Products;
using Components.Scripts.Modules.General.Item.Raw;
using Components.Scripts.Modules.General.Level;
using Components.Scripts.Modules.General.Localisation;
using Components.Scripts.Modules.General.Location;
using Components.Scripts.Modules.General.Order;
using Components.Scripts.Modules.General.Unit;
using UnityEngine;
using Zenject;

namespace Components.Scripts.DI.Game
{
    [CreateAssetMenu(fileName = "Game", menuName = "Scriptable/General/Settings", order = -1)]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public LocalisationManager.Settings localization;
        public AudioManager.Settings audioData;
        public LevelManager.Settings level;
        public RawManager.Settings raw;
        public ProductsManager.Settings products;
        public LocationManager.Settings locations;
        public UnitsManager.Settings units;
        public ExpeditionManager.Settings expeditions;
        public ProductionManager.Settings productions;
        public OrderManager.Settings order;

        public override void InstallBindings()
        {
            Container.BindInstance(localization);
            Container.BindInstance(audioData);
            Container.BindInstance(level);
            Container.BindInstance(raw);
            Container.BindInstance(products);
            Container.BindInstance(locations);
            Container.BindInstance(units);
            Container.BindInstance(expeditions);
            Container.BindInstance(productions);
            Container.BindInstance(order);
        }
    }
}