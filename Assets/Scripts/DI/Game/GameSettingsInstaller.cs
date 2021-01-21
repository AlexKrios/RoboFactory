using Modules.General.Audio;
using Modules.General.Item.Production;
using Modules.General.Item.Products;
using Modules.General.Item.Raw;
using Modules.General.Level;
using Modules.General.Localisation;
using Modules.General.Location;
using Modules.General.Order;
using Modules.General.Unit;
using UnityEngine;
using Zenject;

namespace Di.Game
{
    [CreateAssetMenu(fileName = "Game", menuName = "Scriptable/General/Settings", order = -1)]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public LocalisationController.Settings localization;
        public AudioController.Settings audioData;
        public LevelController.Settings level;
        public RawController.Settings raw;
        public ProductsController.Settings products;
        public UnitsController.Settings units;
        public ExpeditionController.Settings locations;
        public ProductionController.Settings production;
        public OrderController.Settings order;

        public override void InstallBindings()
        {
            Container.BindInstance(localization);
            Container.BindInstance(audioData);
            Container.BindInstance(level);
            Container.BindInstance(raw);
            Container.BindInstance(products);
            Container.BindInstance(units);
            Container.BindInstance(locations);
            Container.BindInstance(production);
            Container.BindInstance(order);
        }
    }
}