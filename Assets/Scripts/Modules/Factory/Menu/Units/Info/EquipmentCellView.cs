using System;
using Modules.General.Asset;
using Modules.General.Item.Products;
using Modules.General.Item.Products.Models.Object;
using Modules.General.Item.Products.Models.Types;
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

        [Inject] private readonly IProductsController _productsController;
        [Inject] private readonly UnitsMenuManager _unitsMenuManager;

        #endregion
        
        #region Components

        [Space]
        [SerializeField] private ProductGroup equipmentType;
        
        [Space]
        [SerializeField] private Image emptyIcon;
        [SerializeField] private Image plusIcon;
        [SerializeField] private Image starImage;

        public ProductGroup EquipmentType => equipmentType;

        #endregion
        
        #region Variables

        public Action<EquipmentCellView> OnClickEvent { get; set; }
        
        public ProductObject Data { get; private set; }

        #endregion

        public async void SetEquipmentData(ProductObject product)
        {
            Data = product;

            var sprite = await AssetsController.LoadAsset<Sprite>(product.IconRef);
            SetIconSprite(sprite);
            SetEmptyIcon(null, false);

            starImage.gameObject.SetActive(true);
            plusIcon.gameObject.SetActive(false);
        }

        public async void ResetEquipmentData()
        {
            var unitType = _unitsMenuManager.Roster.ActiveUnit.UnitData.UnitType;
            Data = _productsController.GetDefaultProduct(equipmentType, unitType);

            var sprite = await AssetsController.LoadAsset<Sprite>(Data.IconRef);
            SetIconSprite(null, false);
            SetEmptyIcon(sprite, true);
            
            starImage.gameObject.SetActive(false);
            plusIcon.gameObject.SetActive(true);
        }

        private void SetEmptyIcon(Sprite sprite, bool active)
        {
            emptyIcon.gameObject.SetActive(active);
            emptyIcon.sprite = sprite;
        }

        protected override void Click()
        {
            base.Click();
            
            OnClickEvent?.Invoke(this);
        }
    }
}