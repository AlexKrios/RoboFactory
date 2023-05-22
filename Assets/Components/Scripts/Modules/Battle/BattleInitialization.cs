using System.Collections.Generic;
using System.Linq;
using Components.Scripts.Modules.Battle.Ui.End;
using Components.Scripts.Modules.Battle.Units;
using Components.Scripts.Modules.Battle.Units.Behaviour;
using Components.Scripts.Modules.General.Unit;
using Components.Scripts.Modules.General.Unit.Battle.Models;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Battle
{
    [AddComponentMenu("Scripts/Battle/Battle Initialization", 1)]
    public class BattleInitialization : MonoBehaviour
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly UnitsManager _unitsManager;
        [Inject] private readonly BattleController _battleController;
        [Inject] private readonly EndBattleFactory _endBattleFactory;
        [Inject(Id = "UiCanvas")] private readonly RectTransform _uiCanvas;

        [SerializeField] private List<Transform> allyTeam;
        [SerializeField] private List<Transform> enemyTeam;

        private void Awake()
        {
            _battleController.OnBattleEnd += () => _endBattleFactory.CreateEndPopup(_uiCanvas);
            
            var units = _unitsManager.GetBattleUnits();
            foreach (var unitData in units.Where(x => x != null))
            {
                GameObject unit;
                if (unitData.Team == BattleUnitTeamType.Ally)
                {
                    unit = _container.InstantiatePrefab(unitData.Info.Model, allyTeam[unitData.Place - 1]);
                    unit.transform.rotation = Quaternion.Euler(0, 90, 0);
                    _container.InstantiateComponent<UnitAlly>(unit);
                }
                else
                {
                    unit = _container.InstantiatePrefab(unitData.Info.Model, enemyTeam[unitData.Place - 1]);
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