using System;
using Modules.General.Asset;
using Modules.General.Item.Products.Models.Object;
using Modules.General.Ui.Common.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Factory.Menu.Units.Selection
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
        
        public async void SetEquipmentData(ProductObject product)
        {
            Data = product;
            var sprite = await AssetsController.LoadAsset<Sprite>(product?.IconRef);
            
            SetIconSprite(sprite);
        }
        
        protected override void SetIconSprite(Sprite sprite, bool active = true)
        {
            base.SetIconSprite(sprite, active);
            
            var isEmpty = Data.IsEmpty();
            canvasGroup.alpha = isEmpty ? 0.25f : 1f;
        }
    }
}