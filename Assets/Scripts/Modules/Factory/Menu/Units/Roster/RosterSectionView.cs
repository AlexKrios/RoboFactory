using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Unit;
using Modules.General.Unit.Models.Object;
using Modules.General.Unit.Models.Type;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Units.Roster
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Roster Section View")]
    public class RosterSectionView : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly IUnitsController _unitsController;
        [Inject] private readonly UnitsMenuManager _unitsMenuManager;
        [Inject] private readonly UnitsMenuFactory _unitsMenuFactory;

        #endregion
        
        #region Components

        [SerializeField] private RectTransform unitsParent;
        [SerializeField] private List<RosterCellView> units;

        #endregion

        #region Variables

        public Action OnUnitClickEvent { get; set; }

        private RosterCellView _activeUnit;
        public RosterCellView ActiveUnit
        {
            get => _activeUnit;
            private set
            {
                if (_activeUnit != null)
                    _activeUnit.SetInactive();

                _activeUnit = value;
                _activeUnit.SetActive();
            }
        }
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            _unitsMenuManager.Roster = this;

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

            var width = 240f * units.Count + 10f * (units.Count - 1);
            unitsParent.sizeDelta = new Vector2(width, unitsParent.sizeDelta.y);

            ActiveUnit = units.First();
        }

        private void RemoveUnits()
        {
            units.ForEach(x => Destroy(x.gameObject));
            units.Clear();
        }

        private List<UnitObject> GetFilteredUnits()
        {
            if (_unitsMenuManager.ActiveUnitType == UnitType.None)
                return _unitsController.GetUnits()
                    .Where(x => x.UnitType != _unitsMenuManager.ActiveUnitType)
                    .OrderBy(x => x.UnitType == UnitType.Sniper)
                    .ThenBy(x => x.UnitType == UnitType.Support)
                    .ThenBy(x => x.UnitType == UnitType.Defender)
                    .ThenBy(x => x.UnitType == UnitType.Trooper)
                    .ToList();
            
            return _unitsController.GetUnits()
                .Where(x => x.UnitType == _unitsMenuManager.ActiveUnitType).ToList();
        }

        private void OnUnitClick(RosterCellView cell, UnitType unit)
        {
            if (ActiveUnit == cell)
                return;
            
            _unitsMenuManager.ActiveUnitType = unit;
            ActiveUnit = cell;

            OnUnitClickEvent?.Invoke();
        }
    }
}