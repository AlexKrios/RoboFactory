using System;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui.Common;
using RoboFactory.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace RoboFactory.Factory.Menu.Storage
{
    [AddComponentMenu("Scripts/Factory/Menu/Storage/Tab Cell View")]
    public class TabCellView : CellBase
    {
        #region Zenject
        
        [Inject(Id = "IconUtil")] private readonly IconUtil _iconUtil;
        
        #endregion

        #region Components
        
        [SerializeField] private ProductGroup group;

        #endregion
        
        #region Variables

        public Action<TabCellView, ProductGroup> OnClickEvent { get; set; }
        
        private AssetReference _iconRef;

        #endregion

        protected override void Click()
        {
            base.Click();
            
            OnClickEvent?.Invoke(this, group);
        }

        public void SetTabData(ProductGroup type)
        {
            var iconRef = _iconUtil.GetProductGroupIcon(type);
            SetIconSprite(iconRef);
        }
    }
}