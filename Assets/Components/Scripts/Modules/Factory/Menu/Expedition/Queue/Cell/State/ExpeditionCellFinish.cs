using JetBrains.Annotations;
using RoboFactory.General.Localization;
using RoboFactory.General.Ui;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    public class ExpeditionCellFinish : IExpeditionCellState
    {
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ExpeditionMenuFactory _expeditionMenuFactory;
        
        private readonly ExpeditionCell _cell;

        public ExpeditionCellFinish(ExpeditionCell cell)
        {
            _cell = cell;
        }

        public void Enter()
        {
            var text = _localizationService.GetLanguageValue(LocalizationKeys.ProductionCompleteKey);
            _cell.SetCellTimer(text);
        }

        public void Click()
        {
            _cell.SetStateEmpty();
            
            var parent = _uiController.GetCanvas(CanvasType.Ui);
            _expeditionMenuFactory.CreateResultPopup(parent.transform, _cell.Data);
        }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ExpeditionCell, ExpeditionCellFinish> { }
    }
}
