using System;
using Modules.General.Ui.Common.Menu;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Conversion.Components
{
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Star Button View")]
    public class StarButtonView : StarButtonBase
    {
        #region Zenject
        
        [Inject] private readonly ConversionMenuManager _conversionMenuManager;
        
        #endregion
        
        #region Variables

        public Action OnClickEvent { get; set; }

        private int _activeStar;
        public int ActiveStar
        {
            get => _activeStar;
            set
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

            _conversionMenuManager.Star = this;
            ResetStar();
        }

        private void OnDestroy()
        {
            OnClickEvent = null;
        }

        #endregion

        protected override void Click()
        {
            base.Click();

            var nextStar = ActiveStar + 1;
            ActiveStar = nextStar <= Constants.MaxStar ? nextStar : ConversionMenuManager.DefaultStar;

            OnClickEvent?.Invoke();
        }

        private void ResetStar()
        {
            ActiveStar = ConversionMenuManager.DefaultStar;
        }
    }
}