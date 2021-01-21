using System;
using Modules.General.Ui.Common.Menu;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Sidebar
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Star Button View")]
    public class StarButtonView : StarButtonBase
    {
        #region Zenject

        [Inject] private readonly ExpeditionMenuManager _expeditionMenuManager;

        #endregion
        
        #region Variables

        public Action OnClickEvent { get; set; }
        
        private int _activeStar;
        private int ActiveStar
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

            _expeditionMenuManager.Star = this;
            ResetStar();
        }

        #endregion
        
        protected override void Click()
        {
            base.Click();
            
            var nextStar = ActiveStar + 1;
            ActiveStar = nextStar <= Constants.MaxStar ? nextStar : ExpeditionMenuManager.DefaultStar;

            OnClickEvent?.Invoke();
        }

        public void ResetStar()
        {
            ActiveStar = ExpeditionMenuManager.DefaultStar;
        }
    }
}