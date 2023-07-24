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
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly AddressableService _addressableService;
        [Inject] private readonly IUiController _uiController;            

        public Button CreateButton(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<Button>(_settings.ButtonPrefab, parent);
        }
        
        public async void CreateMenu()
        {
            var canvasT = _uiController.GetCanvas(CanvasType.Ui).transform;
            var menuOriginal = await _addressableService.InstantiateAssetAsync(_settings.MenuAsset, canvasT);
            var menu = _container.InjectGameObjectForComponent<StorageMenuView>(menuOriginal);
            menu.Initialize();
            
            _addressableService.ReleaseAsset(_settings.MenuAsset);
        }
        
        public ItemCellView CreateItem(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<ItemCellView>(_settings.ItemPrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private GameObject _buttonPrefab;
            
            [Space]
            [SerializeField] private AssetReference _menuAsset;

            [Space]
            [SerializeField] private GameObject _itemPrefab;
            
            public GameObject ButtonPrefab => _buttonPrefab;
            public AssetReference MenuAsset => _menuAsset;
            public GameObject ItemPrefab => _itemPrefab;
        }
    }
}
