using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Asset;
using RoboFactory.General.Expedition;
using RoboFactory.General.Location;
using RoboFactory.Utils;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    public class ExpeditionCellBusy : IExpeditionCellState
    {
        [Inject] private readonly AddressableService _addressableService;
        [Inject] private readonly LocationManager _locationManager;
        [Inject] private readonly ExpeditionManager _expeditionManager;

        private readonly ExpeditionCell _cell;
        
        public ExpeditionCellBusy(ExpeditionCell cell)
        {
            _cell = cell;
        }

        public async void Enter()
        {
            var expedition = _expeditionManager.GetExpedition(_cell.Data.Id);
            var location = _locationManager.GetLocation(expedition.Key);

            var sprite = await _addressableService.LoadAssetAsync<Sprite>(location.IconRef);
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
