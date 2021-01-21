using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Ui;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Queue.Cell.State
{
    public class ExpeditionCellFinish : IExpeditionCellState
    {
        #region Zenject
        
        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ExpeditionMenuFactory _expeditionMenuFactory;

        #endregion
        
        #region Variables

        private readonly ExpeditionCell _cell;

        #endregion

        public ExpeditionCellFinish(ExpeditionCell cell)
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
            _cell.SetStateEmpty();
            
            var parent = _uiController.FindCanvas(CanvasType.Menu);
            _expeditionMenuFactory.CreateResultPopup(parent.transform, _cell.Data);
        }

        public void Exit() { }

        public class Factory : PlaceholderFactory<ExpeditionCell, ExpeditionCellFinish> { }
    }
}
