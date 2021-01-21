using Modules.General.Item.Production;
using Modules.General.Item.Products;
using Modules.General.Level;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Save;
using Zenject;

namespace Modules.Factory.Menu.Production.Queue.Cell.State
{
    public class ProductionCellFinish : IProductionCellState
    {
        #region Zenject

        //[Inject] private readonly ResultCanvas.Factory craftResultFactory;

        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly ILevelController _levelController;
        [Inject] private readonly IProductsController _productsController;
        [Inject] private readonly IProductionController _productionController;
        [Inject] private readonly ISaveController _saveController;

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
            var text = _localisationController.GetLanguageValue(LocalisationKeys.ProductionCompleteKey);
            _cell.SetCellTimer(text);
        }

        public void Click()
        {
            //var craftItem = craftController.GetCraft(_craftCell.Id);

            CompleteCraft();
            //craftResultFactory.Create(craftItem);

            _cell.SetStateEmpty();
        }

        public void Exit() { }

        private void CompleteCraft()
        {
            var craftItem = _productionController.GetProduction(_cell.Data.Id);

            var product = _productsController.GetProduct(craftItem.Key);
            _levelController.SetExperience(product.Recipe.Experience);
            product.IncrementCount();
            product.IncrementExperience();

            _productionController.RemoveProduction(_cell.Data.Id);
            _saveController.SaveProduction();
            _saveController.SaveStores();

            _cell.ResetCell();
        }

        public class Factory : PlaceholderFactory<ProductionCell, ProductionCellFinish> { }
    }
}
