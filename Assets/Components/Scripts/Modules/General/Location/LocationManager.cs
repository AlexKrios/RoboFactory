using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace RoboFactory.General.Location
{
    [UsedImplicitly]
    public class LocationManager
    {
        #region Variables

        private readonly Dictionary<string, LocationObject> _locationDictionary;

        #endregion

        public LocationManager(Settings settings)
        {
            _locationDictionary = new Dictionary<string, LocationObject>();
            foreach (var locationData in settings.locations)
            {
                var locationObject = new LocationObject().SetStartData(locationData);
                AddLocation(locationObject);
            }
        }
        
        public void LoadData(LocationSectionDto data)
        {
            if (data == null)
                return;
            
            foreach (var location in data.Locations)
            {
                _locationDictionary[location.Key].Level = location.Value.level;
            }
        }
        
        public List<LocationObject> GetLocations() => _locationDictionary.Values.ToList();
        public LocationObject GetLocation(string key) => _locationDictionary[key];
        private void AddLocation(LocationObject data)
        {
            _locationDictionary.Add(data.Key, data);
        }

        [Serializable]
        public class Settings
        {
            public List<LocationScriptable> locations;
        }
    }
}