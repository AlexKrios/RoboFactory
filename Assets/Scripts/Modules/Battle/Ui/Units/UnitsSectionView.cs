﻿using System.Collections.Generic;
using Modules.General.Unit.Battle.Models;
using UnityEngine;
using Zenject;

namespace Modules.Battle.Ui.Units
{
    [AddComponentMenu("Scripts/Battle/Ui/Units Section View")]
    public class UnitsSectionView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly BattleController _battleController;

        #endregion

        #region Variables
        
        [SerializeField] private List<UnitCellView> allyCells;
        [SerializeField] private List<UnitCellView> enemyCells;

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
                    cell = allyCells[unit.Data.Place - 1];
                else
                    cell = enemyCells[unit.Data.Place - 1];
                
                cell.SetCellData(unit.Data);
            }
        }
    }
}