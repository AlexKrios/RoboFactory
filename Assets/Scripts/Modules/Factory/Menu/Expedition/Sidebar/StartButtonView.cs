using System;
using System.Linq;
using Modules.General.Location;
using Modules.General.Location.Model;
using Modules.General.Save;
using Modules.General.Ui.Common;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Sidebar
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Start Button View")]
    public class StartButtonView : ButtonBase
    {
        #region Zenject
        
        [Inject] private readonly ISaveController _saveController;
        [Inject] private readonly IExpeditionController _expeditionController;
        [Inject] private readonly ExpeditionMenuManager _expeditionMenuManager;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            SetState();
        }

        #endregion
        
        public override void SetState()
        {
            SetInteractable(!_expeditionMenuManager.Units.IsAllUnitEmpty());
        }

        protected override void Click()
        {
            base.Click();

            var activeLocation = _expeditionMenuManager.Locations.ActiveLocation;
            _expeditionController.CurrentBattleLocation = activeLocation.Data;

            var allyUnits = _expeditionMenuManager.Units.GetUnitsWithData();
            var allyKey = allyUnits.Select(x => x.Data.Key).ToList();
            var expeditionTime = activeLocation.Data.Time;
            var expedition = new ExpeditionObject
            {
                Id = Guid.NewGuid(),
                Key = activeLocation.Data.Key,
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