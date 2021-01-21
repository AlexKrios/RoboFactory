using System;
using JetBrains.Annotations;
using Modules.Factory.Menu.Units.Roster;
using Modules.Factory.Menu.Units.Selection;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Units
{
    [UsedImplicitly]
    public class UnitsMenuFactory
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject(Id = "MenuCanvas")] private readonly RectTransform _menuCanvas;

        #endregion                

        public Button CreateButton(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<Button>(_settings.buttonPrefab, parent);
        }
        
        public void CreateMenu()
        {
            _container.InstantiatePrefabForComponent<UnitsMenuView>(_settings.menuPrefab, _menuCanvas);
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
            public GameObject menuPrefab;
            
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
