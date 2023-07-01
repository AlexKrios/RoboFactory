using RoboFactory.General.Asset;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Production Menu")]
    public class ProductionMenuView : MenuBase
    {
        #region Zenject
        
        [Inject] private readonly AssetsManager _assetsManager;
        [Inject] private readonly ProductionMenuFactory _productionMenuFactory;

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

        #region Variables

        public ProductGroup ActiveProductGroup { get; set; }
        public UnitType ActiveUnitType { get; set; }
        public int ActiveProductType { get; set; }
        public int ActiveStar { get; set; }
        public ProductObject ActiveProduct { get; set; }

        #endregion
        
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            upgrade.OnClickAsObservable().Subscribe(_ => OnUpgradeClick()).AddTo(Disposable);

            header.OnTabClickEvent += OnGroupTabClick;
            star.OnClickEvent += OnStarTabClick;
            tabs.OnTabClickEvent += OnTypeTabClick;
            products.OnProductClickEvent += OnProductsClick;
            parts.OnPartClickEvent += OnPartClick;
            create.OnClickEvent += OnCreateClick;
            
            ActiveProductGroup = ProductGroup.Weapon;
            ActiveUnitType = UnitType.Trooper;
            ActiveProductType = 1;
            ActiveStar = 1;
        }

        #endregion

        #region Click Handler

        private void OnGroupTabClick()
        {
            header.SetHeaderData();
            star.ResetStar();
            tabs.Initialize();
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
            header.SetHeaderData();
            star.ResetStar();
            products.CreateProductCells();
            parts.SetData();
            sidebar.SetData();
            create.SetState();
        }
        
        private void OnProductsClick()
        {
            header.SetHeaderData();
            star.ResetStar();
            parts.SetData();
            sidebar.SetData();
            create.SetState();
        }
        
        private void OnPartClick()
        {
            parts.SetData();
            sidebar.SetData();
            create.SetState();
        }

        private void OnCreateClick()
        {
            parts.SetData();
            create.SetState();
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();
            
            UiController.AddUi(this);
            
            header.Initialize();
            star.Initialize();
            tabs.Initialize();
            products.Initialize();
            parts.Initialize();
            sidebar.Initialize();
            create.Initialize();
        }

        private void OnUpgradeClick()
        {
            var canvasT = UiController.GetCanvas(CanvasType.Ui).transform;
            _productionMenuFactory.CreateUpgradePopup(canvasT);
        }

        protected override void Release()
        {
            _assetsManager.ReleaseAllAsset();
        }
    }
}
