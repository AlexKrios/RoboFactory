using System;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui.Common;
using UnityEngine;

namespace RoboFactory.Factory.Menu.Production
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

        public void SetProductData(ProductObject item)
        {
            Data = item;
            SetIconSprite(item.IconRef);
        }
    }
}