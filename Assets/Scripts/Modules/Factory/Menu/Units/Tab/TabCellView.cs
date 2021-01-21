using System;
using Modules.General.Ui.Common.Menu;
using Modules.General.Unit.Models.Type;
using UnityEngine;
using Utils;
using Zenject;

namespace Modules.Factory.Menu.Units.Tab
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Tab Cell View")]
    public class TabCellView : CellBase
    {
        #region Zenject
        
        [Inject(Id = "IconUtil")] private readonly IconUtil _iconUtil;

        #endregion

        #region Components
        
        [SerializeField] private UnitType unitType;

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
            var sprite = _iconUtil.GetUnitIcon(unitType);

            SetIconSprite(sprite);
        }
    }
}