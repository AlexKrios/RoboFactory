using System;
using System.Collections.Generic;
using System.Linq;
using Components.Scripts.Modules.General.Item.Products.Types;
using Components.Scripts.Modules.General.Ui;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Storage.Tab
{
    [AddComponentMenu("Scripts/Factory/Menu/Storage/Tabs Section View")]
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

        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            _menu = _uiController.FindUi<StorageMenuView>();
            
            SetTabsData();
        }

        #endregion

        private void SetTabsData()
        {
            var productGroupCount = Enum.GetValues(typeof(ProductGroup)).Length - 1;
            for (var i = 0; i < productGroupCount; i++)
            {
                tabs[i].SetTabData((ProductGroup) i);
                tabs[i].OnClickEvent += OnTabClick;
            }

            ActiveTab = tabs.First();
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