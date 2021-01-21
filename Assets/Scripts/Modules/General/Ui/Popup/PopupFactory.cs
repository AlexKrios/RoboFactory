using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Modules.General.Ui.Popup.Views;
using Modules.General.Ui.Popup.Views.Common;
using UnityEngine;
using Zenject;

namespace Modules.General.Ui.Popup
{
    [UsedImplicitly]
    public class PopupFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly IUiController _uiController;

        public void Create(UiType type, Transform parent)
        {
            var popupData = _settings.popups.First(x => x.type == type);
            var popupExists = _uiController.FindUi(popupData.type);
            if (popupExists != null)
                return;

            var popup = _container
                .InstantiatePrefabForComponent<PopupBase>(popupData.prefab, parent);
            
            popup.name = popupData.type.ToString();

            _uiController.AddUi(popupData.type, popup.gameObject);
        }
        
        public void CreateSmallNotification(UiType type, Transform parent)
        {
            Debug.LogWarning(_settings);
            var popupData = _settings.popups.First(x => x.type == type);
            var popupExists = _uiController.FindUi(popupData.type);
            if (popupExists != null)
                return;

            var popup = _container
                .InstantiatePrefabForComponent<SmallNotificationView>(popupData.prefab, parent);
            
            popup.name = popupData.type.ToString();

            _uiController.AddUi(popupData.type, popup.gameObject);
        }

        [Serializable]
        public class Settings
        {
            public List<PopupFactoryObject> popups;
        }
    }

    [Serializable]
    public class PopupFactoryObject
    {
        public UiType type;
        public GameObject prefab;
    }
}
