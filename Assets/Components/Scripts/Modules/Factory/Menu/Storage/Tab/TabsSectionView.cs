using System;
using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Storage
{
    public class TabsSectionView : MonoBehaviour
    {
        [Inject] private readonly IUiController _uiController;

        [SerializeField] private List<TabCellView> _tabs;
        
        public Action OnTabClickEvent { get; set; }

        private StorageMenuView _menu;
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
            _menu = _uiController.FindUi<StorageMenuView>();
            
            SetTabsData();
        }

        private void SetTabsData()
        {
            var productGroupCount = Enum.GetValues(typeof(ProductGroup)).Length - 1;
            for (var i = 0; i < productGroupCount; i++)
            {
                _tabs[i].SetTabData((ProductGroup) i);
                _tabs[i].OnClickEvent += OnTabClick;
            }

            ActiveTab = _tabs.First();
        }
        
        private void OnTabClick(TabCellView tab, ProductGroup @group)
        {
            if (ActiveTab == tab)
                return;
            
            _menu.ActiveProductGroup = group;
            ActiveTab = tab;
            
            OnTabClickEvent?.Invoke();
        }
    }
}