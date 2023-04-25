using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Ui;
using Modules.General.Unit.Type;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Production.Tab
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Tabs Section View")]
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

        private ProductionMenuView _menu;
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
            _menu = _uiController.FindUi<ProductionMenuView>();
            
            SetData();
        }

        #endregion

        public void SetData()
        {
            tabs.ForEach(x =>
            {
                x.SetTabData();
                x.OnClickEvent += OnTabClick;
            });

            ActiveTab = tabs.First();
        }
        
        private void OnTabClick(TabCellView tab, UnitType unit)
        {
            if (ActiveTab == tab)
                return;

            _menu.ActiveUnitType = unit;
            _menu.ActiveProductType = 1;

            ActiveTab = tab;

            OnTabClickEvent?.Invoke();
        }
    }
}