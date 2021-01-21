using System;
using DG.Tweening;
using Modules.General.Level;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Location;
using Modules.General.Money;
using Modules.General.Save;
using Modules.General.Scriptable;
using Modules.General.Ui.Common;
using TMPro;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Queue.Upgrade
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Queue/Upgrade Queue Button View")]
    public class UpgradeQueueButtonView : ButtonBase
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IMoneyController _moneyController;
        [Inject] private readonly ILevelController _levelController;
        [Inject] private readonly ISaveController _saveController;
        [Inject] private readonly IExpeditionController _expeditionController;
        [Inject] private readonly FactoryUi _factoryUi;

        #endregion

        #region Components

        [Space]
        [SerializeField] private TextMeshProUGUI title;
        
        [Space]
        [SerializeField] private RectTransform cost;
        [SerializeField] private TextMeshProUGUI costCount;

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
            _buyData = _expeditionController.GetUpgradeData();
            
            SetState();
        }

        #endregion

        protected override void Click()
        {
            base.Click();
            
            _moneyController.MinusMoney(_buyData.Cost);
            _factoryUi.Production.IncreaseCellCount();
            
            _saveController.SaveProduction();
            OnUpgradeClick?.Invoke();
        }

        public void SetData(int costValue, int levelValue)
        {
            if (_levelController.Level > levelValue)
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
