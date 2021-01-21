using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Unit.Models.Type;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Units.Tab
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Tabs Section View")]
    public class TabsSectionView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly UnitsMenuManager _unitsMenuManager;
        
        #endregion

        #region Components
        
        [SerializeField] private List<TabCellView> tabs;

        #endregion
        
        #region Variables

        public Action OnTabClickEvent { get; set; }

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
        
        #region Unity Methods

        private void Awake()
        {
            _unitsMenuManager.Tabs = this;

            SetTabsData();
        }

        #endregion

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
            
            _unitsMenuManager.ActiveUnitType = unit;
            ActiveTab = tab;
            
            OnTabClickEvent?.Invoke();
        }
    }
}