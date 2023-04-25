using Modules.General.Item.Products.Models.Object;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Save;
using Modules.General.Ui;
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
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ISaveController _saveController;

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

            SetButtonText(_localisationController.GetLanguageValue(LocalisationKeys.SelectButtonTitleKey));
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
        
        protected override void Click()
        {
            base.Click();
            
            _menu.ActiveUnit.Outfit[(int)Equipment.ProductGroup - 1] = Equipment.Key;
            _menu.Info.ActiveCell.SetEquipmentData(Equipment);
            _menu.Info.UnitModel.SetEquipment(Equipment);
            
            if (Equipment.ProductType != 0)
                Equipment.DecrementCount();
            
            _saveController.SaveUnits();
            _selectionMenu.Close();
        }
    }
}
