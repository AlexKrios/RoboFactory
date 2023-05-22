using System;
using Components.Scripts.Modules.General.Item.Production;
using Components.Scripts.Modules.General.Level;
using Components.Scripts.Modules.General.Money;
using Components.Scripts.Modules.General.Scriptable;
using Components.Scripts.Modules.General.Ui.Common;
using TMPro;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Production.Queue.Upgrade
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Queue/Upgrade Queue Button View")]
    public class UpgradeQueueButtonView : ButtonBase
    {
        #region Zenject
        
        [Inject] private readonly ProductionManager _productionManager;
        [Inject] private readonly MoneyManager _moneyManager;
        [Inject] private readonly LevelManager _levelManager;

        #endregion
        
        #region Components
        
        [SerializeField] private TMP_Text cost;

        #endregion

        #region Variables
        
        private UpgradeDataObject _buyData;

        public Action OnUpgradeClick { get; set; }

        #endregion
        
        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            _buyData = _productionManager.GetUpgradeQueueData();
        }

        #endregion

        protected override async void Click()
        {
            base.Click();
            
            await _moneyManager.MinusMoney(_buyData.Cost);
            _productionManager.IncreaseQueueCount();

            OnUpgradeClick?.Invoke();
        }

        public void SetData(int costValue, int levelValue)
        {
            if (_levelManager.Level > levelValue)
            {
                SetButtonText($"Level {levelValue}");
                SetInteractable(false);
                return;
            }
            
            if (_moneyManager.Money < _buyData.Cost)
            {
                SetInteractable(false);
            }
            
            cost.gameObject.SetActive(true);
            cost.text = costValue.ToString();
        }
    }
}