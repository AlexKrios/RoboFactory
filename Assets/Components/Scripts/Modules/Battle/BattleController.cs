using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using RoboFactory.Battle.Ui;
using RoboFactory.Battle.Units;
using RoboFactory.General.Item;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Unit.Battle;
using Zenject;

namespace RoboFactory.Battle
{
    [UsedImplicitly]
    public class BattleController
    {
        [Inject] private readonly RawManager _rawManager;

        public Action OnBattleEnd { get; set; }
        
        public List<Unit> AllUnits { get; private set; }
        public List<Unit> AllyUnits { get; set; }
        public List<Unit> EnemyUnits { get; set; }
        
        public Unit ActiveUnit { get; private set; }
        public Unit TargetUnit { get; set; }

        public UnitsSectionView Units { get; set; }
        public QueueSectionView Queue { get; set; }

        public bool IsPlayerCanTurn { get; set; }
        
        public BattleResult BattleResult { get; private set; }

        public BattleController()
        {
            AllUnits = new List<Unit>();
            AllyUnits = new List<Unit>();
            EnemyUnits = new List<Unit>();
        }

        private void SortUnits()
        {
            var sortedList = AllUnits
                .OrderByDescending(x => x.Data.IsAlive)
                .ThenByDescending(x => !x.Data.IsEnded)
                .ThenByDescending(x => x.Data.CurrentInitiative)
                .ThenByDescending(x => x.Data.Team == BattleUnitTeamType.Ally)
                .ToList();

            AllUnits = sortedList;
            ActiveUnit = AllUnits.First();
        }
        
        public void StartBattle()
        {
            SortUnits();
            Queue.CreateUnitsQueue();
            Units.SetUnitsData();
            ActiveUnit.StartTurn();
        }

        private void StartTurn()
        {
            ClearTeam();
            SortUnits();
            Queue.SortUnitsQueue();
            ActiveUnit.StartTurn();
        }

        public void EndTurn()
        {
            if (IsBattleEnd())
            {
                OnBattleEnd?.Invoke();
                return;
            }

            if (IsPhaseEnd())
                AllUnits.ForEach(x => x.Data.IsEnded = false);

            StartTurn();
        }

        private void ClearTeam()
        {
            ActiveUnit = null;
            TargetUnit = null;
            
            AllyUnits.Clear();
            EnemyUnits.Clear();
        }
        
        private bool IsPhaseEnd()
        {
            return AllUnits
                .Where(x => x.Data.IsAlive)
                .All(x => x.Data.IsEnded);
        }
        private bool IsBattleEnd()
        {
            var allyDead = AllUnits
                .Where(x => x.Data.Team == BattleUnitTeamType.Ally)
                .All(x => !x.Data.IsAlive);
            
            var enemyDead = AllUnits
                .Where(x => x.Data.Team == BattleUnitTeamType.Enemy)
                .All(x => !x.Data.IsAlive);

            if (allyDead)
                BattleResult = BattleResult.Lose;
            if (enemyDead)
                BattleResult = BattleResult.Win;

            return allyDead || enemyDead;
        }
        
        public void CollectItems(List<PartObject> parts)
        {
            parts.ForEach(x => _rawManager.GetRaw(x.data.Key).IncrementCount(x.count));
        }
    }
    
    public enum BattleResult
    {
        Win,
        Lose
    }
}