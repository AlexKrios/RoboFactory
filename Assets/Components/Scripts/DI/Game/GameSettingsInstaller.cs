using RoboFactory.General.Audio;
using RoboFactory.General.Expedition;
using RoboFactory.General.Item.Production;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Level;
using RoboFactory.General.Localisation;
using RoboFactory.General.Location;
using RoboFactory.General.Order;
using RoboFactory.General.Unit;
using UnityEngine;
using Zenject;

namespace RoboFactory.DI
{
    [CreateAssetMenu(fileName = "Game", menuName = "Scriptable/General/Settings", order = -1)]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private LocalizationService.Settings _localization;
        [SerializeField] private AudioManager.Settings _audioData;
        [SerializeField] private LevelManager.Settings _level;
        [SerializeField] private RawManager.Settings _raw;
        public ProductsManager.Settings products;
        [SerializeField] private LocationManager.Settings _locations;
        public UnitsManager.Settings units;
        [SerializeField] private ExpeditionManager.Settings _expeditions;
        [SerializeField] private ProductionManager.Settings _productions;
        [SerializeField] private OrderManager.Settings _order;

        public override void InstallBindings()
        {
            Container.BindInstance(_localization);
            Container.BindInstance(_audioData);
            Container.BindInstance(_level);
            Container.BindInstance(_raw);
            Container.BindInstance(products);
            Container.BindInstance(_locations);
            Container.BindInstance(units);
            Container.BindInstance(_expeditions);
            Container.BindInstance(_productions);
            Container.BindInstance(_order);
        }
    }
}