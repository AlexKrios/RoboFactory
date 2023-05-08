using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Location;
using Modules.General.Scriptable;
using Modules.General.Ui;
using Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Queue.Upgrade
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Queue/Upgrade Queue Popup View")]
    public class UpgradeQueuePopupView : PopupBase
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly IExpeditionController _expeditionController;

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
            
            var cellCount = _expeditionController.CellCount;
            currentCount.text = cellCount.ToString();
            nextCount.text = (cellCount + 1).ToString();
            
            titleText.text = _localisationController.GetLanguageValue(LocalisationKeys.UpgradeTitleKey);
        }
        
        private void Start() 
        {
            _buyData = _expeditionController.GetUpgradeData();
            upgrade.SetData(_buyData.Cost, _buyData.Level);
        }

        #endregion
    }
}
