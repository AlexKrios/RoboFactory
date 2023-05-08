using System.Collections.Generic;
using System.Linq;
using Modules.General.Asset;
using Modules.General.Item.Products;
using Modules.General.Localisation;
using Modules.General.Ui;
using Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Units.Selection
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Selection/Selection Popup View")]
    public class SelectionPopupView : PopupBase
    {
        #region Zenject
        
        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly IProductsController _productsController;
        [Inject] private readonly UnitsMenuFactory _unitsMenuFactory;

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

        #endregion

        #region Unity Methods
        
        protected override void Awake()
        {
            base.Awake();
            
            _uiController.AddUi(this);
            _menu = _uiController.FindUi<UnitsMenuView>();

            SetSelectionData();
            SetTitleData();
            SetSidebarData();
        }

        #endregion

        private void SetTitleData()
        {
            title.text = _localisationController.GetLanguageValue(ActiveItem.Data.Key);
        }

        private void SetSelectionData()
        {
            if (cells.Count != 0)
            {
                cells.ForEach(x => Destroy(x.gameObject));
                cells.Clear();
            }
            
            var equipments = _productsController.GetAllProducts()
                .Where(x => x.ProductGroup == _menu.ActiveEquipment.ProductGroup)
                .Where(x => x.UnitType == _menu.ActiveUnit.UnitType)
                .Where(x => x.IsProduct)
                .ToList();

            foreach (var data in equipments)
            {
                var cell = _unitsMenuFactory.CreateSelectionCell(cellsParent);
                cell.OnEquipmentClick += OnEquipmentClick;
                cell.SetEquipmentData(data);
                cells.Add(cell);
            }

            ActiveItem = cells.First();
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
            select.SetState();
        }
        
        private async void SetSidebarData()
        {
            var product = _productsController.GetProduct(ActiveItem.Data.Key);
            sidebarTitle.text = _localisationController.GetLanguageValue(product.Key);
            sidebarIcon.sprite = await AssetsController.LoadAsset<Sprite>(product.IconRef);
            foreach (var specData in product.Recipe.Specs)
            {
                var spec = specs.First(x => x.SpecType == specData.type);
                spec.SetData(specData);
            }
        }
    }
}