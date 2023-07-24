using RoboFactory.General.Item.Products;
using RoboFactory.General.Localization;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using Zenject;

namespace RoboFactory.Factory.Menu.Units
{
    public class SelectButtonView : ButtonBase
    {
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly ProductsManager _productsManager;
        [Inject] private readonly UnitsManager _unitsManager;
        [Inject] private readonly IUiController _uiController;
        
        private UnitsMenuView _menu;
        private SelectionPopupView _selectionMenu;
        private ProductObject Equipment => _selectionMenu.ActiveItem.Data;

        protected override void Awake()
        {
            base.Awake();
            
            _menu = _uiController.FindUi<UnitsMenuView>();
            _selectionMenu = _uiController.FindUi<SelectionPopupView>();

            SetButtonText(_localizationService.GetLanguageValue(LocalizationKeys.SelectButtonTitleKey));
        }

        private void Start()
        {
            SetState();
        }

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
