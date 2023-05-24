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

        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ExpeditionManager expeditionManager;

        #endregion
        
        #region Components
        
        [Space]
        [SerializeField] private TMP_Text titleText;

        [Space]
        [SerializeField] private TMP_Text currentCount;
        [SerializeField] private TMP_Text nextCount;
        
        [Space]
        [SerializeField] private UpgradeQueueButtonView upgrade;

        #endregion

        #region Variables

        private UpgradeDataObject _buyData;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            _uiController.AddUi(this);

            upgrade.OnUpgradeClick += Close;
            
            var cellCount = expeditionManager.CellCount;
            currentCount.text = cellCount.ToString();
            nextCount.text = (cellCount + 1).ToString();
            
            titleText.text = _localisationController.GetLanguageValue(LocalisationKeys.UpgradeTitleKey);
        }
        
        private void Start() 
        {
            _buyData = expeditionManager.GetUpgradeData();
            upgrade.SetData(_buyData.Cost, _buyData.Level);
        }

        #endregion
    }
}
