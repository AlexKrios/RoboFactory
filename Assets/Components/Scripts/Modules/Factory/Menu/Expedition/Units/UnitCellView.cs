using System;
using System.Collections.Generic;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace RoboFactory.Factory.Menu.Expedition
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
