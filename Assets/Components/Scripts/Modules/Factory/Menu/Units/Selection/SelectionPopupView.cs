using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Asset;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Localization;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Units
{
    public class SelectionPopupView : PopupBase
    {
        [Inject] private readonly AddressableService _addressableService;
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ProductsService productsService;
        [Inject] private readonly UnitsMenuFactory _unitsMenuFactory;

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
        
        private UnitsMenuView _menu;
        private SelectionCellView _activeItem;
        public SelectionCellView ActiveItem
        {
            get => _activeItem;
            private set
            {
                if (_activeItem != null)
                    _activeItem.SetInactive();

                _activeItem = value;
                _activeItem.SetActive();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            
            _uiController.AddUi(this);
            _menu = _uiController.FindUi<UnitsMenuView>();

            SetSelectionData();
            SetTitleData();
            SetSidebarData();
        }

        private void SetTitleData()
        {
            _title.text = _localizationService.GetLanguageValue(ActiveItem.Data.Key);
        }

        private void SetSelectionData()
        {
            if (_cells.Count != 0)
            {
                _cells.ForEach(x => Destroy(x.gameObject));
                _cells.Clear();
            }
            
            var equipments = productsService.GetAllProducts()
                .Where(x => x.ProductGroup == _menu.ActiveEquipment.ProductGroup)
                .Where(x => x.UnitType == _menu.ActiveUnit.UnitType)
                .Where(x => x.IsProduct)
                .ToList();

            foreach (var data in equipments)
            {
                var cell = _unitsMenuFactory.CreateSelectionCell(_cellsParent);
                cell.OnEquipmentClick += OnEquipmentClick;
                cell.SetEquipmentData(data);
                _cells.Add(cell);
            }

            ActiveItem = _cells.First();
        }

        private void OnEquipmentClick(SelectionCellView cell)
        {
            if (ActiveItem == cell)
                return;
            
            if (ActiveItem != null)
                ActiveItem.SetInactive();

            ActiveItem = cell;
            ActiveItem.SetActive();
            
            SetTitleData();
            SetSidebarData();
            _select.SetState();
        }
        
        private async void SetSidebarData()
        {
            var product = productsService.GetProduct(ActiveItem.Data.Key);
            _sidebarTitle.text = _localizationService.GetLanguageValue(product.Key);
            _sidebarIcon.sprite = await _addressableService.LoadAssetAsync<Sprite>(product.IconRef);
            foreach (var specData in product.Recipe.Specs)
            {
                var spec = _specs.First(x => x.SpecType == specData.Type);
                spec.SetData(specData);
            }
        }
    }
}