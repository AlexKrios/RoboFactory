using System;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Ui.Common;
using UnityEngine;

namespace RoboFactory.Factory.Menu.Conversion
{
    public class TabCellView : CellBase
    {
        [Space]
        [SerializeField] private RawType _rawType;
        
        public RawType RawType => _rawType;
        
        public Action<TabCellView> OnTabClick { get; set; }

        protected override void Click()
        {
            base.Click();
            OnTabClick?.Invoke(this);
        }

        public void SetTabData(RawObject data)
        {
            SetIconSprite(data.IconRef);
        }
    }
}