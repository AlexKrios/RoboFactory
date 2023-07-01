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
            var menuOriginal = await _assetsManager.InstantiateAssetAsync(_settings.menuAsset, canvasT);
            var menu = _container.InjectGameObjectForComponent<OrderMenuView>(menuOriginal);
            menu.Initialize();
            
            _assetsManager.ReleaseAsset(_settings.menuAsset);
        }
        
        public ItemCellView CreateItem(Transform parent)
        { 
            return _container.InstantiatePrefabForComponent<ItemCellView>(_settings.itemPrefab, parent);
        }
        
        public void CreateUpgradePopup(Transform parent)
        {
            _container.InstantiatePrefabForComponent<UpgradePopupView>(_settings.upgradePrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            public AssetReference menuAsset;

            [Space]
            public GameObject buttonPrefab;
            
            [Space]
            public GameObject itemPrefab;
            
            [Space]
            public GameObject upgradePrefab;
        }
    }
}
