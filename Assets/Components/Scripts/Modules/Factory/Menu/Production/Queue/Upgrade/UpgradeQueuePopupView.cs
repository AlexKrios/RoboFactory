using RoboFactory.General.Item.Production;
using RoboFactory.General.Level;
using RoboFactory.General.Localisation;
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
    [AddComponentMenu("Scripts/Factory/Menu/Production/Queue/Upgrade Queue Popup View")]
    public class UpgradeQueuePopupView : PopupBase
    {
        #region Zenject

        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly MoneyManager _moneyManager;
        [Inject] private readonly LevelManager _levelManager;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ProductionManager _productionManager;

        #endregion
        
        #region Components
        
        [Space]
        [SerializeField] private TMP_Text titleText;

        [Space]
        [SerializeField] private TMP_Text currentCount;
        [SerializeField] private TMP_Text nextCount;

        [Header("Accept Button")]
        [SerializeField] private Button acceptButton;
        [SerializeField] private TMP_Text acceptLevelText;
        [SerializeField] private TMP_Text acceptMoneyText;

        #endregion

        #region Variables

        private UpgradeDataObject _buyData;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            _uiController.AddUi(this);
            
            acceptButton.OnClickAsObservable().Subscribe(_ => OnAcceptClick()).AddTo(Disposable);

            var cellCount = _productionManager.CellCount;
            currentCount.text = cellCount.ToString();
            nextCount.text = (cellCount + 1).ToString();
            
            titleText.text = _localisationController.GetLanguageValue(LocalisationKeys.UpgradeTitleKey);
        }
        
        private void Start() 
        {
            _buyData = _productionManager.GetUpgradeQueueData();
            
            if (_moneyManager.Money < _buyData.Cost || _levelManager.Level < _buyData.Level)
                acceptButton.interactable = false;
            
            acceptLevelText.gameObject.SetActive(_levelManager.Level < _buyData.Level);
            acceptLevelText.text = $"Need level: {_buyData.Level}";
            acceptMoneyText.text = $"<sprite name=MoneyIcon> {_buyData.Cost}";
        }

        #endregion
        
        private async void OnAcceptClick()
        {
            await _moneyManager.MinusMoney(_buyData.Cost);
            await _productionManager.IncreaseQueueCount();

            Close();
        }
    }
}
