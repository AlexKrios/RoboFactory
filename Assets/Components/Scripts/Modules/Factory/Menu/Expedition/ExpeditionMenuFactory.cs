﻿using System;
using JetBrains.Annotations;
using RoboFactory.General.Asset;
using RoboFactory.General.Expedition;
using RoboFactory.General.Ui;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    [UsedImplicitly]
    public class ExpeditionMenuFactory
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
            var menu = _container.InjectGameObjectForComponent<ExpeditionMenuView>(menuOriginal);
            menu.Initialize();
            
            _assetsManager.ReleaseAsset(_settings.menuAsset);
        }
        
        public LocationCellView CreateLocationCell(Transform parent)
        { 
            return _container.InstantiatePrefabForComponent<LocationCellView>(_settings.locationPrefab, parent);
        }
        
        public void CreateSelectionMenu(Transform parent)
        {
            var popup = _container.InstantiatePrefabForComponent<SelectionPopupView>(_settings.selectionPopupPrefab, parent);
            popup.transform.localPosition = new Vector3(0, 0, -500);
        }
        
        public SelectionCellView CreateSelectionCell(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<SelectionCellView>(_settings.selectionCellPrefab, parent);
        }
        
        public void CreateUpgradeQueuePopup(Transform parent)
        {
            _container.InstantiatePrefabForComponent<UpgradeQueuePopupView>(_settings.queuePrefab, parent);
        }
        
        public ResultPopupView CreateResultPopup(Transform parent, ExpeditionObject expedition)
        {
            var popup = _container.InstantiatePrefabForComponent<ResultPopupView>(_settings.resultPopupPrefab, parent);
            popup.SetData(expedition);
            return popup;
        }
        public ResultRewardCellView CreateResultRewardCell(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<ResultRewardCellView>(_settings.resultRewardPrefab, parent);
        }
        public ResultUnitCellView CreateResultUnitCell(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<ResultUnitCellView>(_settings.resultUnitPrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            public AssetReference menuAsset;

            [Space]
            public GameObject buttonPrefab;
            
            [Space]
            public GameObject locationPrefab;

            [Space]
            public GameObject selectionPopupPrefab;
            public GameObject selectionCellPrefab;
            
            [Space]
            public GameObject queuePrefab;
            
            [Space]
            public GameObject resultPopupPrefab;
            public GameObject resultRewardPrefab;
            public GameObject resultUnitPrefab;
        }
    }
}
