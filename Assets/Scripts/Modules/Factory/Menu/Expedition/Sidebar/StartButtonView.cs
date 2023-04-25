using System;
using System.Linq;
using Modules.General.Location;
using Modules.General.Location.Model;
using Modules.General.Save;
using Modules.General.Ui;
using Modules.General.Ui.Common;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Sidebar
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Start Button View")]
    public class StartButtonView : ButtonBase
    {
        #region Zenject
        
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ISaveController _saveController;
        [Inject] private readonly IExpeditionController _expeditionController;

        #endregion

        #region Variables

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

        protected override void Click()
        {
            base.Click();
            
            _expeditionController.CurrentBattleLocation = _menu.ActiveLocation;

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
            
            _expeditionController.AddExpedition(expedition);
            _saveController.SaveExpeditions();
            
            /*var builder = new BattleUnitBuilder();
            foreach (var ally in allyUnits)
            {
                var unit = _unitsController.GetUnit(ally.Data.Key);
                var allyUnit = builder.Create(unit);
                allyUnit.Team = BattleUnitTeamType.Ally;
                allyUnit.Place = ally.transform.GetSiblingIndex() + 1;

                _unitsController.AddBattleUnit(allyUnit);
            }

            var enemyUnits = activeLocation.Data.Enemies;
            foreach (var enemy in enemyUnits)
            {
                var unit = _unitsController.GetUnit(enemy.data.Key);
                var enemyUnit = builder.Create(unit);
                enemyUnit.Team = BattleUnitTeamType.Enemy;
                enemyUnit.Place = enemy.place;

                _unitsController.AddBattleUnit(enemyUnit);
            }
            
            _uiController.RemoveUi(UiType.Expedition);
            SceneManager.LoadScene("Battle");*/
        }
    }
}