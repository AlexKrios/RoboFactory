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
        
        [Inject] private readonly ProductionMenuFactory _productionMenuFactory;

        #endregion

        #region Components

        [SerializeField] private Button _upgrade;
        
        [Space]
        [SerializeField] private HeaderView _header;
        [SerializeField] private StarButtonView _star;
        [SerializeField] private TabsSectionView _tabs;
        [SerializeField] private ProductsSectionView _products;
        [SerializeField] private PartsSectionView _parts;
        [SerializeField] private SidebarView _sidebar;
        [SerializeField] private CreateButtonView _create;

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

            _upgrade.OnClickAsObservable().Subscribe(_ => OnUpgradeClick()).AddTo(Disposable);

            _header.OnTabClickEvent += OnGroupTabClick;
            _star.OnClickEvent += OnStarTabClick;
            _tabs.OnTabClickEvent += OnTypeTabClick;
            _products.OnProductClickEvent += OnProductsClick;
            _parts.OnPartClickEvent += OnPartClick;
            _create.OnClickEvent += OnCreateClick;
            
            ActiveProductGroup = ProductGroup.Weapon;
            ActiveUnitType = UnitType.Trooper;
            ActiveProductType = 1;
            ActiveStar = 1;
        }

        #endregion

        #region Click Handler

        private void OnGroupTabClick()
        {
            _header.SetHeaderData();
            _star.ResetStar();
            _tabs.Initialize();
            _products.CreateProductCells();
            _parts.SetData();
            _sidebar.SetData();
            _create.SetState();
        }
        
        private void OnStarTabClick()
        {
            _parts.SetData();
            _sidebar.SetData();
            _create.SetState();
        }
        
        private void OnTypeTabClick()
        {
            _header.SetHeaderData();
            _star.ResetStar();
            _products.CreateProductCells();
            _parts.SetData();
            _sidebar.SetData();
            _create.SetState();
        }
        
        private void OnProductsClick()
        {
            _header.SetHeaderData();
            _star.ResetStar();
            _parts.SetData();
            _sidebar.SetData();
            _create.SetState();
        }
        
        private void OnPartClick()
        {
            _parts.SetData();
            _sidebar.SetData();
            _create.SetState();
        }

        private void OnCreateClick()
        {
            _parts.SetData();
            _create.SetState();
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();
            
            UiController.AddUi(this);
            
            _header.Initialize();
            _star.Initialize();
            _tabs.Initialize();
            _products.Initialize();
            _parts.Initialize();
            _sidebar.Initialize();
            _create.Initialize();
        }

        private void OnUpgradeClick()
        {
            var canvasT = UiController.GetCanvas(CanvasType.Ui).transform;
            _productionMenuFactory.CreateUpgradePopup(canvasT);
        }
    }
}
