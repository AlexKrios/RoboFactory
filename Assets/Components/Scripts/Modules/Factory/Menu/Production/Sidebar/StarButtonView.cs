using System;
using Components.Scripts.Modules.General.Item.Production;
using Components.Scripts.Modules.General.Ui;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Production.Sidebar
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

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            _menu = _uiController.FindUi<ProductionMenuView>();
            
            ResetStar();
        }

        #endregion
        
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