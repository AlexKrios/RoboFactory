using System;
using Modules.General.Ui;
using Modules.General.Ui.Common.Menu;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Sidebar
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Star Button View")]
    public class StarButtonView : StarButtonBase
    {
        private const int DefaultStar = 1;
        
        #region Zenject

        [Inject] private readonly IUiController _uiController;

        #endregion
        
        #region Variables

        public Action OnClickEvent { get; set; }
        
        private ExpeditionMenuView _menu;
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
            
            _menu = _uiController.FindUi<ExpeditionMenuView>();
            
            ResetStar();
        }

        #endregion
        
        protected override void Click()
        {
            base.Click();
            
            var nextStar = ActiveStar + 1;
            ActiveStar = nextStar <= Constants.MaxStar ? nextStar : DefaultStar;

            OnClickEvent?.Invoke();
        }

        public void ResetStar()
        {
            ActiveStar = DefaultStar;
        }
    }
}