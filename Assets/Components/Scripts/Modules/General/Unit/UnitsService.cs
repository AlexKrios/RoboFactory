using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Api;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Profile;
using RoboFactory.General.Services;
using RoboFactory.General.Unit.Battle;
using UnityEngine;
using Zenject;

namespace RoboFactory.General.Unit
{
    [UsedImplicitly]
    public class UnitsService : Service
    {
        [Inject] private readonly Settings _settings;
        [Inject] private readonly CommonProfile _commonProfile;
        [Inject] private readonly ApiService _apiService;
        [Inject] private readonly UnitObject.Factory _unitFactory;
        
        public override ServiceTypeEnum ServiceType => ServiceTypeEnum.NeedAuth;
        
        private readonly Dictionary<string, UnitObject> _allUnits = new();
        private readonly Dictionary<FaceType, Material> _faceUnits = new();
        private readonly List<BattleUnitObject> _battleUnits = new();

        //public int GroupCount { get; private set; }

        protected override UniTask InitializeAsync()
        {
            if (_allUnits.Count != 0)
                _allUnits.Clear();
            
            foreach (var data in _settings.Units)
            {
                var unit = _unitFactory.Create().SetData(data);
                _allUnits.Add(unit.Key, unit);
            }
            
            foreach (var data in _settings.Faces)
            {
                _faceUnits.Add(data.Type, data.Face);
            }
            
            var unitsData = _commonProfile.UserProfile.UnitsSection;
            if (unitsData?.Units == null) return UniTask.CompletedTask;
            
            //GroupCount = unitsData.groupCount;
            foreach (var unit in unitsData.Units)
            {
                _allUnits[unit.Key].Level = unit.Value.Level;
                _allUnits[unit.Key].Experience = unit.Value.Experience;
                _allUnits[unit.Key].Outfit = unit.Value.Outfit;
            }
            
            return UniTask.CompletedTask;
        }
        
        public UnitObject GetUnit(string key) => _allUnits[key];
        public List<UnitObject> GetUnits() => _allUnits.Values.ToList();
        public Dictionary<string, UnitDto> GetAllUnitsDto()
        {
            var rawDto = new Dictionary<string, UnitDto>();
            _allUnits.ToList().ForEach(x => rawDto.Add(x.Key, x.Value.ToDto()));
            return rawDto;
        } 
        
        public Material GetFace(FaceType type) => _faceUnits[type];

        public List<BattleUnitObject> GetBattleUnits() => _battleUnits;
        public void AddBattleUnit(BattleUnitObject unit) => _battleUnits.Add(unit);
        public void RemoveUnits() => _battleUnits.Clear();

        public async UniTask SetEquipment(string unitKey, ProductGroup group, string itemKey)
        {
            var unit = _allUnits[unitKey];
            unit.Outfit[group] = itemKey;
            await _apiService.SetUserUnitSingle(unitKey, unit.ToDto());
        }
        
        [Serializable]
        public class Settings
        {
            [SerializeField] private List<UnitScriptable> _units;
            [SerializeField] private List<FaceTypeObject> _faces;
            
            public List<UnitScriptable> Units => _units;
            public List<FaceTypeObject> Faces => _faces;
        }
    }
}