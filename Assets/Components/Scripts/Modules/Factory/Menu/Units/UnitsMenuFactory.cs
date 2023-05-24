using System;
using JetBrains.Annotations;
using RoboFactory.Factory.Menu.Units;
using RoboFactory.General.Asset;
using RoboFactory.General.Ui;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu
{
    [UsedImplicitly]
    public class UnitsMenuFactory
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
            _container.InstantiatePrefabForComponent<UnitsMenuView>(menu, canvasT);
        }
        
        public void CreateSelectionMenu(Transform parent)
        {
            var popup = _container.InstantiatePrefabForComponent<SelectionPopupView>(_settings.selectionPrefab, parent);
            popup.transform.localPosition = new Vector3(0, 0, -500);
        }
        
        public SelectionCellView CreateSelectionCell(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<SelectionCellView>(_settings.selectionCell, parent);
        }
        
        public RosterCellView CreateUnit(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<RosterCellView>(_settings.unitPrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            public AssetReference menuAsset;
            
            [Space]
            public GameObject buttonPrefab;
            
            [Space]
            public GameObject unitPrefab;
            
            [Space]
            public GameObject selectionPrefab;
            public GameObject selectionCell;
        }
    }
}
