using Modules.General.Localisation;
using Modules.General.Localisation.Models;
using TMPro;
using UnityEngine;
using Zenject;

namespace Modules.General.Ui.Popup.Common
{
    [AddComponentMenu("Scripts/General/Popup/Small Notification View")]
    public class SmallNotificationView : PopupBase
    {
        [Inject] private readonly ILocalisationController _localisationController;

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
