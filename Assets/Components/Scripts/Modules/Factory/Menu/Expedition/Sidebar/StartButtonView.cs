using System;
using System.Linq;
using RoboFactory.General.Expedition;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Start Button View")]
    public class StartButtonView : ButtonBase
    {
        #region Zenject
        
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ExpeditionManager expeditionManager;

        #endregion

        #region Variables

        public Action EventClick { get; set; }
        
        private ExpeditionMenuView _menu;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            _menu = _uiController.FindUi<ExpeditionMenuView>();
            
            SetState();
        }

        #endregion
        
        public override void SetState()
        {
            SetInteractable(!_menu.Units.IsAllUnitEmpty());
        }

        protected override async void Click()
        {
            base.Click();
            
            expeditionManager.CurrentBattleLocation = _menu.ActiveLocation;

            var allyUnits = _menu.Units.GetUnitsWithData();
            var allyKey = allyUnits.Select(x => x.Data.Key).ToList();
            var expeditionTime = _menu.ActiveLocation.Time;
            var expedition = new ExpeditionObject
            {
                Id = Guid.NewGuid(),
                Key = _menu.ActiveLocation.Key,
                Star = "1",
                Units = allyKey,
                TimeEnd = DateTime.Now.AddSeconds(expeditionTime).ToFileTime()
            };
            
            await expeditionManager.AddExpedition(expedition);
        }
    }
}