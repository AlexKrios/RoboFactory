using System;
using System.Collections.Generic;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using Components.Scripts.Modules.General.Unit.Object;
using Components.Scripts.Modules.General.Unit.Type;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Scripts.Modules.Factory.Menu.Expedition.Units
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Unit Cell View")]
    public class UnitCellView : CellBase
    {
        #region Components
        
        [SerializeField] private Image emptyIcon;
        
        [Space]
        [SerializeField] private List<AttackType> attackTypes;
        
        [Space]
        [SerializeField] private GameObject cellView;
        [SerializeField] private GameObject unitParent;

        public List<AttackType> AttackTypes => attackTypes;
        public GameObject UnitParent => unitParent;

        #endregion

        #region Variables

        public Action<UnitCellView> OnClickEvent { get; set; }
        
        public UnitObject Data { get; private set; }

        #endregion

        public void SetData(UnitObject unit)
        {
            Data = unit;
            
            SetIconSprite(unit?.IconRef, unit != null);
            SetEmptyIconActive(unit == null);
        }

        public void ResetData()
        {
            Data = null;
            
            SetIconSprite(null, false); 
            SetEmptyIconActive(true);
        }

        protected override void Click()
        {
            base.Click();

            OnClickEvent?.Invoke(this);
        }

        public void ActivateCell(bool value)
        {
            cellView.SetActive(value);
        }

        private void SetEmptyIconActive(bool value)
        {
            emptyIcon.gameObject.SetActive(value);
        }
    }
}
