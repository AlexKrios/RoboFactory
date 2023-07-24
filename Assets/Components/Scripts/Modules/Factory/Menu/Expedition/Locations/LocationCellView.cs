using System;
using RoboFactory.General.Location;
using RoboFactory.General.Ui.Common;

namespace RoboFactory.Factory.Menu.Expedition
{
    public class LocationCellView : CellBase
    {
        public Action<LocationCellView> OnClickEvent { get; set; }
        
        public LocationObject Data { get; private set; }

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