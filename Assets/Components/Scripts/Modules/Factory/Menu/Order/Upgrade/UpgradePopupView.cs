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

namespace RoboFactory.Factory.Menu.Order
{
    [AddComponentMenu("Scripts/Factory/Menu/Order/Upgrade/Upgrade Popup View")]
    public class UpgradePopupView : PopupBase
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
        [SerializeField] private TMP_Text currentLevel;
        [SerializeField] private TMP_Text nextLevel;
        
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

            currentLevel.text = _productionManager.Level.ToString();
            nextLevel.text = (_productionManager.Level + 1).ToString();

            titleText.text = _localisationController.GetLanguageValue(LocalisationKeys.UpgradeTitleKey);
        }
        
        private void Start() 
        {
            _buyData = _productionManager.GetUpgradeQualityData();
            
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
            await _productionManager.AddLevel();

            Close();
        }
    }
}
