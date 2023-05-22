using System;
using Components.Scripts.Modules.General.Asset;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using Components.Scripts.Modules.General.Unit.Type;
using Components.Scripts.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Production.Tab
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Tab Cell View")]
    public class TabCellView : CellBase
    {
        #region Zenject

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
            var spriteRef = _iconUtil.GetUnitIcon(unitType);
            SetIconSprite(spriteRef);
        }

        protected override async void SetIconSprite(AssetReference spriteRef, bool active = true)
        {
            icon.sprite = await AssetsManager.LoadAsset<Sprite>(spriteRef);
        }
    }
}