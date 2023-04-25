using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Modules.General.Location.Model;
using Modules.General.Scriptable;
using Zenject;

namespace Modules.General.Location
{
    [UsedImplicitly]
    public class ExpeditionController : IExpeditionController
    {
        #region Zenject
        
        [Inject] private readonly Settings _settings;

        #endregion
        
        #region Variables

        public Action OnExpeditionComplete { get; set; }
        
        private readonly Dictionary<string, LocationObject> _locationDictionary;
        private readonly List<ExpeditionObject> _expeditionData;
        public LocationObject CurrentBattleLocation { get; set; }
        
        public int CellCount { get; set; }

        #endregion

        public ExpeditionController(Settings settings)
        {
            _locationDictionary = new Dictionary<string, LocationObject>();
            _expeditionData = new List<ExpeditionObject>();
            
            foreach (var locationData in settings.locations)
            {
                var locationObject = new LocationBuilder().Create(locationData);
                _locationDictionary.Add(locationObject.Key, locationObject);
            }
            
            CellCount = 1;
        }
        
        public void LoadStoreData(ExpeditionsLoadObject data)
        {
            CellCount = data.count;
            foreach (var expeditionObj in data.expeditions)
            {
                var expedition = new ExpeditionObject
                {
                    Id = expeditionObj.id,
                    Key = expeditionObj.key,
                    Star = expeditionObj.star,
                    Units = expeditionObj.units,
                    TimeEnd = expeditionObj.timeEnd
                };
                AddExpedition(expedition);
            }
        }
        
        public bool IsHaveFreeCell()
        {
            return _expeditionData.Count < CellCount;
        }

        public List<LocationObject> GetLocations() => _locationDictionary.Values.ToList();
        public LocationObject GetLocation(string key) => _locationDictionary[key];
        
        public void AddExpedition(ExpeditionObject data)
        {
            _expeditionData.Add(data);
        }
        public List<ExpeditionObject> GetAllExpeditions() => _expeditionData;
        public ExpeditionObject GetExpedition(Guid id) => _expeditionData.First(x => x.Id == id);
        public void RemoveExpedition(Guid id)
        {
            var expedition = _expeditionData.First(x => x.Id == id);
            _expeditionData.Remove(expedition);
            //OnExpeditionComplete?.Invoke();
        }
        public UpgradeDataObject GetUpgradeData()
        {
            return _settings.upgradeData.Data.First(x => x.Count == CellCount);
        }

        [Serializable]
        public class Settings
        {
            public List<LocationScriptable> locations;
            public UpgradeDataScriptable upgradeData;
        }
    }
}