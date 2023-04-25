using System;
using Modules.General.Asset;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Ui.Common.Menu;
using UnityEngine;
using Utils;
using Zenject;

namespace Modules.Factory.Menu.Storage.Tab
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

        #endregion

        protected override void Click()
        {
            base.Click();
            
            OnClickEvent?.Invoke(this, group);
        }

        public async void SetTabData(ProductGroup type)
        {
            var spriteRef = _iconUtil.GetProductGroupIcon(type);
            var sprite = await AssetsController.LoadAsset<Sprite>(spriteRef);

            SetIconSprite(sprite);
        }
    }
}