using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Api;
using RoboFactory.General.Location;
using RoboFactory.General.Scriptable;
using UnityEngine;
using Zenject;

namespace RoboFactory.General.Expedition
{
    [UsedImplicitly]
    public class ExpeditionManager
    {
        #region Zenject
        
        [Inject] private readonly Settings _settings;
        [Inject] private readonly ApiService apiService;

        #endregion
        
        #region Variables

        public Action OnExpeditionComplete { get; set; }
        
        private readonly Dictionary<Guid, ExpeditionObject> _expeditionData;
        public LocationObject CurrentBattleLocation { get; set; }
        
        public int CellCount { get; set; }

        #endregion

        public ExpeditionManager(Settings settings)
        {
            _expeditionData = new Dictionary<Guid, ExpeditionObject>();

            CellCount = 1;
        }
        
        public void LoadData(ExpeditionSectionDto data)
        {
            CellCount = data.count;
            
            if (data.Expeditions == null)
                return;
            
            foreach (var expeditionDto in data.Expeditions)
            {
                var expedition = new ExpeditionObject().SetDtoData(expeditionDto.Value);
                _expeditionData.Add(expedition.Id, expedition);
            }
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
            await apiService.AddUserExpedition(data.Id, data.ToDto());
        }
        public List<ExpeditionObject> GetAllExpeditions() => _expeditionData.Values.ToList();
        public ExpeditionObject GetExpedition(Guid id) => 
            _expeditionData.First(x => x.Key == id).Value;
        public async UniTask RemoveExpedition(Guid id)
        {
            _expeditionData.Remove(id);
            await apiService.RemoveUserExpedition(id);
            
            OnExpeditionComplete?.Invoke();
        }
        
        public UpgradeDataObject GetUpgradeData()
        {
            return _settings.UpgradeData.Data.First(x => x.Count == CellCount);
        }
        
        public async void IncreaseQueueCount()
        {
            CellCount++;
            await apiService.SetUserExpeditionQueueCount(CellCount);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private UpgradeDataScriptable _upgradeData;
            
            public UpgradeDataScriptable UpgradeData => _upgradeData;
        }
    }
}