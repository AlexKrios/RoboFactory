using Components.Scripts.Modules.General.Asset;
using Components.Scripts.Modules.General.Expedition;
using Components.Scripts.Modules.General.Location;
using Components.Scripts.Utils;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Expedition.Queue.Cell.State
{
    public class ExpeditionCellBusy : IExpeditionCellState
    {
        #region Zenject
        
        [Inject] private readonly LocationManager _locationManager;
        [Inject] private readonly ExpeditionManager _expeditionManager;

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
            var expedition = _expeditionManager.GetExpedition(_cell.Data.Id);
            var location = _locationManager.GetLocation(expedition.Key);

            var sprite = await AssetsManager.LoadAsset<Sprite>(location.IconRef);
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
