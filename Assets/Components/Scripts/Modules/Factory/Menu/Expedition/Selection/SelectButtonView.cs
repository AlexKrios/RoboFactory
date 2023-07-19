using RoboFactory.General.Localisation;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Unit;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Selection/Select Button View")]
    public class SelectButtonView : ButtonBase
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly LocalizationService localizationController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly UnitsManager _unitsManager;

        #endregion
        
        #region Variables

        private ExpeditionMenuView _menu;
        private SelectionPopupView _selectionMenu;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            _menu = _uiController.FindUi<ExpeditionMenuView>();
            _selectionMenu = _uiController.FindUi<SelectionPopupView>();
            
            SetButtonText(localizationController.GetLanguageValue(LocalizationKeys.SelectButtonTitleKey));
        }

        #endregion

        protected override void Click()
        {
            base.Click();

            var activeUnit = _menu.Units.ActiveUnit;
            activeUnit.SetData(_selectionMenu.ActiveUnit.Data);
            activeUnit.ActivateCell(false);
            
            var unit = _container.InstantiatePrefab(activeUnit.Data.Model, activeUnit.UnitParent.transform);
            var unitComponent = _container.InstantiateComponent<UnitModel>(unit);
            unitComponent.SetData(activeUnit.Data);
            
            _unitsManager.RemoveUnits();
            
            _menu.Units.EventClick?.Invoke();
            _selectionMenu.Close();
        }
    }
}
