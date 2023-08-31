using System;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Item.Raw.Convert;
using RoboFactory.General.Localization;
using RoboFactory.General.Ui;
using RoboFactory.General.Ui.Common;
using RoboFactory.General.Ui.Popup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Conversion
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class ConvertButtonView : ButtonBase
    {
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly RawService _rawService;
        [Inject] private readonly ConvertRawService _convertRawService;
        [Inject] private readonly PopupFactory _popupFactory;
        [Inject(Id = Constants.ScreensParentKey)] private readonly Transform _screensParent;

        public Action OnClickEvent { get; set; }
        
        private ConversionMenuView _menu;

        protected override void Awake()
        {
            base.Awake();

            _menu = _uiController.FindUi<ConversionMenuView>();

            SetButtonText(_localizationService.GetLanguageValue(LocalizationKeys.ConversionMenuButtonTitleKey));
        }

        private void OnDestroy()
        {
            OnClickEvent = null;
        }

        public override void SetState()
        {
            var isEnoughRaw = _convertRawService.IsEnoughRaw(_menu.ActiveRaw.Key);
            SetInteractable(isEnoughRaw);
        }
        
        protected override void Click()
        {
            base.Click();
            
            if (_rawService.CheckIfRawStoreFull(_menu.ActiveRaw.Key))
            {
                _popupFactory.CreateSmallNotification(UiType.StorageFull, _screensParent);
                return;
            }

            _convertRawService.RemoveParts();

            OnClickEvent?.Invoke();
        }
    }
}
