using System.Collections.Generic;
using Modules.General.Unit.Battle.Models;
using Modules.General.Unit.Models;
using Modules.General.Unit.Models.Object;

namespace Modules.General.Unit
{
    public interface IUnitsController
    {
        int GroupCount { get; }

        void LoadUnitsInfo(UnitsLoadObject unitsData);

        List<UnitObject> GetUnits();
        UnitObject GetUnit(string key);

        List<BattleUnitObject> GetBattleUnits();
        void AddBattleUnit(BattleUnitObject unit);
        void RemoveUnits();
    }
}