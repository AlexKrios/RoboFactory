using System;
using Components.Scripts.Modules.General.Item.Raw;
using Components.Scripts.Modules.General.Item.Raw.Convert;
using Components.Scripts.Modules.General.Localisation;
using Components.Scripts.Modules.General.Ui;
using Components.Scripts.Modules.General.Ui.Common;
using Components.Scripts.Modules.General.Ui.Popup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Conversion.Components
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("Scripts/Factory/Menu/Conversion/Convert Button View")]
    public class ConvertButtonView : ButtonBase
    {
        #region Zenject

        [Inject] private readonly LocalisationManager _localisationController;
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

            SetButtonText(_localisationController.GetLanguageValue(LocalisationKeys.ConversionMenuButtonTitleKey));
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
