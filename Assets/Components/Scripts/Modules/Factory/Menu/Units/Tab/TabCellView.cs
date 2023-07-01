using System;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using RoboFactory.Utils;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Units
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
            var iconRef = _iconUtil.GetUnitIcon(unitType);
            SetIconSprite(iconRef);
        }
    }
}