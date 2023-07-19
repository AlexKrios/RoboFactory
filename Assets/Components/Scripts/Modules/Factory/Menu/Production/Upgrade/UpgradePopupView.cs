using RoboFactory.General.Item.Production;
using RoboFactory.General.Level;
using RoboFactory.General.Localisation;
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
    [AddComponentMenu("Scripts/Factory/Menu/Production/Upgrade/Upgrade Popup View")]
    public class UpgradePopupView : PopupBase
    {
        private const string NeedLevelTextKey = "button_need_level";
        private const string NeedMoneyTextKey = "<sprite name=MoneyIcon>";
        
        #region Zenject

        [Inject] private readonly LocalizationService localizationService;
        [Inject] private readonly MoneyManager _moneyManager;
        [Inject] private readonly LevelManager _levelManager;
        [Inject] private readonly ProductionManager _productionManager;

        #endregion

        #region Components
        
        [Space]
        [SerializeField] private TMP_Text titleText;

        [Space]
        [SerializeField] private TMP_Text currentLevel;
        [SerializeField] private TMP_Text nextLevel;
        
        [Header("Accept Button")]
        [SerializeField] private Button acceptButton;
        [SerializeField] private TMP_Text acceptText;

        #endregion
        
        #region Variables

        private UpgradeDataObject _buyData;

        #endregion
        
        public override void Initialize()
        {
            base.Initialize();
            
            acceptButton.OnClickAsObservable().Subscribe(_ => OnAcceptClick()).AddTo(Disposable);
            _buyData = _productionManager.GetUpgradeQualityData();
            
            currentLevel.text = _productionManager.Level.ToString();
            nextLevel.text = (_productionManager.Level + 1).ToString();

            titleText.text = localizationService.GetLanguageValue(LocalizationKeys.UpgradeTitleKey);

            if (_moneyManager.Money < _buyData.Cost || _levelManager.Level < _buyData.Level)
                acceptButton.interactable = false;

            if (_levelManager.Level < _buyData.Level)
                acceptText.text = $"{localizationService.GetLanguageValue(NeedLevelTextKey)} {_buyData.Level}";
            else if (_moneyManager.Money < _buyData.Cost)
                acceptText.text = $"{NeedMoneyTextKey} {_buyData.Cost}";
        }

        private async void OnAcceptClick()
        {
            await _moneyManager.MinusMoney(_buyData.Cost);
            await _productionManager.AddLevel();

            Close();
        }
    }
}
