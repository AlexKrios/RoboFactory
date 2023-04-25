using System;
using Modules.General.Asset;
using Modules.General.Ui.Common.Menu;
using Modules.General.Unit.Object;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Factory.Menu.Expedition.Selection
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
        
        public async void SetUnitData(UnitObject unit)
        {
            Data = unit;
            var sprite = await AssetsController.LoadAsset<Sprite>(unit.IconRef);
            
            SetIconSprite(sprite);
        }
    }
}