using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Api;
using UnityEngine;
using Zenject;

namespace RoboFactory.General.Item.Raw
{
    [UsedImplicitly]
    public class RawManager : IItemManager
    {
        [Inject] private readonly ApiService apiService;

        public ItemType ItemType { get; }
        
        private readonly Dictionary<string, RawObject> _rawDictionary;

        public Action OnRawSet { get; set; }
        
        public RawManager(Settings settings)
        {
            ItemType = ItemType.Raw;
            _rawDictionary = new Dictionary<string, RawObject>();
            
            foreach (var rawData in settings.Data)
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
            await apiService.SetUserRawSingle(key, raw.ToDto());
            
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
            await apiService.SetUserRawSingle(key, raw.ToDto());
            
            OnRawSet?.Invoke();
        }

        private async UniTask SendRawOnServer()
        {
            var rawData = GetAllRawDto();
            await apiService.SetUserRaw(rawData);
        }

        public bool CheckIfRawStoreFull(string key)
        {
            return _rawDictionary[key].Count >= 25;
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
            [SerializeField] private List<RawScriptable> _data;
            //[SerializeField] private RawSettingsScriptable _settings;
            
            public List<RawScriptable> Data => _data;
            //public RawSettingsScriptable Settings => _settings;
        }
    }
}
