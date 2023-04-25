using System;
using Modules.General.Asset;
using Modules.General.Location.Model;
using Modules.General.Ui.Common.Menu;
using UnityEngine;

namespace Modules.Factory.Menu.Expedition.Locations
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

        public async void SetData(LocationObject data)
        {
            Data = data;
            var sprite = await AssetsController.LoadAsset<Sprite>(data.IconRef);

            SetIconSprite(sprite);
        }
    }
}