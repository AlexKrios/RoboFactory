using System;
using RoboFactory.General.Location;
using RoboFactory.General.Ui.Common;
using UnityEngine;

namespace RoboFactory.Factory.Menu.Expedition
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Location Cell View")]
    public class LocationCellView : CellBase
    {
        #region Variables

        public Action<LocationCellView> OnClickEvent { get; set; }
        
        public LocationObject Data { get; private set; }

        #endregion

        protected override void Click()
        {
            base.Click();
            
            OnClickEvent?.Invoke(this);
        }

        public void SetData(LocationObject data)
        {
            Data = data;
            SetIconSprite(data.IconRef);
        }
    }
}