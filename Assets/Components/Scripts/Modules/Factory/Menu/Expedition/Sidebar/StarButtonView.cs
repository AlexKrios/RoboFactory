using System;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Star Button View")]
    public class StarButtonView : StarButtonBase
    {
        private const int DefaultStar = 1;
        
        #region Zenject

        [Inject] private readonly IUiController _uiController;

        #endregion
        
        #region Variables

        private Action EventClick { get; set; }
        
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

        public void Initialize()
        {
            _menu = _uiController.FindUi<ExpeditionMenuView>();
            
            ResetStar();
        }
        
        protected override void Click()
        {
            base.Click();
            
            var nextStar = ActiveStar + 1;
            ActiveStar = nextStar <= Constants.MaxStar ? nextStar : DefaultStar;

            EventClick?.Invoke();
        }

        public void ResetStar()
        {
            ActiveStar = DefaultStar;
        }
    }
}