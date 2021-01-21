using System;
using Modules.General.Asset;
using Modules.General.Item.Products.Models.Object;
using Modules.General.Ui.Common.Menu;
using UnityEngine;

namespace Modules.Factory.Menu.Production.Products
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Product Cell View")]
    public class ProductCellView : CellBase
    {
        #region Variables

        public Action<ProductCellView, int> OnClickEvent { get; set; }
        
        public ProductObject Data { get; private set; }

        #endregion
        
        protected override void Click()
        {
            base.Click();
            
            OnClickEvent?.Invoke(this, Data.ProductType);
        }

        public async void SetProductData(ProductObject item)
        {
            Data = item;
            var sprite = await AssetsController.LoadAsset<Sprite>(item.IconRef);

            SetIconSprite(sprite);
        }
    }
}