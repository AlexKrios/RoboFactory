using System;
using Components.Scripts.Modules.General.Item.Products.Object;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Components.Scripts.Modules.Factory.Menu.Units.Selection
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