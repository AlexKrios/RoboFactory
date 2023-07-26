using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Profile;
using RoboFactory.General.Services;
using UnityEngine;
using Zenject;

namespace RoboFactory.General.Location
{
    [UsedImplicitly]
    public class LocationsService : Service
    {
        [Inject] private readonly Settings _settings;
        [Inject] private readonly CommonProfile _commonProfile;
        
        public override ServiceTypeEnum ServiceType => ServiceTypeEnum.NeedAuth;
        
        private readonly Dictionary<string, LocationObject> _locationDictionary = new();
        
        protected override UniTask InitializeAsync()
        {
            foreach (var data in _settings.Locations)
            {
                var location = new LocationObject().SetStartData(data);
                AddLocation(location);
            }
            
            var locationsData = _commonProfile.UserProfile.LocationsSection;
            if (locationsData == null) return UniTask.CompletedTask;
            
            foreach (var location in locationsData.Locations)
            {
                _locationDictionary[location.Key].Level = location.Value.Level;
            }
            
            return UniTask.CompletedTask;
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
            [SerializeField] private List<LocationScriptable> _locations;
            
            public List<LocationScriptable> Locations => _locations;
        }
    }
}