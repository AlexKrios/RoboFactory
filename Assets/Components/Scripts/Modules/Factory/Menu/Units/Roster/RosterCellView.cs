using System;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using Components.Scripts.Modules.General.Unit.Object;
using Components.Scripts.Modules.General.Unit.Type;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Scripts.Modules.Factory.Menu.Units.Roster
{
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("Scripts/Factory/Menu/Units/Roster Cell View")]
    public class RosterCellView : CellBase
    {
        #region Variables

        public Action<RosterCellView, UnitType> OnClickEvent { get; set; }
        
        public UnitObject Data { get; private set; }

        #endregion

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