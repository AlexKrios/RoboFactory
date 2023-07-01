using System;
using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Ui;
using RoboFactory.General.Unit;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Units
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Tabs Section View")]
    public class TabsSectionView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly IUiController _uiController;

        #endregion

        #region Components
        
        [SerializeField] private List<TabCellView> tabs;

        #endregion
        
        #region Variables

        public Action OnTabClickEvent { get; set; }

        private UnitsMenuView _menu;
        private TabCellView _activeTab;
        private TabCellView ActiveTab
        {
            get => _activeTab;
            set
            {
                if (_activeTab != null)
                    _activeTab.SetInactive();
                
                _activeTab = value;
                _activeTab.SetActive();
            }
        }

        #endregion

        public void Initialize()
        {
            _menu = _uiController.FindUi<UnitsMenuView>();
            
            SetTabsData();
        }

        private void SetTabsData()
        {
            tabs.ForEach(x =>
            {
                x.OnTabClick += OnTabClick;
                x.SetTabData();
            });

            ActiveTab = tabs.First();
        }
        
        private void OnTabClick(TabCellView tab, UnitType unit)
        {
            if (ActiveTab == tab)
                return;
            
            _menu.ActiveUnitType = unit;
            ActiveTab = tab;
            
            OnTabClickEvent?.Invoke();
        }
    }
}