using System;
using DG.Tweening;
using RoboFactory.General.Expedition;
using RoboFactory.General.Level;
using RoboFactory.General.Localisation;
using RoboFactory.General.Money;
using RoboFactory.General.Scriptable;
using RoboFactory.General.Ui.Common;
using TMPro;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Queue/Upgrade Queue Button View")]
    public class UpgradeQueueButtonView : ButtonBase
    {
        #region Zenject

        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly MoneyManager _moneyManager;
        [Inject] private readonly LevelManager _levelManager;
        [Inject] private readonly ExpeditionManager _expeditionManager;

        #endregion

        #region Components

        [Space]
        [SerializeField] private TMP_Text title;
        
        [Space]
        [SerializeField] private RectTransform cost;
        [SerializeField] private TMP_Text costCount;

        #endregion
        
        #region Variables
        
        private UpgradeDataObject _buyData;

        public Action OnUpgradeClick { get; set; }

        #endregion
        
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            SetButtonText(_localisationController.GetLanguageValue(LocalisationKeys.UpgradeButtonTitleKey));
            _buyData = _expeditionManager.GetUpgradeData();
            
            SetState();
        }

        #endregion

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
                title.text = $"Level {levelValue}";
                SetInteractable(false);
                return;
            }

            cost.DOAnchorPosY(cost.anchoredPosition.y + 60, 0.5f);
            cost.gameObject.SetActive(true);
            costCount.text = costValue.ToString();
        }
    }
}
