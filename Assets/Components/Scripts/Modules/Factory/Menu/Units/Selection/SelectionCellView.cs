using System;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui.Common;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace RoboFactory.Factory.Menu.Units
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("Scripts/Factory/Menu/Units/Selection/Selection Cell View")]
    public class SelectionCellView : CellBase
    {
        #region Components

        [Space]
        [SerializeField] private CanvasGroup canvasGroup;

        #endregion
        
        #region Variables

        public Action<SelectionCellView> OnEquipmentClick { get; set; }

        public ProductObject Data { get; private set; }

        #endregion

        protected override void Click()
        {
            base.Click();
            
            OnEquipmentClick?.Invoke(this);
        }
        
        public void SetEquipmentData(ProductObject product)
        {
            Data = product;
            SetIconSprite(product?.IconRef);
        }
        
        protected override void SetIconSprite(AssetReference spriteRef, bool active = true)
        {
            base.SetIconSprite(spriteRef, active);

            if (Data.ProductType != 0)
            {
                var isEmpty = Data.IsEmpty();
                canvasGroup.alpha = isEmpty ? 0.25f : 1f;
            }
            else
            {
                canvasGroup.alpha = 1f;
            }
        }
    }
}