using RoboFactory.General.Localisation;
using TMPro;
using UnityEngine;
using Zenject;

namespace RoboFactory.General.Ui.Popup
{
    [AddComponentMenu("Scripts/General/Popup/Small Notification View")]
    public class SmallNotificationView : PopupBase
    {
        [Inject] private readonly LocalisationManager _localisationController;

        [SerializeField] private TMP_Text messageText;

        protected override void Awake()
        {
            base.Awake();
            
            var popupKey = LocalisationKeys.UiKeys[type];
            messageText.text = _localisationController.GetLanguageValue(popupKey);
            
            Close(3f);
        }
    }
}
