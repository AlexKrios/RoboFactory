using System;
using RoboFactory.General.Asset;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    public class TabCellView : CellBase
    {
        [Inject] private readonly AddressableService _addressableService;

        [SerializeField] private UnitType _unitType;
        
        public Action<TabCellView, UnitType> OnClickEvent { get; set; }

        protected override void Click()
        {
            base.Click();
            
            OnClickEvent?.Invoke(this, _unitType);
        }

        public void SetTabData()
        {
            var iconRef = _addressableService.Assets.GetUnitIcon(_unitType);
            SetIconSprite(iconRef);
        }

        protected override async void SetIconSprite(AssetReference spriteRef, bool active = true)
        {
            _icon.sprite = await _addressableService.LoadAssetAsync<Sprite>(spriteRef);
        }
    }
}