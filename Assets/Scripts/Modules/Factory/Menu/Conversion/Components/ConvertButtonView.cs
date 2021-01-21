using System;
using Modules.Factory.Menu.Conversion.Tabs;
using Modules.General.Item.Raw;
using Modules.General.Item.Raw.Convert;
using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using Modules.General.Ui;
using Modules.General.Ui.Common;
using Modules.General.Ui.Popup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Conversion.Components
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Convert Button View")]
    public class ConvertButtonView : ButtonBase
    {
        #region Zenject

        [Inject] private readonly ILocalisationController _localisationController;
        [Inject] private readonly IConvertRawController _convertRawController;
        [Inject] private readonly ConversionMenuManager _conversionMenuManager;
        [Inject] private readonly IRawController _rawController;
        [Inject] private readonly PopupFactory _popupFactory;
        [Inject(Id = "PopupCanvas")] private readonly RectTransform _popupCanvas;
        
        #endregion

        #region Variables

        public Action OnClickEvent { get; set; }

        private TabCellView ActiveTab => _conversionMenuManager.Tabs.ActiveTab;
        private int ActiveStar => _conversionMenuManager.Star.ActiveStar;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            _conversionMenuManager.Convert = this;

            SetButtonText(_localisationController.GetLanguageValue(LocalisationKeys.ConversionMenuButtonTitleKey));
        }

        private void OnDestroy()
        {
            OnClickEvent = null;
        }

        #endregion

        public override void SetState()
        {
            var isEnoughRaw = _convertRawController.IsEnoughRaw(ActiveTab.RawData.Key, ActiveStar);
            SetInteractable(isEnoughRaw);
        }
        
        protected override void Click()
        {
            base.Click();
            
            if (_rawController.CheckIfRawStoreFull(ActiveTab.RawData.Key, ActiveStar))
            {
                _popupFactory.CreateSmallNotification(UiType.StorageFull, _popupCanvas);
                return;
            }

            _convertRawController.RemoveParts();

            OnClickEvent?.Invoke();
        }
    }
}
