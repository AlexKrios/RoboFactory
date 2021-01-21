﻿using System;
using Modules.General.Asset;
using Modules.General.Ui.Common.Menu;
using Modules.General.Unit.Models.Object;
using Modules.General.Unit.Models.Type;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Factory.Menu.Units.Roster
{
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("Scripts/Factory/Menu/Units/Roster Cell View")]
    public class RosterCellView : CellBase
    {
        #region Variables

        public Action<RosterCellView, UnitType> OnClickEvent { get; set; }
        
        public UnitObject UnitData { get; private set; }

        #endregion

        protected override void Click()
        {
            base.Click();
            
            OnClickEvent?.Invoke(this, UnitData.UnitType);
        }
        
        public async void SetProductData(UnitObject unit)
        {
            UnitData = unit;
            var sprite = await AssetsController.LoadAsset<Sprite>(unit.IconRef);

            SetIconSprite(sprite);
        }
    }
}