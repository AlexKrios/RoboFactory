using System;
using JetBrains.Annotations;
using RoboFactory.General.Asset;
using RoboFactory.General.Expedition;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    [UsedImplicitly]
    public class ExpeditionMenuFactory
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
            var menu = _container.InjectGameObjectForComponent<ExpeditionMenuView>(menuOriginal);
            menu.Initialize();
            
            _addressableService.ReleaseAsset(_settings.MenuAsset);
        }
        
        public LocationCellView CreateLocationCell(Transform parent)
        { 
            return _container.InstantiatePrefabForComponent<LocationCellView>(_settings.LocationPrefab, parent);
        }
        
        public void CreateSelectionMenu(Transform parent)
        {
            var popup = _container.InstantiatePrefabForComponent<SelectionPopupView>(_settings.SelectionPopupPrefab, parent);
            popup.transform.localPosition = new Vector3(0, 0, -500);
        }
        
        public SelectionCellView CreateSelectionCell(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<SelectionCellView>(_settings.SelectionCellPrefab, parent);
        }
        
        public void CreateUpgradeQueuePopup(Transform parent)
        {
            _container.InstantiatePrefabForComponent<UpgradeQueuePopupView>(_settings.QueuePrefab, parent);
        }
        
        public ResultPopupView CreateResultPopup(Transform parent, ExpeditionObject expedition)
        {
            var popup = _container.InstantiatePrefabForComponent<ResultPopupView>(_settings.ResultPopupPrefab, parent);
            popup.SetData(expedition);
            return popup;
        }
        public ResultRewardCellView CreateResultRewardCell(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<ResultRewardCellView>(_settings.ResultRewardPrefab, parent);
        }
        public ResultUnitCellView CreateResultUnitCell(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<ResultUnitCellView>(_settings.ResultUnitPrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private GameObject _buttonPrefab;
            
            [Space]
            [SerializeField] private AssetReference _menuAsset;

            [Space]
            [SerializeField] private GameObject _locationPrefab;

            [Space]
            [SerializeField] private GameObject _selectionPopupPrefab;
            [SerializeField] private GameObject _selectionCellPrefab;
            
            [Space]
            [SerializeField] private GameObject _queuePrefab;
            
            [Space]
            [SerializeField] private GameObject _resultPopupPrefab;
            [SerializeField] private GameObject _resultRewardPrefab;
            [SerializeField] private GameObject _resultUnitPrefab;
            
            public GameObject ButtonPrefab => _buttonPrefab;
            public AssetReference MenuAsset => _menuAsset;
            public GameObject LocationPrefab => _locationPrefab;
            public GameObject SelectionPopupPrefab => _selectionPopupPrefab;
            public GameObject SelectionCellPrefab => _selectionCellPrefab;
            public GameObject QueuePrefab => _queuePrefab;
            public GameObject ResultPopupPrefab => _resultPopupPrefab;
            public GameObject ResultRewardPrefab => _resultRewardPrefab;
            public GameObject ResultUnitPrefab => _resultUnitPrefab;
        }
    }
}
