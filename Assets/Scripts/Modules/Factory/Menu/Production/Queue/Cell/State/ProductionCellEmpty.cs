using Zenject;

namespace Modules.Factory.Menu.Production.Queue.Cell.State
{
    public class ProductionCellEmpty : IProductionCellState
    {
        private readonly ProductionCell _cell;

        public ProductionCellEmpty(ProductionCell cell)
        {
            _cell = cell;
        }

        public void Enter()
        {
            _cell.ResetCell();
        }

        public void Click() { }

        public void Exit() { }

        public class Factory : PlaceholderFactory<ProductionCell, ProductionCellEmpty> { }
    }
}
