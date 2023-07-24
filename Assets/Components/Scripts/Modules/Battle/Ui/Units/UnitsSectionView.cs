using System.Collections.Generic;
using RoboFactory.General.Unit.Battle;
using UnityEngine;
using Zenject;

namespace RoboFactory.Battle.Ui
{
    [AddComponentMenu("Scripts/Battle/Ui/Units Section View")]
    public class UnitsSectionView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly BattleController _battleController;

        #endregion

        #region Variables
        
        [SerializeField] private List<UnitCellView> _allyCells;
        [SerializeField] private List<UnitCellView> _enemyCells;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _battleController.Units = this;
        }

        #endregion
        
        public void SetUnitsData()
        {
            var units = _battleController.AllUnits;
            foreach (var unit in units)
            {
                UnitCellView cell;
                if (unit.Data.Team == BattleUnitTeamType.Ally)
                    cell = _allyCells[unit.Data.Place - 1];
                else
                    cell = _enemyCells[unit.Data.Place - 1];
                
                cell.SetCellData(unit.Data);
            }
        }
    }
}