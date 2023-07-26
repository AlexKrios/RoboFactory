using JetBrains.Annotations;
using RoboFactory.General.Item.Production;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Localization;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    public class ProductionCellFinish : IProductionCellState
    {
        #region Zenject
        
        [Inject] private readonly LocalizationService localizationController;
        [Inject] private readonly ProductsService productsService;
        [Inject] private readonly ProductionService productionService;

        #endregion
        
        #region Variables

        private readonly ProductionCell _cell;

        #endregion

        public ProductionCellFinish(ProductionCell cell)
        {
            _cell = cell;
        }

        public void Enter()
        {
            var text = localizationController.GetLanguageValue(LocalizationKeys.ProductionCompleteKey);
            _cell.SetCellTimer(text);
        }

        public void Click()
        {
            CompleteCraft();

            _cell.SetStateEmpty();
        }

        public void Exit() { }

        private void CompleteCraft()
        {
            var craftItem = productionService.GetProduction(_cell.Data.Id);
            
            productsService.CreateProduct(craftItem.Key);
            productionService.RemoveProduction(_cell.Data.Id);
            _cell.ResetCell();
        }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ProductionCell, ProductionCellFinish> { }
    }
}
