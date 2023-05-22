using System;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using Components.Scripts.Modules.General.Unit.Type;
using Components.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Units.Tab
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Tab Cell View")]
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
        
        public Action<TabCellView, UnitType> OnTabClick { get; set; }

        #endregion

        protected override void Click()
        {
            base.Click();

            OnTabClick?.Invoke(this, unitType);
        }

        public void SetTabData()
        {
            var spriteRef = _iconUtil.GetUnitIcon(unitType);
            SetIconSprite(spriteRef);
        }
    }
}