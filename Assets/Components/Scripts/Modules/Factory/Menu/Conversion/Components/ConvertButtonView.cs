using System;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Item.Raw.Convert;
using RoboFactory.General.Localisation;
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
        [Inject] private readonly RawManager _rawManager;
        [Inject] private readonly ConvertRawController _convertRawController;
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
            var isEnoughRaw = _convertRawController.IsEnoughRaw(_menu.ActiveRaw.Key);
            SetInteractable(isEnoughRaw);
        }
        
        protected override void Click()
        {
            base.Click();
            
            if (_rawManager.CheckIfRawStoreFull(_menu.ActiveRaw.Key))
            {
                var canvasT = _uiController.GetCanvas(CanvasType.HUD).transform;
                _popupFactory.CreateSmallNotification(UiType.StorageFull, canvasT);
                return;
            }

            _convertRawController.RemoveParts();

            OnClickEvent?.Invoke();
        }
    }
}
