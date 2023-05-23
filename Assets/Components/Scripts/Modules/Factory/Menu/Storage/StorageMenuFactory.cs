﻿using System;
using Components.Scripts.Modules.Factory.Menu.Storage.Items;
using Components.Scripts.Modules.General.Asset;
using Components.Scripts.Modules.General.Ui;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Storage
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