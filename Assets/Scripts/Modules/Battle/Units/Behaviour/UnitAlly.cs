using System.Linq;
using Modules.General.Unit.Battle.Models;
using Modules.General.Unit.Type;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.Battle.Units.Behaviour
{
    [AddComponentMenu("Scripts/Battle/Units/Unit Ally")]
    public class UnitAlly : UnitBehaviour
    {
        public override void StartTurn()
        {
            IsUnitCanTurn = true;
            ActiveUnit.UnitMarks.ActivateTurnMark(true);
            AllyUnits = AllUnits
                .Where(x => x.Data.Team == BattleUnitTeamType.Ally)
                .Where(x => x.Data.IsAlive)
                .ToList();
            
            EnemyUnits = AllUnits
                .Where(x => x.Data.Team == BattleUnitTeamType.Enemy)
                .Where(x => x.Data.IsAlive)
                .ToList();
        }

        private bool IsHealTarget(BattleUnitObject unit)
        {
            var isAlly = unit.Team == BattleUnitTeamType.Ally;
            var isAttackHeal = ActiveUnit.Data.Info.AttackType == AttackType.Heal;
            var isNeedHeal = unit.CurrentHealth < unit.Info.Health;

            return isAlly && isAttackHeal && isNeedHeal;
        }
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!IsUnitCanTurn)
                return;
            
            TargetUnit = GetComponent<Unit>();
            if (!IsHealTarget(TargetUnit.Data)) 
                return;
                
            StartCoroutine(Heal());
        }
        
        public override void SetTargets()
        {
            if (ActiveUnit.Data.Info.AttackType == AttackType.Heal)
            {
                AllUnits
                    .Where(x => x.Data.CurrentHealth < x.Data.Info.Health)
                    .ToList()
                    .ForEach(x => x.UnitMarks.ActivateAllyMark(true));
            }

            EnemyUnits.ForEach(x => x.UnitMarks.ActivateEnemyMark(true));
        }
    }
}