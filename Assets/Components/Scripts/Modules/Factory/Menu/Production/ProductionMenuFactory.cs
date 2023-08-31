using System;
using JetBrains.Annotations;
using RoboFactory.General.Asset;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    [UsedImplicitly]
    public class ProductionMenuFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly AddressableService _addressableService;
        [Inject(Id = Constants.ScreensParentKey)] private readonly Transform _screensParent;

        public Button CreateButton(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<Button>(_settings.ButtonPrefab, parent);
        }
        
        public async void CreateMenu()
        {
            var menuOriginal = await _addressableService.InstantiateAssetAsync(_settings.MenuReference, _screensParent);
            var menu = _container.InjectGameObjectForComponent<ProductionMenuView>(menuOriginal);
            menu.Initialize();
        }
        
        public ProductCellView CreateProduct(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<ProductCellView>(_settings.ProductPrefab, parent);
        }
        
        public async void CreateUpgradePopup(Transform parent)
        {
            var popupOriginal = await _addressableService.InstantiateAssetAsync(_settings.UpgradeReference, parent);
            var popup = _container.InjectGameObjectForComponent<UpgradePopupView>(popupOriginal);
            popup.Initialize();
        }
        
        public async void CreateUpgradeQueuePopup(Transform parent)
        {
            var popupOriginal = await _addressableService.InstantiateAssetAsync(_settings.QueueReference, parent);
            var popup = _container.InjectGameObjectForComponent<UpgradeQueuePopupView>(popupOriginal);
            popup.Initialize();
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private GameObject _buttonPrefab;
            
            [Space]
            [SerializeField] private AssetReference _menuReference;

            [Space]
            [SerializeField] private GameObject _productPrefab;
            
            [Space]
            [SerializeField] private AssetReference _upgradeReference;
            
            [Space]
            [SerializeField] private AssetReference _queueReference;
            
            public GameObject ButtonPrefab => _buttonPrefab;
            public AssetReference MenuReference => _menuReference;
            public GameObject ProductPrefab => _productPrefab;
            public AssetReference UpgradeReference => _upgradeReference;
            public AssetReference QueueReference => _queueReference;
        }
    }
}
