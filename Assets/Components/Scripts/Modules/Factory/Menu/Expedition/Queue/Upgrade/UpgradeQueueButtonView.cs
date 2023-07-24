using System;
using RoboFactory.General.Expedition;
using RoboFactory.General.Level;
using RoboFactory.General.Localization;
using RoboFactory.General.Money;
using RoboFactory.General.Scriptable;
using RoboFactory.General.Ui.Common;
using TMPro;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    public class UpgradeQueueButtonView : ButtonBase
    {
        [Inject] private readonly LocalizationService localizationController;
        [Inject] private readonly MoneyManager _moneyManager;
        [Inject] private readonly LevelManager _levelManager;
        [Inject] private readonly ExpeditionManager _expeditionManager;

        [Space]
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _costCount;
        
        private UpgradeDataObject _buyData;

        public Action OnUpgradeClick { get; set; }
        
        protected override void Awake()
        {
            base.Awake();

            SetButtonText(localizationController.GetLanguageValue(LocalizationKeys.UpgradeButtonTitleKey));
            _buyData = _expeditionManager.GetUpgradeData();
            
            SetState();
        }

        protected override async void Click()
        {
            base.Click();
            
            await _moneyManager.MinusMoney(_buyData.Cost);
            _expeditionManager.IncreaseQueueCount();
            
            OnUpgradeClick?.Invoke();
        }

        public void SetData(int costValue, int levelValue)
        {
            if (_levelManager.Level > levelValue)
            {
                _title.text = $"Level {levelValue}";
                SetInteractable(false);
                return;
            }
            
            _costCount.text = costValue.ToString();
        }
    }
}
