using System;
using Modules.General.Ui.Common.Menu;
using Modules.General.Unit.Models.Type;
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

        public void SetTabData()
        {
            var sprite = _iconUtil.GetUnitIcon(unitType);

            SetIconSprite(sprite);
        }
    }
}