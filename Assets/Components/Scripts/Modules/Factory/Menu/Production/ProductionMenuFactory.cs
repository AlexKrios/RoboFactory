using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Asset;
using RoboFactory.General.Ui;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    [UsedImplicitly]
    public class ProductionMenuFactory
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly AssetsManager _assetsManager;
        [Inject] private readonly IUiController _uiController;

        #endregion

        public Button CreateButton(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<Button>(_settings.buttonPrefab, parent);
        }
        
        public async void CreateMenu()
        {
            var canvasT = _uiController.GetCanvas(CanvasType.Ui).transform;
            var menuOriginal = await _assetsManager.InstantiateAssetAsync(_settings.menuReference, canvasT);
            var menu = _container.InjectGameObjectForComponent<ProductionMenuView>(menuOriginal);
            menu.Initialize();
            
            _assetsManager.ReleaseAsset(_settings.menuReference);
        }
        
        public ProductCellView CreateProduct(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<ProductCellView>(_settings.productReference, parent);
        }
        
        public void CreateUpgradePopup(Transform parent)
        {
            _container.InstantiatePrefabForComponent<UpgradePopupView>(_settings.upgradeReference, parent);
        }
        
        public void CreateUpgradeQueuePopup(Transform parent)
        {
            _container.InstantiatePrefabForComponent<UpgradeQueuePopupView>(_settings.queueReference, parent);
        }

        [Serializable]
        public class Settings
        {
            public GameObject buttonPrefab;
            
            [Space]
            public AssetReference menuReference;

            [Space]
            public GameObject productReference;
            
            [Space]
            public GameObject upgradeReference;
            
            [Space]
            public GameObject queueReference;
        }
    }
}
