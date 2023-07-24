using System;
using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Ui;
using RoboFactory.General.Unit;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    public class TabsSectionView : MonoBehaviour
    {
        [Inject] private readonly IUiController _uiController;

        [SerializeField] private List<TabCellView> _tabs;
        
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

        public void Initialize()
        {
            _menu = _uiController.FindUi<ProductionMenuView>();
            
            _tabs.ForEach(x =>
            {
                x.SetTabData();
                x.OnClickEvent += OnTabClick;
            });

            ActiveTab = _tabs.First();
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