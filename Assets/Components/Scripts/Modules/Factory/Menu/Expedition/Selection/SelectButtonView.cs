using RoboFactory.General.Localization;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    public class SelectButtonView : ButtonBase
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly UnitsService _unitsService;
        
        private ExpeditionMenuView _menu;
        private SelectionPopupView _selectionMenu;

        protected override void Awake()
        {
            base.Awake();
            
            _menu = _uiController.FindUi<ExpeditionMenuView>();
            _selectionMenu = _uiController.FindUi<SelectionPopupView>();
            
            SetButtonText(_localizationService.GetLanguageValue(LocalizationKeys.SelectButtonTitleKey));
        }

        protected override void Click()
        {
            base.Click();

            var activeUnit = _menu.Units.ActiveUnit;
            activeUnit.SetData(_selectionMenu.ActiveUnit.Data);
            activeUnit.ActivateCell(false);
            
            var unit = _container.InstantiatePrefab(activeUnit.Data.Model, activeUnit.UnitParent.transform);
            var unitComponent = _container.InstantiateComponent<UnitModel>(unit);
            unitComponent.SetData(activeUnit.Data);
            
            _unitsService.RemoveUnits();
            
            _menu.Units.EventClick?.Invoke();
            _selectionMenu.Close();
        }
    }
}
