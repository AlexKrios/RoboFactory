using System;
using JetBrains.Annotations;
using RoboFactory.General.Asset;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Order
{
    [UsedImplicitly]
    public class OrderMenuFactory
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
            var menuOriginal = await _addressableService.InstantiateAssetAsync(_settings.MenuAsset, _screensParent);
            var menu = _container.InjectGameObjectForComponent<OrderMenuView>(menuOriginal);
            menu.Initialize();
            
            _addressableService.ReleaseAsset(_settings.MenuAsset);
        }
        
        public ItemCellView CreateItem(Transform parent)
        { 
            return _container.InstantiatePrefabForComponent<ItemCellView>(_settings.ItemPrefab, parent);
        }
        
        public void CreateUpgradePopup(Transform parent)
        {
            _container.InstantiatePrefabForComponent<UpgradePopupView>(_settings.UpgradePrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            public GameObject _buttonPrefab;

            [Space]
            public AssetReference _menuAsset;

            [Space]
            public GameObject _itemPrefab;
            
            [Space]
            public GameObject _upgradePrefab;
            
            public GameObject ButtonPrefab => _buttonPrefab;
            public AssetReference MenuAsset => _menuAsset;
            public GameObject ItemPrefab => _itemPrefab;
            public GameObject UpgradePrefab => _upgradePrefab;
        }
    }
}
