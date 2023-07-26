using System;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Units
{
    public class EquipmentCellView : CellBase
    {
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ProductsService productsService;
        
        [Space]
        [SerializeField] private ProductGroup _equipmentType;
        
        [Space]
        [SerializeField] private Image _starImage;

        public ProductGroup EquipmentType => _equipmentType;
        
        public Action<EquipmentCellView> OnClickEvent { get; set; }
        
        private UnitsMenuView _menu;
        public ProductObject Data { get; private set; }

        public void Initialize()
        {
            _menu = _uiController.FindUi<UnitsMenuView>();
        }

        public void SetEquipmentData(ProductObject product)
        {
            Data = product;
            SetIconSprite(product.IconRef);

            _starImage.gameObject.SetActive(product.ProductType != 0);
        }

        public void ResetEquipmentData()
        {
            Data = productsService.GetDefaultProduct(_equipmentType, _menu.ActiveUnit.UnitType);
            SetIconSprite(Data.IconRef);

            _starImage.gameObject.SetActive(false);
        }
        
        protected override void Click()
        {
            base.Click();
            
            OnClickEvent?.Invoke(this);
        }
    }
}