using JetBrains.Annotations;
using RoboFactory.General.Localization;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    public class ExpeditionCellFinish : IExpeditionCellState
    {
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly ExpeditionMenuFactory _expeditionMenuFactory;
        [Inject(Id = Constants.PopupsParentKey)] private readonly Transform _popupsParent;
        
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
            _expeditionMenuFactory.CreateResultPopup(_popupsParent, _cell.Data);
        }

        public void Exit() { }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<ExpeditionCell, ExpeditionCellFinish> { }
    }
}
