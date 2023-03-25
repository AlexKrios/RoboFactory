using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Location;
using Modules.General.Scriptable;
using Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Expedition.Queue.Upgrade
{
    [AddComponentMenu("Scripts/Factory/Menu/Expedition/Queue/Upgrade Queue Popup View")]
    public class UpgradeQueuePopupView : MenuBase
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IExpeditionController _expeditionController;

        #endregion
        
        #region Components
        
        [Space]
        [SerializeField] private TextMeshProUGUI titleText;

        [Space]
        [SerializeField] private TextMeshProUGUI currentCount;
        [SerializeField] private TextMeshProUGUI nextCount;
        
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
