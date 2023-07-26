using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Api;
using RoboFactory.General.Location;
using RoboFactory.General.Profile;
using RoboFactory.General.Scriptable;
using RoboFactory.General.Services;
using UnityEngine;
using Zenject;

namespace RoboFactory.General.Expedition
{
    [UsedImplicitly]
    public class ExpeditionService : Service
    {
        [Inject] private readonly Settings _settings;
        [Inject] private readonly CommonProfile _commonProfile;
        [Inject] private readonly ApiService _apiService;
        
        public override ServiceTypeEnum ServiceType => ServiceTypeEnum.NeedAuth;
        
        public Action OnExpeditionComplete { get; set; }
        
        private readonly Dictionary<Guid, ExpeditionObject> _expeditionData = new();
        public LocationObject CurrentBattleLocation { get; set; }
        
        public int CellCount { get; set; }

        public ExpeditionService(Settings settings)
        {
            CellCount = 1;
        }
        
        protected override UniTask InitializeAsync()
        {
            var expeditionData = _commonProfile.UserProfile.ExpeditionsSection;
            
            CellCount = expeditionData.Count;
            
            if (expeditionData.Expeditions == null) return UniTask.CompletedTask;
            
            foreach (var expeditionDto in expeditionData.Expeditions)
            {
                var expedition = new ExpeditionObject().SetDtoData(expeditionDto.Value);
                _expeditionData.Add(expedition.Id, expedition);
            }
            
            return UniTask.CompletedTask;
        }
        
        public bool IsHaveFreeCell()
        {
            return _expeditionData.Count < CellCount;
        }
        
        public Dictionary<string, ExpeditionDto> GetAllExpeditionDto()
        {
            var rawDto = new Dictionary<string, ExpeditionDto>();
            _expeditionData.ToList()
                .ForEach(x => rawDto.Add(x.Key.ToString(), x.Value.ToDto()));
            
            return rawDto;
        } 
        
        public async UniTask AddExpedition(ExpeditionObject data)
        {
            _expeditionData.Add(data.Id, data);
            await _apiService.AddUserExpedition(data.Id, data.ToDto());
        }
        public List<ExpeditionObject> GetAllExpeditions() => _expeditionData.Values.ToList();
        public ExpeditionObject GetExpedition(Guid id) => 
            _expeditionData.First(x => x.Key == id).Value;
        public async UniTask RemoveExpedition(Guid id)
        {
            _expeditionData.Remove(id);
            await _apiService.RemoveUserExpedition(id);
            
            OnExpeditionComplete?.Invoke();
        }
        
        public UpgradeDataObject GetUpgradeData()
        {
            return _settings.UpgradeData.Data.First(x => x.Count == CellCount);
        }
        
        public async void IncreaseQueueCount()
        {
            CellCount++;
            await _apiService.SetUserExpeditionQueueCount(CellCount);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private UpgradeDataScriptable _upgradeData;
            
            public UpgradeDataScriptable UpgradeData => _upgradeData;
        }
    }
}