using System;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui.Common;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace RoboFactory.Factory.Menu.Units
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(CanvasGroup))]
    public class SelectionCellView : CellBase
    {
        public Action<SelectionCellView> OnEquipmentClick { get; set; }

        public ProductObject Data { get; private set; }

        private CanvasGroup _canvasGroup;

        protected override void Awake()
        {
            base.Awake();

            _canvasGroup = GetComponent<CanvasGroup>();
        }

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
                _canvasGroup.alpha = isEmpty ? 0.25f : 1f;
            }
            else
            {
                _canvasGroup.alpha = 1f;
            }
        }
    }
}