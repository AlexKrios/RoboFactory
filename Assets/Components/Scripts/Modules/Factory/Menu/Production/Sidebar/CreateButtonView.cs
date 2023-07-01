using System;
using RoboFactory.General.Item.Production;
using RoboFactory.General.Localisation;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
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

        public void Initialize()
        {
            _menu = _uiController.FindUi<ProductionMenuView>();

            SetButtonText(_localisationController.GetLanguageValue(LocalisationKeys.ProductionMenuButtonTextKey));
            SetState();
        }

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
