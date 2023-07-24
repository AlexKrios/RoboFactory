using System;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace RoboFactory.Factory.Menu.Units
{
    [RequireComponent(typeof(Button))]
    public class RosterCellView : CellBase
    {
        public Action<RosterCellView, UnitType> OnClickEvent { get; set; }
        
        public UnitObject Data { get; private set; }

        protected override void Click()
        {
            base.Click();
            
            OnClickEvent?.Invoke(this, Data.UnitType);
        }
        
        public void SetProductData(UnitObject unit)
        {
            Data = unit;
            SetIconSprite(unit.IconRef);
        }
    }
}