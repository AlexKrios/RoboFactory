using System;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace RoboFactory.Factory.Menu.Expedition
{
    [RequireComponent(typeof(Image))]
    public class SelectionCellView : CellBase
    {
        public Action<SelectionCellView> OnUnitClick { get; set; }

        public UnitObject Data { get; private set; }

        protected override void Click()
        {
            base.Click();
            
            OnUnitClick?.Invoke(this);
        }
        
        public void SetUnitData(UnitObject unit)
        {
            Data = unit;
            SetIconSprite(unit.IconRef);
        }
    }
}