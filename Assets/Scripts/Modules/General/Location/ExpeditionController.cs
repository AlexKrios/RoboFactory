using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Location.Model;
using Modules.General.Scriptable;
using Zenject;

namespace Modules.General.Location
{
    public class ExpeditionController : IExpeditionController
    {
        #region Zenject
        
        [Inject] private readonly Settings _settings;

        #endregion
        
        #region Variables

        public Action OnExpeditionComplete { get; set; }
        
        private readonly Dictionary<string, LocationScriptable> _locationDictionary;
        private readonly List<ExpeditionObject> _expeditionData;
        public LocationScriptable CurrentBattleLocation { get; set; }
        
        public int CellCount { get; set; }

        #endregion

        public ExpeditionController(Settings settings)
        {
            _locationDictionary = new Dictionary<string, LocationScriptable>();
            _expeditionData = new List<ExpeditionObject>();
            
            foreach (var locationData in settings.locations)
            {
                _locationDictionary.Add(locationData.Key, locationData);
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

        public List<LocationScriptable> GetLocations() => _locationDictionary.Values.ToList();
        public LocationScriptable GetLocation(string key) => _locationDictionary[key];
        
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