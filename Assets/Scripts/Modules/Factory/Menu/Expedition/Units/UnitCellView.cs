using System;
using System.Collections.Generic;
using Modules.General.Asset;
using Modules.General.Ui.Common.Menu;
using Modules.General.Unit.Models.Object;
using Modules.General.Unit.Models.Type;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Factory.Menu.Expedition.Units
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

        public async void SetData(UnitObject unit)
        {
            Data = unit;
            var sprite = await AssetsController.LoadAsset<Sprite>(unit?.IconRef);
            
            SetIconSprite(sprite, unit != null);
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
