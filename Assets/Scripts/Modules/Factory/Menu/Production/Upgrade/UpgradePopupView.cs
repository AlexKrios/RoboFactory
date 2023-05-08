using Modules.General.Item.Production;
using Modules.General.Level;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Money;
using Modules.General.Save;
using Modules.General.Scriptable;
using Modules.General.Ui.Common.Menu;
using TMPro;
using UniRx;
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
        [Inject] private readonly ISaveController _saveController;
        [Inject] private readonly IMoneyController _moneyController;
        [Inject] private readonly ILevelController _levelController;
        [Inject] private readonly IProductionController _productionController;

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
            
            acceptButton.OnClickAsObservable().Subscribe(_ => OnAcceptClick()).AddTo(Disposable);

            currentLevel.text = _productionController.Level.ToString();
            nextLevel.text = (_productionController.Level + 1).ToString();

            titleText.text = _localisationController.GetLanguageValue(LocalisationKeys.UpgradeTitleKey);
        }
        
        private void Start() 
        {
            _buyData = _productionController.GetUpgradeQualityData();
            
            if (_moneyController.Money < _buyData.Cost || _levelController.Level < _buyData.Level)
                acceptButton.interactable = false;
            
            acceptLevelText.gameObject.SetActive(_levelController.Level < _buyData.Level);
            acceptLevelText.text = $"Need level: {_buyData.Level}";
            acceptMoneyText.text = $"<sprite name=MoneyIcon> {_buyData.Cost}";
        }

        #endregion

        private void OnAcceptClick()
        {
            _moneyController.MinusMoney(_buyData.Cost);
            _productionController.Level++;
            _saveController.SaveProduction();
            
            Close();
        }
    }
}
