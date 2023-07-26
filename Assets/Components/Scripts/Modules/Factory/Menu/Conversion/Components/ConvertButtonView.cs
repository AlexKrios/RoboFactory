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
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Convert Button View")]
    public class ConvertButtonView : ButtonBase
    {
        #region Zenject

        [Inject] private readonly LocalizationService localizationController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly RawService _rawService;
        [Inject] private readonly ConvertRawService convertRawService;
        [Inject] private readonly PopupFactory _popupFactory;

        #endregion

        #region Variables

        public Action OnClickEvent { get; set; }
        
        private ConversionMenuView _menu;
        
        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            _menu = _uiController.FindUi<ConversionMenuView>();

            SetButtonText(localizationController.GetLanguageValue(LocalizationKeys.ConversionMenuButtonTitleKey));
        }

        private void OnDestroy()
        {
            OnClickEvent = null;
        }

        #endregion

        public override void SetState()
        {
            var isEnoughRaw = convertRawService.IsEnoughRaw(_menu.ActiveRaw.Key);
            SetInteractable(isEnoughRaw);
        }
        
        protected override void Click()
        {
            base.Click();
            
            if (_rawService.CheckIfRawStoreFull(_menu.ActiveRaw.Key))
            {
                var canvasT = _uiController.GetCanvas(CanvasType.HUD).transform;
                _popupFactory.CreateSmallNotification(UiType.StorageFull, canvasT);
                return;
            }

            convertRawService.RemoveParts();

            OnClickEvent?.Invoke();
        }
    }
}
