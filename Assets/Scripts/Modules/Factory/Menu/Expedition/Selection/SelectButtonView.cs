using Modules.Factory.Menu.Expedition.Units;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
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
        [Inject] private readonly IUnitsController _unitsController;
        [Inject] private readonly ExpeditionMenuManager _expeditionMenuManager;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            SetButtonText(_localisationController.GetLanguageValue(LocalisationKeys.SelectButtonTitleKey));
        }

        #endregion

        protected override void Click()
        {
            base.Click();

            var activeUnit = _expeditionMenuManager.Units.ActiveUnit;
            activeUnit.SetData(_expeditionMenuManager.Selection.ActiveUnit.Data);
            
            activeUnit.ActivateCell(false);
            var unit = _container.InstantiatePrefab(activeUnit.Data.Model, activeUnit.UnitParent.transform);
            var uniComponent = _container.InstantiateComponent<UnitModel>(unit);
            uniComponent.SetData(activeUnit.Data);
            
            _unitsController.RemoveUnits();
            
            _expeditionMenuManager.Units.OnClickEvent?.Invoke();
            _expeditionMenuManager.Selection.Close();
        }
    }
}
