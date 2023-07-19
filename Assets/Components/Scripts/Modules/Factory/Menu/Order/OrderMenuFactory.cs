using System;
using JetBrains.Annotations;
using RoboFactory.General.Asset;
using RoboFactory.General.Ui;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Order
{
    [UsedImplicitly]
    public class OrderMenuFactory
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly AddressableService addressableService;
        [Inject] private readonly IUiController _uiController;

        #endregion
        
        public Button CreateButton(Transform parent)
        { 
            return _container.InstantiatePrefabForComponent<Button>(_settings.ButtonPrefab, parent);
        }
        
        public async void CreateMenu()
        { 
            var canvasT = _uiController.GetCanvas(CanvasType.Ui).transform;
            var menuOriginal = await addressableService.InstantiateAssetAsync(_settings.MenuAsset, canvasT);
            var menu = _container.InjectGameObjectForComponent<OrderMenuView>(menuOriginal);
            menu.Initialize();
            
            addressableService.ReleaseAsset(_settings.MenuAsset);
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
