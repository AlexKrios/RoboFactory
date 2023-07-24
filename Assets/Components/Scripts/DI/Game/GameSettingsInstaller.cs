using RoboFactory.General.Asset;
using RoboFactory.General.Audio;
using RoboFactory.General.Expedition;
using RoboFactory.General.Item.Production;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Level;
using RoboFactory.General.Localization;
using RoboFactory.General.Location;
using RoboFactory.General.Order;
using RoboFactory.General.Scene;
using RoboFactory.General.Unit;
using UnityEngine;
using Zenject;

namespace RoboFactory.DI
{
    [CreateAssetMenu(fileName = "Game", menuName = "Scriptable/General/Settings", order = -1)]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private AddressableService.Settings _assets;
        [SerializeField] private SceneService.Settings _scene;
        [SerializeField] private LocalizationService.Settings _localization;
        [SerializeField] private AudioManager.Settings _audioData;
        [SerializeField] private LevelManager.Settings _level;
        [SerializeField] private RawManager.Settings _raw;
        [SerializeField] private ProductsManager.Settings _products;
        [SerializeField] private LocationManager.Settings _locations;
        [SerializeField] private UnitsManager.Settings _units;
        [SerializeField] private ExpeditionManager.Settings _expeditions;
        [SerializeField] private ProductionManager.Settings _productions;
        [SerializeField] private OrderManager.Settings _order;

        public override void InstallBindings()
        {
            Container.BindInstance(_assets);
            Container.BindInstance(_scene);
            Container.BindInstance(_localization);
            Container.BindInstance(_audioData);
            Container.BindInstance(_level);
            Container.BindInstance(_raw);
            Container.BindInstance(_products);
            Container.BindInstance(_locations);
            Container.BindInstance(_units);
            Container.BindInstance(_expeditions);
            Container.BindInstance(_productions);
            Container.BindInstance(_order);
        }
    }
}