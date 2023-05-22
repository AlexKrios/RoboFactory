using System;
using Components.Scripts.Modules.General.Item.Raw;
using Components.Scripts.Modules.General.Item.Raw.Object;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using UnityEngine;

namespace Components.Scripts.Modules.Factory.Menu.Conversion.Tabs
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