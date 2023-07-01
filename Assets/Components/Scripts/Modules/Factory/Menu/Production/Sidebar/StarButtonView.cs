using System;
using RoboFactory.General.Item.Production;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Star Button View")]
    public class StarButtonView : StarButtonBase
    {
        private const int DefaultStar = 1;
        
        #region Zenject
        
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ProductionManager _productionManager;

        #endregion
        
        #region Variables

        public Action OnClickEvent { get; set; }
        
        private ProductionMenuView _menu;
        private int ActiveStar
        {
            get => _menu.ActiveStar;
            set
            {
                _menu.ActiveStar = value;
                SetStarLevel(value);
            } 
        }

        #endregion

        public void Initialize()
        {
            _menu = _uiController.FindUi<ProductionMenuView>();
            
            ResetStar();
        }

        protected override void Click()
        {
            base.Click();

            var nextStar = ActiveStar + 1;
            if (nextStar - 1 <= _productionManager.Level && nextStar <= Constants.MaxStar)
                ActiveStar = nextStar;
            else
                ActiveStar = 1;

            OnClickEvent?.Invoke();
        }

        public void ResetStar()
        {
            ActiveStar = DefaultStar;
        }
    }
}