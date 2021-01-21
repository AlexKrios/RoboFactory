using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using TMPro;
using UnityEngine;
using Zenject;

namespace Modules.General.Ui.Popup.Views.Common
{
    [AddComponentMenu("Scripts/General/Popup/Small Notification View")]
    public class SmallNotificationView : PopupBase
    {
        [Inject] private readonly ILocalisationController _localisationController;

        [SerializeField] private TextMeshProUGUI messageText;

        protected override void Awake()
        {
            base.Awake();
            
            var popupKey = LocalisationKeys.UiKeys[type];
            messageText.text = _localisationController.GetLanguageValue(popupKey);
            
            PlayFadeInOut(3f);
        }
    }
}
