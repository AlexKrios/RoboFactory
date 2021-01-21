using Modules.Factory.Menu.Production.Header;
using Modules.Factory.Menu.Production.Parts;
using Modules.Factory.Menu.Production.Products;
using Modules.Factory.Menu.Production.Sidebar;
using Modules.Factory.Menu.Production.Tab;
using Modules.General.Save;
using Modules.General.Ui.Common.Menu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Production
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Production Menu")]
    public class ProductionMenuView : MenuBase
    {
        #region Zenject
        
        [Inject] private readonly ISaveController _saveController;
        [Inject] private readonly ProductionMenuFactory _productionMenuFactory;
        [Inject] private readonly ProductionMenuManager _productionMenuManager;
        [Inject(Id = "PopupCanvas")] private readonly RectTransform _popupCanvas;

        #endregion

        #region Components

        [SerializeField] private Button upgrade;
        
        [Space]
        [SerializeField] private HeaderView header;
        [SerializeField] private StarButtonView star;
        [SerializeField] private TabsSectionView tabs;
        [SerializeField] private ProductsSectionView products;
        [SerializeField] private PartsSectionView parts;
        [SerializeField] private SidebarView sidebar;
        [SerializeField] private CreateButtonView create;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            _productionMenuManager.Menu = this;
            
            base.Awake();
            
            upgrade.onClick.AddListener(OnUpgradeClick);

            header.OnTabClickEvent += OnGroupTabClick;
            star.OnClickEvent += OnStarTabClick;
            tabs.OnTabClickEvent += OnTypeTabClick;
            products.OnProductClickEvent += OnProductsClick;
            parts.OnPartClickEvent += OnPartClick;
            create.OnClickEvent += OnCreateClick;
        }
        
        #endregion

        #region Click Handler

        private void OnGroupTabClick()
        {
            header.SetData();
            star.ResetStar();
            tabs.SetData();
            products.CreateProductCells();
            parts.SetData();
            sidebar.SetData();
            create.SetState();
        }
        
        private void OnStarTabClick()
        {
            parts.SetData();
            sidebar.SetData();
            create.SetState();
        }
        
        private void OnTypeTabClick()
        {
            header.SetData();
            star.ResetStar();
            products.CreateProductCells();
            parts.SetData();
            sidebar.SetData();
            create.SetState();
        }
        
        private void OnProductsClick()
        {
            header.SetData();
            star.ResetStar();
            parts.SetData();
            sidebar.SetData();
            create.SetState();
        }
        
        private void OnPartClick()
        {
            parts.SetComponent();
            sidebar.SetData();
            create.SetState();
        }

        private void OnCreateClick()
        {
            parts.SetComponent();
            create.SetState();
            _saveController.SaveProduction(true);
        }

        #endregion

        private void OnUpgradeClick()
        {
            _productionMenuFactory.CreateUpgradePopup(_popupCanvas);
        }
        
        public override void Close()
        {
            base.Close();
            
            _productionMenuManager.Reset();
        }
    }
}
