using System;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Ui.Common.Menu;
using UnityEngine;
using Utils;
using Zenject;

namespace Modules.Factory.Menu.Production.Header
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Header Tab Cell View")]
    public class HeaderTabCellView : CellBase
    {
        #region Zenject

        [Inject(Id = "IconUtil")] private readonly IconUtil _iconUtil;

        #endregion

        #region Components
        
        [SerializeField] private ProductGroup group;

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
            var sprite = _iconUtil.GetProductGroupIcon(type);

            SetIconSprite(sprite);
        }
    }
}