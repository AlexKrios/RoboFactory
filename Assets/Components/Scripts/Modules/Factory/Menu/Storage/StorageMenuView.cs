using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui.Common;
using UnityEngine;

namespace RoboFactory.Factory.Menu.Storage
{
    [AddComponentMenu("Scripts/Factory/Menu/Storage/Storage Menu")]
    public class StorageMenuView : MenuBase
    {
        #region Components
        
        [Space]
        [SerializeField] private HeaderView _header;
        [SerializeField] private TabsSectionView _tabs;
        [SerializeField] private ItemsSectionView _items;
        [SerializeField] private SidebarView _sidebar;
        
        #endregion

        #region Variables

        public ProductGroup ActiveProductGroup { get; set; }
        public ProductObject ActiveItem { get; set; }
        
        public bool IsItemEmpty => _items.Items.Count == 0;
        public bool IsDefault { get; set; }

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            _header.OnToggleClickEvent += OnDefaultToggleClick;
            _tabs.OnTabClickEvent += OnTabClick;
            _items.OnTabClickEvent += OnItemClick;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            _header.OnToggleClickEvent -= OnDefaultToggleClick;
            _tabs.OnTabClickEvent -= OnTabClick;
            _items.OnTabClickEvent -= OnItemClick;
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();
            
            UiController.AddUi(this);
            
            _header.Initialize();
            _tabs.Initialize();
            _items.Initialize();
            _sidebar.Initialize();
        }

        private void OnDefaultToggleClick()
        {
            _items.CreateItemCells();
            _sidebar.SetData();
        }
        
        private void OnTabClick()
        {
            _header.SetData();
            _items.CreateItemCells();
            _sidebar.SetData();
        }
        
        private void OnItemClick()
        {
            _header.SetData();
            _sidebar.SetData();
        }
    }
}
