using RoboFactory.General.Item.Products;
using RoboFactory.General.Localisation;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Units
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Selection/Select Button View")]
    public class SelectButtonView : ButtonBase
    {
        #region Zenject

        [Inject] private readonly LocalizationService localizationController;
        [Inject] private readonly ProductsManager _productsManager;
        [Inject] private readonly UnitsManager _unitsManager;
        [Inject] private readonly IUiController _uiController;

        #endregion
        
        #region Variables

        private UnitsMenuView _menu;
        private SelectionPopupView _selectionMenu;
        private ProductObject Equipment => _selectionMenu.ActiveItem.Data;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            _menu = _uiController.FindUi<UnitsMenuView>();
            _selectionMenu = _uiController.FindUi<SelectionPopupView>();

            SetButtonText(localizationController.GetLanguageValue(LocalizationKeys.SelectButtonTitleKey));
        }

        private void Start()
        {
            SetState();
        }

        #endregion

        public override void SetState()
        {
            SetInteractable(!Equipment.IsEmpty() || Equipment.ProductType == 0);
        }
        
        protected override async void Click()
        {
            base.Click();

            await _unitsManager.SetEquipment(_menu.ActiveUnit.Key, Equipment.ProductGroup, Equipment.Key);
            _menu.Info.ActiveCell.SetEquipmentData(Equipment);
            _menu.Info.UnitModel.SetEquipment(Equipment);
            
            if (Equipment.ProductType != 0)
                await _productsManager.RemoveItem(Equipment.Key);
            
            _selectionMenu.Close();
        }
    }
}
