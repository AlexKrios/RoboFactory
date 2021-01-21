using Zenject;

namespace Modules.Factory.Menu.Expedition.Queue.Cell.State
{
    public class ExpeditionCellEmpty : IExpeditionCellState
    {
        private readonly ExpeditionCell _cell;

        public ExpeditionCellEmpty(ExpeditionCell cell)
        {
            _cell = cell;
        }

        public void Enter()
        {
            _cell.ResetCell();
        }

        public void Click() { }

        public void Exit() { }

        public class Factory : PlaceholderFactory<ExpeditionCell, ExpeditionCellEmpty> { }
    }
}
