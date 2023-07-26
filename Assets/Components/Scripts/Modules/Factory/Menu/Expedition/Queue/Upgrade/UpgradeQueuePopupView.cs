using RoboFactory.General.Expedition;
using RoboFactory.General.Localization;
using RoboFactory.General.Scriptable;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using TMPro;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    public class UpgradeQueuePopupView : PopupBase
    {
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ExpeditionService expeditionService;
        
        [Space]
        [SerializeField] private TMP_Text _titleText;

        [Space]
        [SerializeField] private TMP_Text _currentCount;
        [SerializeField] private TMP_Text _nextCount;
        
        [Space]
        [SerializeField] private UpgradeQueueButtonView _upgrade;

        private UpgradeDataObject _buyData;

        protected override void Awake()
        {
            base.Awake();
            
            _uiController.AddUi(this);

            _upgrade.OnUpgradeClick += Close;
            
            var cellCount = expeditionService.CellCount;
            _currentCount.text = cellCount.ToString();
            _nextCount.text = (cellCount + 1).ToString();
            
            _titleText.text = _localizationService.GetLanguageValue(LocalizationKeys.UpgradeTitleKey);
        }
        
        private void Start() 
        {
            _buyData = expeditionService.GetUpgradeData();
            _upgrade.SetData(_buyData.Cost, _buyData.Level);
        }
    }
}
