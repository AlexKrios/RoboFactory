using RoboFactory.General.Asset;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui.Common;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Storage
{
    [AddComponentMenu("Scripts/Factory/Menu/Storage/Storage Menu")]
    public class StorageMenuView : MenuBase
    {
        #region Zenject

        [Inject] private readonly AssetsManager _assetsManager;

        #endregion
        
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

        public override void Initialize()
        {
            base.Initialize();
            
            UiController.AddUi(this);
            
            header.Initialize();
            tabs.Initialize();
            items.Initialize();
            sidebar.Initialize();
        }

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
        
        protected override void Release()
        {
            _assetsManager.ReleaseAllAsset();
        }
    }
}
