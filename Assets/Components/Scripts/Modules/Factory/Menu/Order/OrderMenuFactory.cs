using System;
using Components.Scripts.Modules.Factory.Menu.Order.Components;
using Components.Scripts.Modules.Factory.Menu.Order.Upgrade;
using Components.Scripts.Modules.General.Asset;
using Components.Scripts.Modules.General.Ui;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Order
{
    [UsedImplicitly]
    public class OrderMenuFactory
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly IUiController _uiController;

        #endregion
        
        public Button CreateButton(Transform parent)
        { 
            return _container.InstantiatePrefabForComponent<Button>(_settings.buttonPrefab, parent);
        }
        
        public async void CreateMenu()
        { 
            var menu = await AssetsManager.LoadAsset<GameObject>(_settings.menuAsset);
            var canvasT = _uiController.GetCanvas(CanvasType.Ui).transform;
            _container.InstantiatePrefabForComponent<OrderMenuView>(menu, canvasT);
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
