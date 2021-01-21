using Modules.Battle.Units.Behaviour;
using Modules.General.Unit.Battle.Models;
using UnityEngine;

namespace Modules.Battle.Units
{
    [AddComponentMenu("Scripts/Battle/Unit", 1)]
    public class Unit : MonoBehaviour
    {
        public BattleUnitObject Data { get; private set; }
        
        public UnitBehaviour UnitBehaviour { get; private  set; }
        public UnitAnimation UnitAnimation { get; private set; }
        public UnitMarks UnitMarks { get; private  set; }

        private void Awake()
        {
            UnitBehaviour = GetComponent<UnitBehaviour>();
            UnitAnimation = GetComponent<UnitAnimation>();
            UnitMarks = GetComponent<UnitMarks>();
        }

        private void Start()
        {
            Data.Model = gameObject;
        }

        public void SetUnitData(BattleUnitObject data)
        {
            Data = data;
        }

        public void StartTurn()
        {
            UnitBehaviour.StartTurn();
            UnitBehaviour.SetTargets();
        }
    }
}