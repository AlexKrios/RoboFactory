using Components.Scripts.Modules.Factory.Menu.Storage.Header;
using Components.Scripts.Modules.Factory.Menu.Storage.Items;
using Components.Scripts.Modules.Factory.Menu.Storage.Sidebar;
using Components.Scripts.Modules.Factory.Menu.Storage.Tab;
using Components.Scripts.Modules.General.Item.Products.Object;
using Components.Scripts.Modules.General.Item.Products.Types;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using UnityEngine;

namespace Components.Scripts.Modules.Factory.Menu.Storage
{
    [AddComponentMenu("Scripts/Factory/Menu/Storage/Storage Menu")]
    public class StorageMenuView : MenuBase
    {
        #region Components
        
        [SerializeField] private HeaderView header;
        [SerializeField] private TabsSectionView tabs;
        [SerializeField] private ItemsSectionView items;
        [SerializeField] private SidebarView sidebar;
        
        #endregion

        #region Variables

        public ProductGroup ActiveProductGroup { get; set; }
        public ProductObject ActiveItem { get; set; }
        
        public bool IsItemEmpty => items.Items.Count == 0;
        public bool IsDefault { get; set; }

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            UiController.AddUi(this);

            header.OnToggleClickEvent += OnDefaultToggleClick;
            tabs.OnTabClickEvent += OnTabClick;
            items.OnTabClickEvent += OnItemClick;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            header.OnToggleClickEvent -= OnDefaultToggleClick;
            tabs.OnTabClickEvent -= OnTabClick;
            items.OnTabClickEvent -= OnItemClick;
        }

        #endregion
        
        private void OnDefaultToggleClick()
        {
            items.CreateItemCells();
            sidebar.SetData();
        }
        
        private void OnTabClick()
        {
            header.SetData();
            items.CreateItemCells();
            sidebar.SetData();
        }
        
        private void OnItemClick()
        {
            header.SetData();
            sidebar.SetData();
        }
    }
}
