using System;
using Components.Scripts.Modules.General.Item.Production;
using Components.Scripts.Modules.General.Item.Production.Object;
using Components.Scripts.Modules.General.Localisation;
using Components.Scripts.Modules.General.Ui;
using Components.Scripts.Modules.General.Ui.Common;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Production.Sidebar
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Create Button View")]
    public class CreateButtonView : ButtonBase
    {
        #region Zenject

        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly ProductionManager _productionManager;

        #endregion

        #region Variables
        
        public Action OnClickEvent { get; set; }

        private ProductionMenuView _menu;
        private ProductionObject _productionObject;

        #endregion
        
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            _menu = _uiController.FindUi<ProductionMenuView>();

            SetButtonText(_localisationController.GetLanguageValue(LocalisationKeys.ProductionMenuButtonTextKey));
            SetState();
        }

        #endregion

        public override void SetState()
        {
            _productionObject = new ProductionObject().SetInitData(_menu.ActiveProduct, _menu.ActiveStar);
            var buttonState = _menu.ActiveStar <= _productionManager.Level
                              && _productionManager.IsEnoughParts(_productionObject);
            
            SetInteractable(buttonState);
        }

        protected override async void Click()
        {
            base.Click();

            await _productionManager.AddProduction(_productionObject);

            OnClickEvent?.Invoke();

            if (!_productionManager.IsHaveFreeCell())
            {
                var menu = _uiController.FindUi<ProductionMenuView>();
                menu.Close();
            }
        }
    }
}
