using Modules.General.Item.Products.Models.Object;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Save;
using Modules.General.Ui.Common;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Units.Selection
{
    [AddComponentMenu("Scripts/Factory/Menu/Units/Selection/Select Button View")]
    public class SelectButtonView : ButtonBase
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly ISaveController _saveController;
        [Inject] private readonly UnitsMenuManager _unitsMenuManager;

        #endregion
        
        #region Variables

        private ProductObject Equipment => _unitsMenuManager.Selection.ActiveItem.Data;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            SetButtonText(_localisationController.GetLanguageValue(LocalisationKeys.SelectButtonTitleKey));
        }

        private void Start()
        {
            SetState();
        }

        #endregion

        public override void SetState()
        {
            SetInteractable(!Equipment.IsEmpty());
        }
        
        protected override void Click()
        {
            base.Click();
            
            var unit = _unitsMenuManager.Roster.ActiveUnit;
            var equipmentCell = _unitsMenuManager.Info.ActiveCell;
            unit.UnitData.Outfit[(int)Equipment.ProductGroup] = Equipment.Key;
            
            equipmentCell.SetEquipmentData(Equipment);
            Equipment.DecrementCount();
            
            _saveController.SaveUnits();
            _unitsMenuManager.Selection.Close();
        }
    }
}
