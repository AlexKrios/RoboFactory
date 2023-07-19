using System;
using RoboFactory.General.Asset;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using RoboFactory.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Tab Cell View")]
    public class TabCellView : CellBase
    {
        #region Zenject

        [Inject] private readonly AddressableService addressableService;
        [Inject(Id = "IconUtil")] private readonly IconUtil _iconUtil;

        #endregion

        #region Components

        [SerializeField] private UnitType _unitType;

        #endregion
        
        #region Variables

        public Action<TabCellView, UnitType> OnClickEvent { get; set; }
        
        #endregion

        protected override void Click()
        {
            base.Click();
            
            OnClickEvent?.Invoke(this, _unitType);
        }

        public void SetTabData()
        {
            var iconRef = _iconUtil.GetUnitIcon(_unitType);
            SetIconSprite(iconRef);
        }

        protected override async void SetIconSprite(AssetReference spriteRef, bool active = true)
        {
            icon.sprite = await addressableService.LoadAssetAsync<Sprite>(spriteRef);
        }
    }
}