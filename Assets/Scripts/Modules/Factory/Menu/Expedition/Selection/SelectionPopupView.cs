using System.Collections.Generic;
using System.Linq;
using Modules.General.Asset;
using Modules.General.Item.Products;
using Modules.General.Item.Products.Models.Object.Spec;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Localisation;
using Modules.General.Ui.Common.Menu;
using Modules.General.Unit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Selection
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Selection/Selection Popup View")]
    public class SelectionPopupView : PopupBase
    {
        #region Zenject
        
        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IProductsController _productsController;
        [Inject] private readonly IUnitsController _unitsController;
        [Inject] private readonly ExpeditionMenuFactory _expeditionMenuFactory;

        #endregion

        #region Components

        [Space]
        [SerializeField] private TextMeshProUGUI title;
        
        [Space]
        [SerializeField] private Transform cellsParent;
        [SerializeField] private List<SelectionCellView> cells;
        
        [Space]
        [SerializeField] private TextMeshProUGUI sidebarTitle;
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
            var unitsObject = _unitsController.GetUnits()
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
            var unit = _unitsController.GetUnit(ActiveUnit.Data.Key);
            sidebarTitle.text = _localisationController.GetLanguageValue(unit.Key);
            sidebarIcon.sprite = await AssetsController.LoadAsset<Sprite>(unit.IconRef);
            for (var i = 0; i < unit.Outfit.Count; i++)
            {
                var spec = unit.Specs[(SpecType) i];
                var specObject = new SpecObject();
                foreach (var outfit in unit.Outfit)
                {
                    if (outfit == Constants.EmptyOutfit)
                        continue;

                    var item = _productsController.GetProduct(outfit).Recipe;
                    spec += item.Specs[i].value;
                }

                specObject.type = (SpecType) i;
                specObject.value = spec;
                
                specs[i].SetData(specObject);
            }
        }
    }
}