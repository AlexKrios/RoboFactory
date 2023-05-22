using Components.Scripts.Modules.General.Asset;
using Components.Scripts.Modules.General.Item.Production;
using Components.Scripts.Modules.General.Item.Products;
using Components.Scripts.Utils;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Production.Queue.Cell.State
{
    public class ProductionCellBusy : IProductionCellState
    {
        #region Zenject

        [Inject] private readonly ProductsManager _productsManager;
        [Inject] private readonly ProductionManager _productionManager;

        #endregion

        #region Variables

        private readonly ProductionCell _cell;

        #endregion
        
        public ProductionCellBusy(ProductionCell cell)
        {
            _cell = cell;
        }

        public async void Enter()
        {
            var craftObj = _productionManager.GetProduction(_cell.Data.Id);
            var craftItem = _productsManager.GetProduct(craftObj.Key);

            var sprite = await AssetsManager.LoadAsset<Sprite>(craftItem.IconRef);
            _cell.SetCellIcon(sprite);
            StartProductionTimer();
        }

        public void Click() { }

        public void Exit() { }
        
        private async void StartProductionTimer()
        {
            var productionTime = DateUtil.GetTime(_cell.Data.TimeEnd);
            if (productionTime == 0)
            {
                _cell.SetStateFinish();
                return;
            }

            while (productionTime > 0)
            {
                _cell.SetCellTimer(DateUtil.DateCraftTimer(productionTime));
                await UniTask.Delay(1000);
                productionTime--;
            }
            
            _cell.SetStateFinish();
        }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ProductionCell, ProductionCellBusy> { }
    }
}
