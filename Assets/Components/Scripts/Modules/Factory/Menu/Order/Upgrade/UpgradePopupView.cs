using RoboFactory.General.Level;
using RoboFactory.General.Localization;
using RoboFactory.General.Money;
using RoboFactory.General.Order;
using RoboFactory.General.Scriptable;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Order
{
    public class UpgradePopupView : PopupBase
    {
        private const string NeedLevelTextKey = "button_need_level";
        private const string NeedMoneyTextKey = "<sprite name=MoneyIcon>";
        
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly MoneyService _moneyService;
        [Inject] private readonly ExperienceService _experienceService;
        [Inject] private readonly OrderService orderService;

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
            _buyData = orderService.GetUpgradeQualityData();
            
            _titleText.text = _localizationService.GetLanguageValue(LocalizationKeys.UpgradeTitleKey);
            
            _currentLevel.text = orderService.Level.ToString();
            _nextLevel.text = (orderService.Level + 1).ToString();
            
            if (_moneyService.Money.Value < _buyData.Cost || _experienceService.Level < _buyData.Level)
                _acceptButton.interactable = false;

            if (_experienceService.Level < _buyData.Level)
                _acceptText.text = $"{_localizationService.GetLanguageValue(NeedLevelTextKey)} {_buyData.Level}";
            else if (_moneyService.Money.Value < _buyData.Cost)
                _acceptText.text = $"{NeedMoneyTextKey} {_buyData.Cost}";
        }

        private async void OnAcceptClick()
        {
            await _moneyService.MinusMoney(_buyData.Cost);
            await orderService.IncreaseOrderLevel();

            Close();
        }
    }
}
