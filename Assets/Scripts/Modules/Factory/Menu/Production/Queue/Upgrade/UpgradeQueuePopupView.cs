using Modules.General.Item.Production;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Scriptable;
using Modules.General.Ui;
using Modules.General.Ui.Common.Menu;
using TMPro;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Production.Queue.Upgrade
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Queue/Upgrade Queue Popup View")]
    public class UpgradeQueuePopupView : PopupBase
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly IProductionController _productionController;

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
            
            _uiController.AddUi(this);
            
            upgrade.OnUpgradeClick += Close;
            
            var cellCount = _productionController.CellCount;
            currentCount.text = cellCount.ToString();
            nextCount.text = (cellCount + 1).ToString();
            
            titleText.text = _localisationController.GetLanguageValue(LocalisationKeys.UpgradeTitleKey);
        }
        
        private void Start() 
        {
            _buyData = _productionController.GetUpgradeQueueData();
            upgrade.SetData(_buyData.Cost, _buyData.Level);
        }

        #endregion
    }
}
