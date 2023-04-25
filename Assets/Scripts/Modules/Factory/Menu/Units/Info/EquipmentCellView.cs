using System;
using Modules.General.Asset;
using Modules.General.Item.Products;
using Modules.General.Item.Products.Models.Object;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Ui;
using Modules.General.Ui.Common.Menu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Units.Info
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Equipment Cell View")]
    public class EquipmentCellView : CellBase
    {
        #region Zenject

        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly IProductsController _productsController;

        #endregion
        
        #region Components

        [Space]
        [SerializeField] private ProductGroup equipmentType;
        
        [Space]
        [SerializeField] private Image starImage;

        public ProductGroup EquipmentType => equipmentType;

        #endregion
        
        #region Variables

        public Action<EquipmentCellView> OnClickEvent { get; set; }
        
        private UnitsMenuView _menu;
        public ProductObject Data { get; private set; }

        #endregion
        
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            _menu = _uiController.FindUi<UnitsMenuView>();
        }

        #endregion

        public async void SetEquipmentData(ProductObject product)
        {
            Data = product;

            var sprite = await AssetsController.LoadAsset<Sprite>(product.IconRef);
            SetIconSprite(sprite);

            starImage.gameObject.SetActive(product.ProductType != 0);
        }

        public async void ResetEquipmentData()
        {
            Data = _productsController.GetDefaultProduct(equipmentType, _menu.ActiveUnit.UnitType);

            var sprite = await AssetsController.LoadAsset<Sprite>(Data.IconRef);
            SetIconSprite(sprite);

            starImage.gameObject.SetActive(false);
        }
        
        protected override void Click()
        {
            base.Click();
            
            OnClickEvent?.Invoke(this);
        }
    }
}