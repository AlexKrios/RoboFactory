using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Unit.Battle.Models;
using Modules.General.Unit.Models;
using Modules.General.Unit.Models.Object;
using Modules.General.Unit.Models.Scriptable;

namespace Modules.General.Unit
{
    public class UnitsController : IUnitsController
    {
        private readonly Dictionary<string, UnitObject> _allUnits;
        private readonly List<BattleUnitObject> _battleUnits;

        public int GroupCount { get; private set; }

        public UnitsController(Settings settings)
        {
            _allUnits = new Dictionary<string, UnitObject>();
            _battleUnits = new List<BattleUnitObject>();
            
            var builder = new UnitBuilder();
            foreach (var data in settings.units)
            {
                var unit = builder.Create(data);
                _allUnits.Add(unit.Key, unit);
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

        public List<BattleUnitObject> GetBattleUnits() => _battleUnits;
        public void AddBattleUnit(BattleUnitObject unit) => _battleUnits.Add(unit);
        public void RemoveUnits() => _battleUnits.Clear();
        
        
        [Serializable]
        public class Settings
        {
            public List<UnitScriptable> units;
        }
    }
}