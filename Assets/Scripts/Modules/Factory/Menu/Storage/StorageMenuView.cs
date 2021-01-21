using Modules.Factory.Menu.Storage.Header;
using Modules.Factory.Menu.Storage.Items;
using Modules.Factory.Menu.Storage.Sidebar;
using Modules.Factory.Menu.Storage.Tab;
using Modules.General.Ui.Common.Menu;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Storage
{
    [AddComponentMenu("Scripts/Factory/Menu/Storage/Storage Menu")]
    public class StorageMenuView : MenuBase
    {
        #region Zenject

        [Inject] private readonly StorageMenuManager _storageMenuManager;

        #endregion

        #region Components
        
        [SerializeField] private HeaderView header;
        [SerializeField] private TabsSectionView tabs;
        [SerializeField] private ItemsSectionView items;
        [SerializeField] private SidebarView sidebar;
        
        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            header.OnToggleClickEvent += OnDefaultToggleClick;
            tabs.OnTabClickEvent += OnTabClick;
            items.OnTabClickEvent += OnItemClick;
        }

        private void OnDestroy()
        {
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

        public override void Close()
        {
            base.Close();
            
            _storageMenuManager.Reset();
        }
    }
}
