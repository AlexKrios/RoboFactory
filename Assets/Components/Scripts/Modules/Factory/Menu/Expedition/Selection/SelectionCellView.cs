using System;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using Components.Scripts.Modules.General.Unit.Object;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Scripts.Modules.Factory.Menu.Expedition.Selection
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Selection/Selection Cell View")]
    public class SelectionCellView : CellBase
    {
        #region Variables

        public Action<SelectionCellView> OnUnitClick { get; set; }

        public UnitObject Data { get; private set; }

        #endregion

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