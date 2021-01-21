using System.Collections;
using Modules.General.Asset;
using Modules.General.Coroutines;
using Modules.General.Item.Production;
using Modules.General.Item.Products;
using UnityEngine;
using Utils;
using Zenject;

namespace Modules.Factory.Menu.Production.Queue.Cell.State
{
    public class ProductionCellBusy : IProductionCellState
    {
        #region Zenject

        [Inject] private readonly ICoroutinesController _coroutinesController;
        [Inject] private readonly IProductsController _productsController;
        [Inject] private readonly IProductionController _productionController;

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
            var craftObj = _productionController.GetProduction(_cell.Data.Id);
            var craftItem = _productsController.GetProduct(craftObj.Key);

            var sprite = await AssetsController.LoadAsset<Sprite>(craftItem.IconRef);
            _cell.SetCellIcon(sprite);
            _cell.Data.Timer = _coroutinesController.StartNewCoroutine(StartProductionTimer());
        }

        public void Click() { }

        public void Exit() { }
        
        private IEnumerator StartProductionTimer()
        {
            var productionTime = DateUtil.GetTime(_cell.Data.TimeEnd);
            if (productionTime == 0)
            {
                yield return new WaitForSeconds(0.1f);
                _cell.SetStateFinish();
                yield break;
            }

            while (productionTime > 0)
            {
                _cell.SetCellTimer(DateUtil.DateCraftTimer(productionTime));
                yield return new WaitForSeconds(1.0f);
                productionTime--;
            }
            
            _cell.SetStateFinish();
        }

        public class Factory : PlaceholderFactory<ProductionCell, ProductionCellBusy> { }
    }
}
