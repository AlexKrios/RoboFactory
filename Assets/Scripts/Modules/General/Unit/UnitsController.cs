using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Modules.General.Unit.Battle.Models;
using Modules.General.Unit.Object;
using Modules.General.Unit.Scriptable;
using Modules.General.Unit.Type;
using UnityEngine;

namespace Modules.General.Unit
{
    [UsedImplicitly]
    public class UnitsController : IUnitsController
    {
        private readonly Dictionary<string, UnitObject> _allUnits;
        private readonly Dictionary<FaceType, Material> _faceUnits;
        private readonly List<BattleUnitObject> _battleUnits;

        public int GroupCount { get; private set; }

        public UnitsController(Settings settings, UnitObject.Factory unitFactory)
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

        public void LoadUnitsInfo(UnitsLoadObject unitsData)
        {
            GroupCount = unitsData.groupCount;
            foreach (var unit in unitsData.units)
            {
                _allUnits[unit.key].Level = unit.level;
                _allUnits[unit.key].Experience = unit.experience;
                _allUnits[unit.key].IsLocked = unit.isLocked;
                _allUnits[unit.key].Outfit = unit.outfit;
            }
        }
        
        public UnitObject GetUnit(string key) => _allUnits[key];
        public List<UnitObject> GetUnits() => _allUnits.Values.ToList();
        
        public Material GetFace(FaceType type) => _faceUnits[type];

        public List<BattleUnitObject> GetBattleUnits() => _battleUnits;
        public void AddBattleUnit(BattleUnitObject unit) => _battleUnits.Add(unit);
        public void RemoveUnits() => _battleUnits.Clear();
        
        
        [Serializable]
        public class Settings
        {
            public List<UnitScriptable> units;
            public List<KeyValuePair<FaceType, Material>> faces;
        }
    }
}