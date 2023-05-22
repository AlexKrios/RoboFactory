using System;
using Components.Scripts.Modules.General.Item.Products.Object;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using UnityEngine;

namespace Components.Scripts.Modules.Factory.Menu.Production.Products
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