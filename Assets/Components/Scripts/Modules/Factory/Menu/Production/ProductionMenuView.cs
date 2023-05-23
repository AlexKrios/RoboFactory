﻿using Components.Scripts.Modules.Factory.Menu.Production.Header;
using Components.Scripts.Modules.Factory.Menu.Production.Parts;
using Components.Scripts.Modules.Factory.Menu.Production.Products;
using Components.Scripts.Modules.Factory.Menu.Production.Sidebar;
using Components.Scripts.Modules.Factory.Menu.Production.Tab;
using Components.Scripts.Modules.General.Item.Products.Object;
using Components.Scripts.Modules.General.Item.Products.Types;
using Components.Scripts.Modules.General.Ui;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using Components.Scripts.Modules.General.Unit.Type;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Production
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Production Menu")]
    public class ProductionMenuView : MenuBase
    {
        #region Zenject
        
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

            UiController.AddUi(this);

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

        private void OnUpgradeClick()
        {
            var canvasT = UiController.GetCanvas(CanvasType.Ui).transform;
            _productionMenuFactory.CreateUpgradePopup(canvasT);
        }
    }
}