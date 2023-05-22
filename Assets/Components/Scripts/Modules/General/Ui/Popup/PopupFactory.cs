using System;
using System.Collections.Generic;
using System.Linq;
using Components.Scripts.Modules.General.Ui.Popup.Common;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.General.Ui.Popup
{
    [UsedImplicitly]
    public class PopupFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly IUiController _uiController;

        public void CreateSmallNotification(UiType type, Transform parent)
        {
            var popupData = _settings.popups.First(x => x.Key == type);
            var popupExists = _uiController.FindUi<SmallNotificationView>();
            if (popupExists != null)
                return;

            var popup = _container.InstantiatePrefabForComponent<SmallNotificationView>(popupData.Value, parent);
            popup.name = popupData.Key.ToString();

            _uiController.AddUi(this);
        }

        [Serializable]
        public class Settings
        {
            public List<KeyValuePair<UiType, GameObject>> popups;
        }
    }
}
