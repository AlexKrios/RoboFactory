using System;
using JetBrains.Annotations;
using Modules.Factory.Menu.Expedition.Locations;
using Modules.Factory.Menu.Expedition.Queue.Upgrade;
using Modules.Factory.Menu.Expedition.Result;
using Modules.Factory.Menu.Expedition.Selection;
using Modules.General.Location.Model;
using Modules.General.Ui;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Expedition
{
    [UsedImplicitly]
    public class ExpeditionMenuFactory
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
        
        public void CreateMenu()
        { 
            var canvasT = _uiController.GetCanvas(CanvasType.Ui).transform;
            _container.InstantiatePrefabForComponent<ExpeditionMenuView>(_settings.menuPrefab, canvasT);
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
            public GameObject menuPrefab;
            
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
