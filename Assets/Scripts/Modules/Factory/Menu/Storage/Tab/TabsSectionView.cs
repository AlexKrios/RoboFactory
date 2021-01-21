using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Item.Products.Models.Types;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Storage.Tab
{
    [AddComponentMenu("Scripts/Factory/Menu/Storage/Tabs Section View")]
    public class TabsSectionView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly StorageMenuManager _storageMenuManager;
        
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
            _storageMenuManager.Tabs = this;

            SetTabsData();
        }

        #endregion

        private void SetTabsData()
        {
            var productGroupCount = Enum.GetValues(typeof(ProductGroup)).Length;
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
            
            _storageMenuManager.ActiveProductGroup = group;
            ActiveTab = tab;
            
            OnTabClickEvent?.Invoke();
        }
    }
}