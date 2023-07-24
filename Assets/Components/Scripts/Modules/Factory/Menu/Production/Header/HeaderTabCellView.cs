using System;
using RoboFactory.General.Asset;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui.Common;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    public class HeaderTabCellView : CellBase
    {
        [Inject] private readonly AddressableService _addressableService;

        [SerializeField] private ProductGroup _group;
        
        public Action<HeaderTabCellView, ProductGroup> OnClickEvent { get; set; }

        protected override void Click()
        {
            base.Click();
            
            OnClickEvent?.Invoke(this, _group);
        }

        public void SetTabData(ProductGroup type)
        {
            var iconRef = _addressableService.Assets.GetProductGroupIcon(type);
            SetIconSprite(iconRef);
        }
    }
}