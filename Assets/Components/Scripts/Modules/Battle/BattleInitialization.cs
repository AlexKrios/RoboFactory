using System.Collections.Generic;
using System.Linq;
using RoboFactory.Battle.Ui;
using RoboFactory.Battle.Units;
using RoboFactory.General.Unit;
using RoboFactory.General.Unit.Battle;
using UnityEngine;
using Zenject;

namespace RoboFactory.Battle
{
    public class BattleInitialization : MonoBehaviour
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly UnitsService _unitsService;
        [Inject] private readonly BattleController _battleController;
        [Inject] private readonly EndBattleFactory _endBattleFactory;
        [Inject(Id = "UiCanvas")] private readonly RectTransform _uiCanvas;

        [SerializeField] private List<Transform> _allyTeam;
        [SerializeField] private List<Transform> _enemyTeam;

        private void Awake()
        {
            _battleController.OnBattleEnd += () => _endBattleFactory.CreateEndPopup(_uiCanvas);
            
            var units = _unitsService.GetBattleUnits();
            foreach (var unitData in units.Where(x => x != null))
            {
                GameObject unit;
                if (unitData.Team == BattleUnitTeamType.Ally)
                {
                    unit = _container.InstantiatePrefab(unitData.Info.Model, _allyTeam[unitData.Place - 1]);
                    unit.transform.rotation = Quaternion.Euler(0, 90, 0);
                    _container.InstantiateComponent<UnitAlly>(unit);
                }
                else
                {
                    unit = _container.InstantiatePrefab(unitData.Info.Model, _enemyTeam[unitData.Place - 1]);
                    unit.transform.rotation = Quaternion.Euler(0, -90, 0);
                    _container.InstantiateComponent<UnitEnemy>(unit);
                }

                var unitWrapper = _container.InstantiateComponent<Unit>(unit);
                unitWrapper.SetUnitData(unitData);
                _battleController.AllUnits.Add(unitWrapper);
            }
        }

        private void Start()
        {
            _battleController.StartBattle();
        }

        private void OnDestroy()
        {
            _battleController.OnBattleEnd = null;
        }
    }
}