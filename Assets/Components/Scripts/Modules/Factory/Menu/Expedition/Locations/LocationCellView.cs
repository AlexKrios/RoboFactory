using System;
using Components.Scripts.Modules.General.Location;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using UnityEngine;

namespace Components.Scripts.Modules.Factory.Menu.Expedition.Locations
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