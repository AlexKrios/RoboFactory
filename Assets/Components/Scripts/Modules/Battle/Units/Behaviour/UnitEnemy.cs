using System.Collections;
using System.Linq;
using Components.Scripts.Modules.General.Unit.Battle.Models;
using Components.Scripts.Modules.General.Unit.Type;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

namespace Components.Scripts.Modules.Battle.Units.Behaviour
{
    [AddComponentMenu("Scripts/Battle/Units/Unit Enemy")]
    public class UnitEnemy : UnitBehaviour
    {
        public override void StartTurn()
        {
            IsUnitCanTurn = true;
            ActiveUnit.UnitMarks.ActivateTurnMark(true);
            AllyUnits = AllUnits
                .Where(x => x.Data.Team == BattleUnitTeamType.Enemy)
                .Where(x => x.Data.IsAlive)
                .ToList();
            
            EnemyUnits = AllUnits
                .Where(x => x.Data.Team == BattleUnitTeamType.Ally)
                .Where(x => x.Data.IsAlive)
                .ToList();

            Turn();
        }
        
        private bool IsAttackTarget(BattleUnitObject unit)
        {
            return unit.Team == BattleUnitTeamType.Enemy && unit.IsAlive;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!IsUnitCanTurn)
                return;
            
            TargetUnit = GetComponent<Unit>();
            if (!IsAttackTarget(TargetUnit.Data)) 
                return;

            
            StartCoroutine(ActiveUnit.UnitBehaviour.Attack());
        }

        private void Turn()
        {
            if (ActiveUnit.Data.Info.AttackType == AttackType.Heal)
            {
                var count = AllyUnits.Count(x => x.Data.CurrentHealth < x.Data.Info.Health);
                if (count != 0)
                {
                    StartCoroutine(Heal());
                    return;
                }
            }
            
            StartCoroutine(Attack());
        }

        public override IEnumerator Heal()
        {
            var targets = AllUnits.Where(x => x.Data.CurrentHealth < x.Data.Info.Health).ToList();
            var random = new Random().Next(0, targets.Count);
            TargetUnit = targets[random];
            
            yield return base.Heal();
        }
        public override IEnumerator Attack()
        {
            var random = new Random().Next(0, EnemyUnits.Count);
            TargetUnit = EnemyUnits.First(x => x == EnemyUnits[random]);
            
            yield return base.Attack();
        }
    }
}