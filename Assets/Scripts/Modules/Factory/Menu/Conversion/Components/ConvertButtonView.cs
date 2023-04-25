using System;
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
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly IRawController _rawController;
        [Inject] private readonly IConvertRawController _convertRawController;
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
            var isEnoughRaw = _convertRawController.IsEnoughRaw(_menu.ActiveRaw.Key, _menu.ActiveStar);
            SetInteractable(isEnoughRaw);
        }
        
        protected override void Click()
        {
            base.Click();
            
            if (_rawController.CheckIfRawStoreFull(_menu.ActiveRaw.Key, _menu.ActiveStar))
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
