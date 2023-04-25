using System;
using Modules.General.Asset;
using Modules.General.Ui.Common.Menu;
using Modules.General.Unit.Type;
using UnityEngine;
using Utils;
using Zenject;

namespace Modules.Factory.Menu.Production.Tab
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

        public async void SetTabData()
        {
            var spriteRef = _iconUtil.GetUnitIcon(unitType);
            var sprite = await AssetsController.LoadAsset<Sprite>(spriteRef);
            
            SetIconSprite(sprite);
        }
    }
}