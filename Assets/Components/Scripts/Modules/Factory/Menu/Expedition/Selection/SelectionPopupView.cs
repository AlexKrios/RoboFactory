using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Asset;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Localisation;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Selection/Selection Popup View")]
    public class SelectionPopupView : PopupBase
    {
        #region Zenject
        
        [Inject] private readonly AssetsManager _assetsManager;
        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly ProductsManager _productsManager;
        [Inject] private readonly UnitsManager _unitsManager;
        [Inject] private readonly ExpeditionMenuFactory _expeditionMenuFactory;

        #endregion

        #region Components

        [Space]
        [SerializeField] private TMP_Text title;
        
        [Space]
        [SerializeField] private Transform cellsParent;
        [SerializeField] private List<SelectionCellView> cells;
        
        [Space]
        [SerializeField] private TMP_Text sidebarTitle;
        [SerializeField] private Image sidebarIcon;
        [SerializeField] private List<SpecCellView> specs;
        
        [Space]
        [SerializeField] private SelectButtonView select;

        #endregion
        
        #region Varaibles
        
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

        #endregion

        #region Unity Methods
        
        protected override void Awake()
        {
            base.Awake();
            
            UiController.AddUi(this);
            _menu = UiController.FindUi<ExpeditionMenuView>();

            SetSelectionData();
            SetTitleData();
            SetSidebarData();
        }

        #endregion

        private void SetTitleData()
        {
            title.text = _localisationController.GetLanguageValue(ActiveUnit.Data.Key);
        }

        private void SetSelectionData()
        {
            if (cells.Count != 0)
            {
                cells.ForEach(x => Destroy(x.gameObject));
                cells.Clear();
            }
            
            var unitAttackType = _menu.Units.ActiveUnit.AttackTypes;
            var unitsObject = _unitsManager.GetUnits()
                .Where(x => unitAttackType.Contains(x.AttackType))
                .Where(x => _menu.Units.GetUnitsWithData()
                    .All(y => x != y.Data))
                .OrderBy(x => x.UnitType)
                .ToList();

            foreach (var data in unitsObject)
            {
                var cell = _expeditionMenuFactory.CreateSelectionCell(cellsParent);
                cell.OnUnitClick += OnEquipmentClick;
                cell.SetUnitData(data);
                cells.Add(cell);
            }

            ActiveUnit = cells.First();
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
            select.SetState();
        }
        
        private async void SetSidebarData()
        {
            var unit = _unitsManager.GetUnit(ActiveUnit.Data.Key);
            sidebarTitle.text = _localisationController.GetLanguageValue(unit.Key);
            sidebarIcon.sprite = await _assetsManager.LoadAssetAsync<Sprite>(unit.IconRef);
            for (var i = 0; i < unit.Outfit.Count; i++)
            {
                var spec = unit.Specs[(SpecType) i];
                var specObject = new SpecObject();
                foreach (var outfit in unit.Outfit)
                {
                    var item = _productsManager.GetProduct(outfit.Value).Recipe;
                    spec += item.Specs[i].value;
                }

                specObject.type = (SpecType) i;
                specObject.value = spec;
                
                specs[i].SetData(specObject);
            }
        }
    }
}