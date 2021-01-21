using Modules.General.Audio;
using Modules.General.Audio.Models;
using Modules.General.Item.Production;
using Modules.General.Level;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Money;
using Modules.General.Save;
using Modules.General.Scriptable;
using Modules.General.Ui;
using Modules.General.Ui.Popup.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Production.Upgrade
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Upgrade/Upgrade Popup View")]
    public class UpgradePopupView : PopupBase
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IAudioController _audioController;
        [Inject] private readonly ISaveController _saveController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly IMoneyController _moneyController;
        [Inject] private readonly ILevelController _levelController;
        [Inject] private readonly IProductionController _productionController;
        [Inject] private readonly ProductionMenuManager _productionMenuManager;

        #endregion

        #region Components
        
        [Space]
        [SerializeField] private Button close;
        
        [Space]
        [SerializeField] private TextMeshProUGUI titleText;

        [Space]
        [SerializeField] private TextMeshProUGUI currentLevel;
        [SerializeField] private TextMeshProUGUI nextLevel;
        
        [Header("Accept Button")]
        [SerializeField] private Button acceptButton;
        [SerializeField] private TMP_Text acceptLevelText;
        [SerializeField] private TMP_Text acceptMoneyText;

        #endregion
        
        #region Variables

        private UpgradeDataObject _buyData;
        
        private int ActiveStar => _productionMenuManager.Star.ActiveStar;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            _uiController.AddUi(type, gameObject);
            
            close.onClick.AddListener(Close);
            acceptButton.onClick.AddListener(OnAcceptClick);

            currentLevel.text = _productionController.Level.ToString();
            nextLevel.text = (_productionController.Level + 1).ToString();

            titleText.text = _localisationController.GetLanguageValue(LocalisationKeys.UpgradeTitleKey);
        }
        
        private void Start() 
        {
            _buyData = _productionController.GetUpgradeData();
            if (_levelController.Level >= _buyData.Level)
                acceptLevelText.text = _buyData.Level.ToString();
            else
                acceptLevelText.gameObject.SetActive(false);
            
            acceptMoneyText.text = $"<sprite name=MoneyIcon> {_buyData.Cost}";

            PlayFadeIn();
        }

        private void OnDestroy()
        {
            close.onClick.RemoveListener(Close);
            acceptButton.onClick.RemoveListener(OnAcceptClick);
        }

        #endregion

        private void OnAcceptClick()
        {
            if (_moneyController.Money >= _buyData.Cost && _levelController.Level >= _buyData.Level)
                return;
            
            _audioController.PlayAudio(AudioClipType.ButtonClick);
            _productionController.Level++;
            _saveController.SaveProduction();
            _uiController.RemoveUi(type);
        }
    }
}
