using System;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using Zenject;

namespace RoboFactory.Factory.Menu.Conversion
{
    public class StarButtonView : StarButtonBase
    {
        private const int DefaultStar = 2;
        
        [Inject] private readonly IUiController _uiController;
        
        public Action OnClickEvent { get; set; }

        private ConversionMenuView _menu;
        private int ActiveStar
        {
            get => _menu.ActiveStar;
            set
            {
                _menu.ActiveStar = value;
                SetStarLevel(value);
            } 
        }

        
        protected override void Awake()
        {
            base.Awake();
            
            _menu = _uiController.FindUi<ConversionMenuView>();
            
            ResetStar();
        }

        private void OnDestroy()
        {
            OnClickEvent = null;
        }

        protected override void Click()
        {
            base.Click();

            var nextStar = ActiveStar + 1;
            ActiveStar = nextStar <= Constants.MaxStar ? nextStar : DefaultStar;

            OnClickEvent?.Invoke();
        }

        private void ResetStar()
        {
            ActiveStar = DefaultStar;
        }
    }
}