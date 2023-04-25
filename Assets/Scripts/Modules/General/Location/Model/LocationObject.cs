using System.Collections.Generic;
using Modules.General.Item.Models.Recipe;
using UnityEngine.AddressableAssets;

namespace Modules.General.Location.Model
{
    public class LocationObject
    {
        public string Key { get; set; }
        public int Time { get; set; }
        public AssetReference IconRef { get; set; }
        public List<LocationUnitData> Enemies { get; set; }
        public List<PartObject> Reward { get; set; }
    }
}