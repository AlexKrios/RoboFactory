using System;
using System.Collections.Generic;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace RoboFactory.Factory.Menu.Expedition
{
    public class UnitCellView : CellBase
    {
        [Space]
        [SerializeField] private Image _emptyIcon;
        
        [Space]
        [SerializeField] private List<AttackType> _attackTypes;
        
        [Space]
        [SerializeField] private GameObject _cellView;
        [SerializeField] private GameObject _unitParent;

        public List<AttackType> AttackTypes => _attackTypes;
        public GameObject UnitParent => _unitParent;

        public Action<UnitCellView> OnClickEvent { get; set; }
        
        public UnitObject Data { get; private set; }

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
            _cellView.SetActive(value);
        }

        private void SetEmptyIconActive(bool value)
        {
            _emptyIcon.gameObject.SetActive(value);
        }
    }
}
