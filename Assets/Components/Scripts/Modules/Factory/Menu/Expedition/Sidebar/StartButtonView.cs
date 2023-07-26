using System;
using System.Linq;
using RoboFactory.General.Expedition;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    public class StartButtonView : ButtonBase
    {
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ExpeditionService expeditionService;

        public Action EventClick { get; set; }
        
        private ExpeditionMenuView _menu;
        public void Initialize()
        {
            _menu = _uiController.FindUi<ExpeditionMenuView>();
            
            SetState();
        }

        public override void SetState()
        {
            SetInteractable(!_menu.Units.IsAllUnitEmpty());
        }

        protected override async void Click()
        {
            base.Click();
            
            expeditionService.CurrentBattleLocation = _menu.ActiveLocation;

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
            
            await expeditionService.AddExpedition(expedition);
        }
    }
}