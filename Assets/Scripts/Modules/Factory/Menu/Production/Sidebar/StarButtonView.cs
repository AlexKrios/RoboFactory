using System;
using Modules.General.Item.Production;
using Modules.General.Ui;
using Modules.General.Ui.Common.Menu;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Production.Sidebar
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Star Button View")]
    public class StarButtonView : StarButtonBase
    {
        private const int DefaultStar = 1;
        
        #region Zenject
        
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly IProductionController _productionController;

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
            if (nextStar - 1 <= _productionController.Level && nextStar <= Constants.MaxStar)
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