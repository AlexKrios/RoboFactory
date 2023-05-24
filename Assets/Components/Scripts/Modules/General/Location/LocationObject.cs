using System.Collections.Generic;
using RoboFactory.General.Item;
using UnityEngine.AddressableAssets;

namespace RoboFactory.General.Location
{
    public class LocationObject
    {
        public string Key { get; set; }
        public int Time { get; set; }
        public AssetReference IconRef { get; set; }
        public List<LocationUnitData> Enemies { get; set; }
        public List<PartObject> Reward { get; set; }
        
        public int Level { get; set; }

        public LocationObject SetStartData(LocationScriptable data)
        {
            Key = data.Key;
            Time = data.Time;
            IconRef = data.IconRef;
            Enemies = data.Enemies;
            Reward = data.Reward;

            return this;
        }
    }
}