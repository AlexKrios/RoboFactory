using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Api;
using RoboFactory.General.Profile;
using RoboFactory.General.Services;
using UnityEngine;
using Zenject;

namespace RoboFactory.General.Item.Raw
{
    [UsedImplicitly]
    public class RawService : Service, IItemManager
    {
        public ItemType ItemType { get; }
        
        [Inject] private readonly Settings _settings;
        [Inject] private readonly CommonProfile _commonProfile;
        [Inject] private readonly ApiService _apiService;
        
        public override ServiceTypeEnum ServiceType => ServiceTypeEnum.NeedAuth;

        private readonly Dictionary<string, RawObject> _rawDictionary = new();

        public Action OnRawSet { get; set; }
        
        public RawService(Settings settings)
        {
            ItemType = ItemType.Raw;
        }
        
        protected override UniTask InitializeAsync()
        {
            foreach (var data in _settings.Data)
            {
                var rawObj = new RawObject().SetInitData(data);
                _rawDictionary.Add(rawObj.Key, rawObj);
            }
            
            var storeData = _commonProfile.UserProfile.StoresSection;
            if (storeData == null) return UniTask.CompletedTask;

            var rawData = storeData.Raw;
            foreach (var raw in rawData)
            {
                _rawDictionary[raw.Key].Count = raw.Value.Count;
                _rawDictionary[raw.Key].Level = raw.Value.Level;
            }
            
            OnRawSet?.Invoke();
            
            return UniTask.CompletedTask;
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

        public async UniTask AddItem(string key, int count = 1)
        {
            var raw = _rawDictionary[key];
            raw.IncrementCount(count);
            await _apiService.SetUserRawSingle(key, raw.ToDto());
            
            OnRawSet?.Invoke();
        }

        public async UniTask AddItemsThenSend(List<PartObject> data)
        {
            data.ForEach(x => _rawDictionary[x.Data.Key].IncrementCount(x.Count));
            await SendRawOnServer();
            
            OnRawSet?.Invoke();
        }
        
        public async UniTask RemoveItem(string key, int count = 1)
        {
            var raw = _rawDictionary[key];
            raw.DecrementCount(count);
            await _apiService.SetUserRawSingle(key, raw.ToDto());
            
            OnRawSet?.Invoke();
        }

        private async UniTask SendRawOnServer()
        {
            var rawData = GetAllRawDto();
            await _apiService.SetUserRaw(rawData);
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
