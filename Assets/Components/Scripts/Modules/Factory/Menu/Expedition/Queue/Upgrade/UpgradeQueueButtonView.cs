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
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly MoneyService _moneyService;
        [Inject] private readonly ExperienceService _experienceService;
        [Inject] private readonly ExpeditionService expeditionService;

        [Space]
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _costCount;
        
        private UpgradeDataObject _buyData;

        public Action OnUpgradeClick { get; set; }
        
        protected override void Awake()
        {
            base.Awake();

            SetButtonText(_localizationService.GetLanguageValue(LocalizationKeys.UpgradeButtonTitleKey));
            _buyData = expeditionService.GetUpgradeData();
            
            SetState();
        }

        protected override async void Click()
        {
            base.Click();
            
            await _moneyService.MinusMoney(_buyData.Cost);
            expeditionService.IncreaseQueueCount();
            
            OnUpgradeClick?.Invoke();
        }

        public void SetData(int costValue, int levelValue)
        {
            if (_experienceService.Level > levelValue)
            {
                _title.text = $"Level {levelValue}";
                SetInteractable(false);
                return;
            }
            
            _costCount.text = costValue.ToString();
        }
    }
}
