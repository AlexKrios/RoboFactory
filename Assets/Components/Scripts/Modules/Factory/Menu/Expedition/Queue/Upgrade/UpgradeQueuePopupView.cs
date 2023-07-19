using RoboFactory.General.Expedition;
using RoboFactory.General.Localisation;
using RoboFactory.General.Scriptable;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using TMPro;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Expedition
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Queue/Upgrade Queue Popup View")]
    public class UpgradeQueuePopupView : PopupBase
    {
        #region Zenject

        [Inject] private readonly LocalizationService localizationController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ExpeditionManager expeditionManager;

        #endregion
        
        #region Components
        
        [Space]
        [SerializeField] private TMP_Text _titleText;

        [Space]
        [SerializeField] private TMP_Text _currentCount;
        [SerializeField] private TMP_Text _nextCount;
        
        [Space]
        [SerializeField] private UpgradeQueueButtonView _upgrade;

        #endregion

        #region Variables

        private UpgradeDataObject _buyData;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            _uiController.AddUi(this);

            _upgrade.OnUpgradeClick += Close;
            
            var cellCount = expeditionManager.CellCount;
            _currentCount.text = cellCount.ToString();
            _nextCount.text = (cellCount + 1).ToString();
            
            _titleText.text = localizationController.GetLanguageValue(LocalizationKeys.UpgradeTitleKey);
        }
        
        private void Start() 
        {
            _buyData = expeditionManager.GetUpgradeData();
            _upgrade.SetData(_buyData.Cost, _buyData.Level);
        }

        #endregion
    }
}
