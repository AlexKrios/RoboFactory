using System;
using Modules.General.Item.Production;
using Modules.General.Ui.Common.Menu;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Production.Sidebar
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Star Button View")]
    public class StarButtonView : StarButtonBase
    {
        #region Zenject

        [Inject] private readonly IProductionController _productionController;
        [Inject] private readonly ProductionMenuManager _productionMenuManager;

        #endregion
        
        #region Variables

        public Action OnClickEvent { get; set; }
        
        private int _activeStar;
        public int ActiveStar
        {
            get => _activeStar;
            private set
            {
                _activeStar = value;
                SetStarLevel(value);
            } 
        }

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            _productionMenuManager.Star = this;
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
            ActiveStar = ProductionMenuManager.DefaultStar;
        }
    }
}