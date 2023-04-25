using System.Collections.Generic;
using Modules.General.Unit.Battle.Models;
using Modules.General.Unit.Object;
using Modules.General.Unit.Type;
using UnityEngine;

namespace Modules.General.Unit
{
    public interface IUnitsController
    {
        int GroupCount { get; }

        void LoadUnitsInfo(UnitsLoadObject unitsData);

        List<UnitObject> GetUnits();
        UnitObject GetUnit(string key);

        Material GetFace(FaceType type);

        List<BattleUnitObject> GetBattleUnits();
        void AddBattleUnit(BattleUnitObject unit);
        void RemoveUnits();
    }
}