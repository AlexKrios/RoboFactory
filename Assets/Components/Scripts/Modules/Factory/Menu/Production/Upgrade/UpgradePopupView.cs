using RoboFactory.General.Item.Production;
using RoboFactory.General.Level;
using RoboFactory.General.Localization;
using RoboFactory.General.Money;
using RoboFactory.General.Scriptable;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    public class UpgradePopupView : PopupBase
    {
        private const string NeedLevelTextKey = "button_need_level";
        private const string NeedMoneyTextKey = "<sprite name=MoneyIcon>";
        
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly MoneyService _moneyService;
        [Inject] private readonly ExperienceService _experienceService;
        [Inject] private readonly ProductionService productionService;

        [Space]
        [SerializeField] private TMP_Text _titleText;

        [Space]
        [SerializeField] private TMP_Text _currentLevel;
        [SerializeField] private TMP_Text _nextLevel;
        
        [Header("Accept Button")]
        [SerializeField] private Button _acceptButton;
        [SerializeField] private TMP_Text _acceptText;
        
        private UpgradeDataObject _buyData;
        
        public override void Initialize()
        {
            base.Initialize();
            
            _uiController.AddUi(this);

            _acceptButton.OnClickAsObservable().Subscribe(_ => OnAcceptClick()).AddTo(Disposable);
            _buyData = productionService.GetUpgradeQualityData();
            
            _titleText.text = _localizationService.GetLanguageValue(LocalizationKeys.UpgradeTitleKey);
            
            _currentLevel.text = productionService.Level.ToString();
            _nextLevel.text = (productionService.Level + 1).ToString();

            if (_moneyService.Money.Value < _buyData.Cost || _experienceService.Level < _buyData.Level)
                _acceptButton.interactable = false;

            _acceptText.text = _experienceService.Level < _buyData.Level 
                ? $"{_localizationService.GetLanguageValue(NeedLevelTextKey)} {_buyData.Level}" 
                : $"{NeedMoneyTextKey} {_buyData.Cost}";
        }

        private async void OnAcceptClick()
        {
            await _moneyService.MinusMoney(_buyData.Cost);
            await productionService.AddLevel();

            Close();
        }
    }
}
