using System;
using JetBrains.Annotations;
using RoboFactory.General.Asset;
using RoboFactory.General.Ui;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Storage
{
    [UsedImplicitly]
    public class StorageMenuFactory
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
            _container.InstantiatePrefabForComponent<StorageMenuView>(menu, canvasT);
        }
        
        public ItemCellView CreateItem(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<ItemCellView>(_settings.itemPrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            public AssetReference menuAsset;

            [Space]
            public GameObject buttonPrefab;
            
            [Space]
            public GameObject itemPrefab;
        }
    }
}
