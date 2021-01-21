using System.Collections;
using Modules.General.Asset;
using Modules.General.Coroutines;
using Modules.General.Location;
using UnityEngine;
using Utils;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Queue.Cell.State
{
    public class ExpeditionCellBusy : IExpeditionCellState
    {
        #region Zenject

        [Inject] private readonly ICoroutinesController _coroutinesController;
        [Inject] private readonly IExpeditionController _expeditionController;

        #endregion

        #region Variables

        private readonly ExpeditionCell _cell;

        #endregion
        
        public ExpeditionCellBusy(ExpeditionCell cell)
        {
            _cell = cell;
        }

        public async void Enter()
        {
            var expedition = _expeditionController.GetExpedition(_cell.Data.Id);
            var location = _expeditionController.GetLocation(expedition.Key);

            var sprite = await AssetsController.LoadAsset<Sprite>(location.IconRef);
            _cell.SetCellIcon(sprite);
            _cell.Data.Timer = _coroutinesController.StartNewCoroutine(StartExpeditionTimer());
        }

        public void Click() { }

        public void Exit() { }
        
        private IEnumerator StartExpeditionTimer()
        {
            var expeditionTime = DateUtil.GetTime(_cell.Data.TimeEnd);
            if (expeditionTime == 0)
            {
                yield return new WaitForSeconds(0.1f);
                _cell.SetStateFinish();
                yield break;
            }

            while (expeditionTime > 0)
            {
                _cell.SetCellTimer(DateUtil.DateCraftTimer(expeditionTime));
                yield return new WaitForSeconds(1.0f);
                expeditionTime--;
            }
            
            _cell.SetStateFinish();
        }

        public class Factory : PlaceholderFactory<ExpeditionCell, ExpeditionCellBusy> { }
    }
}
