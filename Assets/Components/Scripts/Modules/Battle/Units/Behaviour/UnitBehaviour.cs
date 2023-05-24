using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace RoboFactory.Battle.Units
{
    [RequireComponent(typeof(UnitMarks))]
    public abstract class UnitBehaviour : MonoBehaviour, IPointerClickHandler
    {
        #region Zenejct

        [Inject] protected readonly BattleController BattleController;

        #endregion

        #region Varaibles

        protected List<Unit> AllUnits => BattleController.AllUnits;
        protected List<Unit> AllyUnits
        {
            get => BattleController.AllyUnits;
            set => BattleController.AllyUnits = value;
        }

        protected List<Unit> EnemyUnits
        {
            get => BattleController.EnemyUnits;
            set => BattleController.EnemyUnits = value;
        }
        
        protected Unit ActiveUnit => BattleController.ActiveUnit;
        protected Unit TargetUnit
        {
            get => BattleController.TargetUnit;
            set => BattleController.TargetUnit = value;
        }

        protected bool IsUnitCanTurn
        {
            get => BattleController.IsPlayerCanTurn;
            set => BattleController.IsPlayerCanTurn = value;
        }

        #endregion

        public abstract void StartTurn();

        public void EndTurn()
        {
            ActiveUnit.Data.IsEnded = true;
            BattleController.EndTurn();
        }
        public virtual void SetTargets() { }

        public abstract void OnPointerClick(PointerEventData eventData);

        private void RemoveMarks()
        {
            ActiveUnit.UnitMarks.ActivateTurnMark(false);
            AllUnits.ToList().ForEach(x => x.UnitMarks.ActivateEnemyMark(false));
        }

        public virtual IEnumerator Attack()
        {
            IsUnitCanTurn = false;
            TargetUnit.Data.CurrentHealth -= ActiveUnit.Data.CurrentAttack;
            RemoveMarks();
            
            yield return StartCoroutine(ActiveUnit.UnitAnimation.PlayAttack());
            if (TargetUnit.Data.CurrentHealth <= 0)
                yield return StartCoroutine(TargetUnit.UnitAnimation.PlayDead());
            else
                yield return StartCoroutine(TargetUnit.UnitAnimation.PlayHit());

            EndTurn();
        }
        public virtual IEnumerator Heal()
        {
            IsUnitCanTurn = false;
            TargetUnit.Data.CurrentHealth += ActiveUnit.Data.CurrentAttack;
            RemoveMarks();
            
            yield return StartCoroutine(ActiveUnit.UnitAnimation.PlayAttack());
            
            EndTurn();
        }
    }
}