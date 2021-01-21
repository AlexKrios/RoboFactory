using System.Collections.Generic;
using Modules.Factory.Building.Floors;

namespace Modules.Factory.Building
{
    public class FactoryBuildingController : IFactoryBuildingController
    {
        public Dictionary<int, BuildingFloorBase> FloorsDictionary { get; }
        public BuildingFloorBase ActiveFloor { get; set; }
        
        public FactoryBuildingController()
        {
            FloorsDictionary = new Dictionary<int, BuildingFloorBase>();
        }

        public void Init()
        {
            ActiveFloor = FloorsDictionary[0];
            SetBuildingVisibility();
        }

        public void SetBuildingVisibility()
        {
            for (var i = 0; i < FloorsDictionary.Count; i++)
            {
                FloorsDictionary[i].SetFloorVisible(ActiveFloor.Floor >= i);
            }

            ActiveFloor.SetFloorActive();
            ActiveFloor.SetMenuButtonsActive();
        }
    }
}