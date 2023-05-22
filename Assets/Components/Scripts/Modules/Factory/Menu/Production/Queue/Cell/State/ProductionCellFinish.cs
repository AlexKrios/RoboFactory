﻿using Components.Scripts.Modules.General.Item.Production;
using Components.Scripts.Modules.General.Item.Products;
using Components.Scripts.Modules.General.Localisation;
using JetBrains.Annotations;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Production.Queue.Cell.State
{
    public class ProductionCellFinish : IProductionCellState
    {
        #region Zenject
        
        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly ProductsManager _productsManager;
        [Inject] private readonly ProductionManager _productionManager;

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
            CompleteCraft();

            _cell.SetStateEmpty();
        }

        public void Exit() { }

        private void CompleteCraft()
        {
            var craftItem = _productionManager.GetProduction(_cell.Data.Id);
            
            _productsManager.CreateProduct(craftItem.Key);
            _productionManager.RemoveProduction(_cell.Data.Id);
            _cell.ResetCell();
        }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ProductionCell, ProductionCellFinish> { }
    }
}
