using System;
using System.Collections.Generic;
using System.Linq;
using Components.Scripts.Modules.Factory.Api;
using Components.Scripts.Modules.General.Item.Models;
using Components.Scripts.Modules.General.Item.Models.Recipe;
using Components.Scripts.Modules.General.Item.Models.Types;
using Components.Scripts.Modules.General.Item.Raw.Object;
using Components.Scripts.Modules.General.Item.Raw.Scriptable;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Zenject;

namespace Components.Scripts.Modules.General.Item.Raw
{
    [UsedImplicitly]
    public class RawManager : IItemManager
    {
        [Inject] private readonly ApiManager _apiManager;

        public ItemType ItemType { get; }
        
        private readonly Settings _settings;
        private readonly Dictionary<string, RawObject> _rawDictionary;

        public Action OnRawSet { get; set; }
        
        public RawManager(Settings settings)
        {
            ItemType = ItemType.Raw;

            _settings = settings;
            _rawDictionary = new Dictionary<string, RawObject>();
            
            foreach (var rawData in _settings.data)
            {
                var rawObj = new RawObject().SetInitData(rawData);
                _rawDictionary.Add(rawObj.Key, rawObj);
            }
        }
        
        public void LoadData(Dictionary<string, RawDto> rawData)
        {
            if (rawData == null)
                return;
            
            foreach (var raw in rawData)
            {
                _rawDictionary[raw.Key].Count = raw.Value.count;
                _rawDictionary[raw.Key].Level = raw.Value.level;
                _rawDictionary[raw.Key].Settings = _settings.settings.Settings[raw.Value.level];
            }
            
            OnRawSet?.Invoke();
        }

        public List<RawObject> GetAllRaw() => _rawDictionary.Values.ToList();
        public ItemBase GetItem(string key) => _rawDictionary[key];
        public RawObject GetRaw(string key) => _rawDictionary[key];

        public Dictionary<string, RawDto> GetAllRawDto()
        {
            var rawDto = new Dictionary<string, RawDto>();
            _rawDictionary.ToList().ForEach(x => rawDto.Add(x.Key, x.Value.ToDto()));
            return rawDto;
        } 
        
        public List<RawObject> GetRawByType(RawType type)
        {
            return _rawDictionary.Values.Where(x => x.RawType == type).ToList();
        }
        public List<RawObject> GetMainRaw()
        {
            return _rawDictionary.Values.Where(x => x.IsRefill).ToList();
        }

        public async UniTask AddItem(string key, int count = 1)
        {
            var raw = _rawDictionary[key];
            raw.IncrementCount(count);
            await _apiManager.SetUserRawSingle(key, raw.ToDto());
            
            OnRawSet?.Invoke();
        }

        public async UniTask AddItemsThenSend(List<PartObject> data)
        {
            data.ForEach(x => _rawDictionary[x.data.Key].IncrementCount(x.count));
            await SendRawOnServer();
            
            OnRawSet?.Invoke();
        }
        
        public async UniTask RemoveItem(string key, int count = 1)
        {
            var raw = _rawDictionary[key];
            raw.DecrementCount(count);
            await _apiManager.SetUserRawSingle(key, raw.ToDto());
            
            OnRawSet?.Invoke();
        }

        private async UniTask SendRawOnServer()
        {
            var rawData = GetAllRawDto();
            await _apiManager.SetUserRaw(rawData);
        }

        public bool CheckIfRawStoreFull(string key)
        {
            return _rawDictionary[key].Count >= _rawDictionary[key].Settings.cap;
        }

        public async void SetMaxRaw()
        {
            foreach (var rawObject in _rawDictionary.Values)
            {
                rawObject.Count = 25 /*rawObject.Settings.cap*/;
            }

            await SendRawOnServer();
        }

        public async void SetMinRaw()
        {
            foreach (var rawObject in _rawDictionary.Values)
            {
                rawObject.Count = 0;
            }

            await SendRawOnServer();
        }

        [Serializable]
        public class Settings
        {
            public List<RawScriptable> data;
            public RawSettingsScriptable settings;
        }
    }
}
