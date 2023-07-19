using System;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Ui.Common;
using UnityEngine;

namespace RoboFactory.Factory.Menu.Conversion
{
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Tab Cell View")]
    public class TabCellView : CellBase
    {
        #region Components

        [Space]
        [SerializeField] private RawType _rawType;
        
        public RawType RawType => _rawType;

        #endregion
        
        #region Variables

        public Action<TabCellView> OnTabClick { get; set; }

        #endregion

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