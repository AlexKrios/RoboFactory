using System;
using Modules.General.Item.Production;
using Modules.General.Item.Production.Models.Object;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Save;
using Modules.General.Ui;
using Modules.General.Ui.Common;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Production.Sidebar
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Create Button View")]
    public class CreateButtonView : ButtonBase
    {
        #region Zenject

        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IProductionController _productionController;
        [Inject] private readonly ISaveController _saveController;
        [Inject] private readonly ProductionMenuManager _productionMenuManager;

        #endregion

        #region Variables
        
        public Action OnClickEvent { get; set; }

        private ProductionObject _productionObject;

        #endregion
        
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            SetButtonText(_localisationController.GetLanguageValue(LocalisationKeys.ProductionMenuButtonTextKey));
            SetState();
        }

        #endregion

        public override void SetState()
        { 
            var product = _productionMenuManager.Parts.ActiveItem;
            var star = _productionMenuManager.Star.ActiveStar;
            _productionObject = new ProductionObjectBuilder().Create(product, star);
            var buttonState = _productionMenuManager.Star.ActiveStar <= _productionController.Level
                              && _productionController.IsEnoughParts(_productionObject);
            SetInteractable(buttonState);
        }

        protected override void Click()
        {
            base.Click();

            _productionController.AddProduction(_productionObject);

            OnClickEvent?.Invoke();
            _saveController.Save();

            if (!_productionController.IsHaveFreeCell())
            {
                var menu = _uiController.FindUi<ProductionMenuView>(UiType.Production);
                menu.Close();
            }
        }
    }
}
