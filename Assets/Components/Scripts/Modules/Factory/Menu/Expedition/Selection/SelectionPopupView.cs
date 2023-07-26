using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Asset;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Localization;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    public class SelectionPopupView : PopupBase
    {
        [Inject] private readonly AddressableService _addressableService;
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly ProductsService _productsService;
        [Inject] private readonly UnitsService _unitsService;
        [Inject] private readonly ExpeditionMenuFactory _expeditionMenuFactory;

        [Space]
        [SerializeField] private TMP_Text _title;
        
        [Space]
        [SerializeField] private Transform _cellsParent;
        [SerializeField] private List<SelectionCellView> _cells;
        
        [Space]
        [SerializeField] private TMP_Text _sidebarTitle;
        [SerializeField] private Image _sidebarIcon;
        [SerializeField] private List<SpecCellView> _specs;
        
        [Space]
        [SerializeField] private SelectButtonView _select;
        
        private ExpeditionMenuView _menu;
        private SelectionCellView _activeUnit;
        public SelectionCellView ActiveUnit
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

        protected override void Awake()
        {
            base.Awake();
            
            UiController.AddUi(this);
            _menu = UiController.FindUi<ExpeditionMenuView>();

            SetSelectionData();
            SetTitleData();
            SetSidebarData();
        }

        private void SetTitleData()
        {
            _title.text = _localizationService.GetLanguageValue(ActiveUnit.Data.Key);
        }

        private void SetSelectionData()
        {
            if (_cells.Count != 0)
            {
                _cells.ForEach(x => Destroy(x.gameObject));
                _cells.Clear();
            }
            
            var unitAttackType = _menu.Units.ActiveUnit.AttackTypes;
            var unitsObject = _unitsService.GetUnits()
                .Where(x => unitAttackType.Contains(x.AttackType))
                .Where(x => _menu.Units.GetUnitsWithData()
                    .All(y => x != y.Data))
                .OrderBy(x => x.UnitType)
                .ToList();

            foreach (var data in unitsObject)
            {
                var cell = _expeditionMenuFactory.CreateSelectionCell(_cellsParent);
                cell.OnUnitClick += OnEquipmentClick;
                cell.SetUnitData(data);
                _cells.Add(cell);
            }

            ActiveUnit = _cells.First();
        }

        private void OnEquipmentClick(SelectionCellView cell)
        {
            if (ActiveUnit == cell)
                return;
            
            if (ActiveUnit != null)
                ActiveUnit.SetInactive();

            ActiveUnit = cell;
            ActiveUnit.SetActive();
            
            SetTitleData();
            SetSidebarData();
            _select.SetState();
        }
        
        private async void SetSidebarData()
        {
            var unit = _unitsService.GetUnit(ActiveUnit.Data.Key);
            _sidebarTitle.text = _localizationService.GetLanguageValue(unit.Key);
            _sidebarIcon.sprite = await _addressableService.LoadAssetAsync<Sprite>(unit.IconRef);
            for (var i = 0; i < unit.Outfit.Count; i++)
            {
                var spec = unit.Specs[(SpecType) i];
                var specObject = new SpecObject();
                foreach (var outfit in unit.Outfit)
                {
                    var item = _productsService.GetProduct(outfit.Value).Recipe;
                    spec += item.Specs[i].Value;
                }

                specObject.Type = (SpecType) i;
                specObject.Value = spec;
                
                _specs[i].SetData(specObject);
            }
        }
    }
}