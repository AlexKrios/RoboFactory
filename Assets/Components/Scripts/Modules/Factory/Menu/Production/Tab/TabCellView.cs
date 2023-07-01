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

        [Inject] private readonly AssetsManager _assetsManager;
        [Inject(Id = "IconUtil")] private readonly IconUtil _iconUtil;

        #endregion

        #region Components

        [SerializeField] private UnitType unitType;
        public UnitType UnitType => unitType;

        #endregion
        
        #region Variables

        public Action<TabCellView, UnitType> OnClickEvent { get; set; }
        
        #endregion

        protected override void Click()
        {
            base.Click();
            
            OnClickEvent?.Invoke(this, unitType);
        }

        public void SetTabData()
        {
            var iconRef = _iconUtil.GetUnitIcon(unitType);
            SetIconSprite(iconRef);
        }

        protected override async void SetIconSprite(AssetReference spriteRef, bool active = true)
        {
            icon.sprite = await _assetsManager.LoadAssetAsync<Sprite>(spriteRef);
        }
    }
}