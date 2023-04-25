using Modules.Factory.Menu.Expedition.Units;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Ui;
using Modules.General.Ui.Common;
using Modules.General.Unit;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Selection
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Selection/Select Button View")]
    public class SelectButtonView : ButtonBase
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly IUnitsController _unitsController;

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
            
            SetButtonText(_localisationController.GetLanguageValue(LocalisationKeys.SelectButtonTitleKey));
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
            
            _unitsController.RemoveUnits();
            
            _menu.Units.OnClickEvent?.Invoke();
            _selectionMenu.Close();
        }
    }
}
