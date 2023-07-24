using System;
using RoboFactory.General.Asset;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Units
{
    public class TabCellView : CellBase
    {
        [Inject] private readonly AddressableService _addressableService;

        [SerializeField] private UnitType _unitType;
        
        public Action<TabCellView, UnitType> OnTabClick { get; set; }

        protected override void Click()
        {
            base.Click();

            OnTabClick?.Invoke(this, _unitType);
        }

        public void SetTabData()
        {
            var iconRef = _addressableService.Assets.GetUnitIcon(_unitType);
            SetIconSprite(iconRef);
        }
    }
}