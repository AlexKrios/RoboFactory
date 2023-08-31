using System;
using JetBrains.Annotations;
using RoboFactory.Factory.Menu.Units;
using RoboFactory.General.Asset;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu
{
    [UsedImplicitly]
    public class UnitsMenuFactory
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
            var menu = _container.InjectGameObjectForComponent<UnitsMenuView>(menuOriginal);
            menu.Initialize();
            
            //_addressableService.ReleaseAsset(_settings.MenuAsset);
        }
        
        public void CreateSelectionMenu(Transform parent)
        {
            var popup = _container.InstantiatePrefabForComponent<SelectionPopupView>(_settings.SelectionPrefab, parent);
            popup.transform.localPosition = new Vector3(0, 0, -500);
        }
        
        public SelectionCellView CreateSelectionCell(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<SelectionCellView>(_settings.SelectionCell, parent);
        }
        
        public RosterCellView CreateUnit(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<RosterCellView>(_settings.UnitPrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private GameObject _buttonPrefab;
            
            [Space]
            [SerializeField] private AssetReference _menuAsset;

            [Space]
            [SerializeField] private GameObject _unitPrefab;
            
            [Space]
            [SerializeField] private GameObject _selectionPrefab;
            [SerializeField] private GameObject _selectionCell;
            
            public GameObject ButtonPrefab => _buttonPrefab;
            public AssetReference MenuAsset => _menuAsset;
            public GameObject UnitPrefab => _unitPrefab;
            public GameObject SelectionPrefab => _selectionPrefab;
            public GameObject SelectionCell => _selectionCell;
        }
    }
}
