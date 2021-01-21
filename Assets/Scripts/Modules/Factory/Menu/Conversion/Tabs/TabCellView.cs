using System;
using Modules.General.Asset;
using Modules.General.Item.Raw.Models.Object;
using Modules.General.Item.Raw.Models.Type;
using Modules.General.Ui.Common.Menu;
using UnityEngine;

namespace Modules.Factory.Menu.Conversion.Tabs
{
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Tab Cell View")]
    public class TabCellView : CellBase
    {
        #region Components

        [Space]
        [SerializeField] private RawType rawType;
        
        public RawType RawType => rawType;

        #endregion
        
        #region Variables

        public Action<TabCellView> OnTabClick { get; set; }

        public RawObject RawData { get; private set; }
        
        #endregion

        protected override void Click()
        {
            base.Click();
            OnTabClick?.Invoke(this);
        }

        public async void SetTabData(RawObject data)
        {
            RawData = data;
            var sprite = await AssetsController.LoadAsset<Sprite>(data.IconRef);

            SetIconSprite(sprite);
        }
    }
}