using System;
using System.Collections.Generic;
using System.Linq;
using Components.Scripts.Modules.Factory.Api;
using Components.Scripts.Modules.General.Item.Products.Types;
using Components.Scripts.Modules.General.Unit.Battle.Models;
using Components.Scripts.Modules.General.Unit.Object;
using Components.Scripts.Modules.General.Unit.Scriptable;
using Components.Scripts.Modules.General.Unit.Type;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.General.Unit
{
    [UsedImplicitly]
    public class UnitsManager
    {
        [Inject] private readonly ApiManager _apiManager;
        
        private readonly Dictionary<string, UnitObject> _allUnits;
        private readonly Dictionary<FaceType, Material> _faceUnits;
        private readonly List<BattleUnitObject> _battleUnits;

        public int GroupCount { get; private set; }

        public UnitsManager(Settings settings, UnitObject.Factory unitFactory)
        {
            _allUnits = new Dictionary<string, UnitObject>();
            _faceUnits = new Dictionary<FaceType, Material>();
            _battleUnits = new List<BattleUnitObject>();
            
            foreach (var data in settings.units)
            {
                var unit = unitFactory.Create().SetData(data);
                _allUnits.Add(unit.Key, unit);
            }
            
            foreach (var data in settings.faces)
            {
                _faceUnits.Add(data.Key, data.Value);
            }
        }

        public void LoadData(UnitsLoadObject unitsData)
        {
            if (unitsData == null)
                return;
            
            GroupCount = unitsData.groupCount;
            foreach (var unit in unitsData.Units)
            {
                _allUnits[unit.Key].Level = unit.Value.level;
                _allUnits[unit.Key].Experience = unit.Value.experience;
                _allUnits[unit.Key].Outfit = unit.Value.Outfit;
            }
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
            await _apiManager.SetUserUnitSingle(unitKey, unit.ToDto());
        }
        
        [Serializable]
        public class Settings
        {
            public List<UnitScriptable> units;
            public List<KeyValuePair<FaceType, Material>> faces;
        }
    }
}