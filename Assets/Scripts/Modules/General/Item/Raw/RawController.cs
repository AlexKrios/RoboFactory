using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Item.Models;
using Modules.General.Item.Models.Recipe;
using Modules.General.Item.Models.Types;
using Modules.General.Item.Raw.Models.Load;
using Modules.General.Item.Raw.Models.Object;
using Modules.General.Item.Raw.Models.Scriptable;
using Modules.General.Item.Raw.Models.Type;
using Modules.General.Save;
using Zenject;

namespace Modules.General.Item.Raw
{
    public class RawController : IRawController, IGetItem
    {
        [Inject] private readonly ISaveController _saveController;

        public ItemType ItemType { get; }
        
        private readonly Settings _settings;
        private readonly Dictionary<string, RawObject> _rawDictionary;

        public Action OnRawSet { get; set; }
        
        public RawController(Settings settings)
        {
            ItemType = ItemType.Raw;

            _settings = settings;
            _rawDictionary = new Dictionary<string, RawObject>();

            var builder = new RawBuilder();
            foreach (var fileData in _settings.data)
            {
                var rawObj = builder.Create(fileData);
                _rawDictionary.Add(rawObj.Key, rawObj);
            }
        }

        public ItemBase GetItem(string key) => _rawDictionary[key];
        public RawObject GetRaw(string key) => _rawDictionary[key];
        public List<RawObject> GetAllRaw() => _rawDictionary.Values.ToList();
        
        public List<RawObject> GetRawByType(RawType type)
        {
            return _rawDictionary.Values.Where(x => x.RawType == type).ToList();
        }
        public List<RawObject> GetMainRaw()
        {
            return _rawDictionary.Values.Where(x => x.IsRefill).ToList();
        }
        
        public void LoadRawData(List<RawLoadObject> rawData)
        {
            foreach (var raw in rawData)
            {
                _rawDictionary[raw.key].Count = raw.count;
                _rawDictionary[raw.key].Level = raw.level;
                _rawDictionary[raw.key].Settings = _settings.settings.Settings[raw.level];

                OnRawSet?.Invoke();
            }
        }
        
        public void AddRaw(PartObject part)
        {
            _rawDictionary[part.data.Key].IncrementCount(part.count);
            _saveController.SaveStores();
            
            OnRawSet?.Invoke();
        }
        public void AddRaw(string key, int star, int count)
        {
            _rawDictionary[key].IncrementCount(count);
            _saveController.SaveStores();
            
            OnRawSet?.Invoke();
        }

        public bool CheckIfRawStoreFull(string key, int star)
        {
            return _rawDictionary[key].Count >= _rawDictionary[key].Settings.cap;
        }

        public void SetMaxRaw()
        {
            foreach (var rawObject in _rawDictionary.Values)
            {
                rawObject.Count = rawObject.Settings.cap;
            }
        }

        public void SetMinRaw()
        {
            foreach (var rawObject in _rawDictionary.Values)
            {
                rawObject.Count = 0;
            }
        }
        
        [Serializable]
        public class Settings
        {
            public List<RawScriptable> data;
            public RawSettingsScriptable settings;
        }
    }
}
