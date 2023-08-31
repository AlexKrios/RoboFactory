using RoboFactory.General.Localization;
using TMPro;
using UnityEngine;
using Zenject;

namespace RoboFactory.General.Ui.Popup
{
    public class SmallNotificationView : PopupBase
    {
        [Inject] private readonly LocalizationService localizationController;

        [SerializeField] private TMP_Text _messageText;

        protected override void Awake()
        {
            base.Awake();
            
            var popupKey = LocalizationKeys.UiKeys[_type];
            _messageText.text = localizationController.GetLanguageValue(popupKey);
            
            Close(3f);
        }
    }
}
