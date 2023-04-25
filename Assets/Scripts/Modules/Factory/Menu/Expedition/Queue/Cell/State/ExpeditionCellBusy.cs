using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Modules.General.Asset;
using Modules.General.Location;
using UnityEngine;
using Utils;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Queue.Cell.State
{
    public class ExpeditionCellBusy : IExpeditionCellState
    {
        #region Zenject
        
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
            StartExpeditionTimer();
        }

        public void Click() { }

        public void Exit() { }
        
        private async void StartExpeditionTimer()
        {
            var expeditionTime = DateUtil.GetTime(_cell.Data.TimeEnd);
            if (expeditionTime == 0)
            {
                _cell.SetStateFinish();
                return;
            }

            while (expeditionTime > 0)
            {
                _cell.SetCellTimer(DateUtil.DateCraftTimer(expeditionTime));
                await UniTask.Delay(1000);
                expeditionTime--;
            }
            
            _cell.SetStateFinish();
        }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ExpeditionCell, ExpeditionCellBusy> { }
    }
}
