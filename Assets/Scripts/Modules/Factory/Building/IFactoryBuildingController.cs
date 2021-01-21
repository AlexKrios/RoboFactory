using System.Collections.Generic;
using Modules.Factory.Building.Floors;

namespace Modules.Factory.Building
{
    public interface IFactoryBuildingController
    {
        Dictionary<int, BuildingFloorBase> FloorsDictionary { get; }
        BuildingFloorBase ActiveFloor { get; set; }

        void Init();
        void SetBuildingVisibility();
    }
}