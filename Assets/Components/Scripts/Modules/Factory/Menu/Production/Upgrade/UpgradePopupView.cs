using RoboFactory.General.Item.Production;
using RoboFactory.General.Level;
using RoboFactory.General.Localization;
using RoboFactory.General.Money;
using RoboFactory.General.Scriptable;
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
        [Inject] private readonly MoneyManager _moneyManager;
        [Inject] private readonly LevelManager _levelManager;
        [Inject] private readonly ProductionManager _productionManager;

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
            
            _acceptButton.OnClickAsObservable().Subscribe(_ => OnAcceptClick()).AddTo(Disposable);
            _buyData = _productionManager.GetUpgradeQualityData();
            
            _titleText.text = _localizationService.GetLanguageValue(LocalizationKeys.UpgradeTitleKey);
            
            _currentLevel.text = _productionManager.Level.ToString();
            _nextLevel.text = (_productionManager.Level + 1).ToString();

            if (_moneyManager.Money < _buyData.Cost || _levelManager.Level < _buyData.Level)
                _acceptButton.interactable = false;

            if (_levelManager.Level < _buyData.Level)
                _acceptText.text = $"{_localizationService.GetLanguageValue(NeedLevelTextKey)} {_buyData.Level}";
            else if (_moneyManager.Money < _buyData.Cost)
                _acceptText.text = $"{NeedMoneyTextKey} {_buyData.Cost}";
        }

        private async void OnAcceptClick()
        {
            await _moneyManager.MinusMoney(_buyData.Cost);
            await _productionManager.AddLevel();

            Close();
        }
    }
}
