using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Ui;
using Modules.General.Unit;
using Modules.General.Unit.Object;
using Modules.General.Unit.Type;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Units.Roster
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Roster Section View")]
    public class RosterSectionView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly IUnitsController _unitsController;
        [Inject] private readonly UnitsMenuFactory _unitsMenuFactory;

        #endregion
        
        #region Components

        [SerializeField] private RectTransform unitsParent;
        [SerializeField] private List<RosterCellView> units;

        #endregion

        #region Variables

        public Action OnUnitClickEvent { get; set; }

        private UnitsMenuView _menu;
        private RosterCellView _activeUnit;
        private RosterCellView ActiveUnit
        {
            get => _activeUnit;
            set
            {
                if (_activeUnit != null)
                    _activeUnit.SetInactive();

                _activeUnit = value;
                _menu.ActiveUnit = value.Data;
                _activeUnit.SetActive();
            }
        }
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            _menu = _uiController.FindUi<UnitsMenuView>();

            CreateUnits();
        }

        #endregion
        
        public void CreateUnits()
        {
            if (units.Count != 0)
                RemoveUnits();

            var allUnits = GetFilteredUnits();
            foreach (var unitData in allUnits)
            {
                var unit = _unitsMenuFactory.CreateUnit(unitsParent);
                unit.OnClickEvent += OnUnitClick;
                unit.SetProductData(unitData);
                units.Add(unit);
            }
            
            ActiveUnit = units.First();
        }

        private void RemoveUnits()
        {
            units.ForEach(x => Destroy(x.gameObject));
            units.Clear();
        }

        private List<UnitObject> GetFilteredUnits()
        {
            if (_menu.ActiveUnitType == UnitType.All)
                return _unitsController.GetUnits()
                    .Where(x => x.UnitType != _menu.ActiveUnitType)
                    .OrderBy(x => x.UnitType == UnitType.Sniper)
                    .ThenBy(x => x.UnitType == UnitType.Support)
                    .ThenBy(x => x.UnitType == UnitType.Defender)
                    .ThenBy(x => x.UnitType == UnitType.Trooper)
                    .ToList();
            
            return _unitsController.GetUnits()
                .Where(x => x.UnitType == _menu.ActiveUnitType).ToList();
        }

        private void OnUnitClick(RosterCellView cell, UnitType unit)
        {
            if (ActiveUnit == cell)
                return;
            
            ActiveUnit = cell;
            _menu.ActiveUnit = cell.Data;

            OnUnitClickEvent?.Invoke();
        }
    }
}