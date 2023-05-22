using System;
using Components.Scripts.Modules.General.Item.Products.Types;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using Components.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Production.Header
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Header Tab Cell View")]
    public class HeaderTabCellView : CellBase
    {
        #region Zenject

        [Inject(Id = "IconUtil")] private readonly IconUtil _iconUtil;

        #endregion

        #region Components
        
        [SerializeField] private ProductGroup group;
        public ProductGroup Group => group;

        #endregion
        
        #region Variables

        public Action<HeaderTabCellView, ProductGroup> OnClickEvent { get; set; }

        #endregion

        protected override void Click()
        {
            base.Click();
            
            OnClickEvent?.Invoke(this, group);
        }

        public void SetTabData(ProductGroup type)
        {
            var spriteRef = _iconUtil.GetProductGroupIcon(type);
            SetIconSprite(spriteRef);
        }
    }
}