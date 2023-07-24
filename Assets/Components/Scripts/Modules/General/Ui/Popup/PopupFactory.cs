using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Ui.Popup
{
    [UsedImplicitly]
    public class PopupFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly IUiController _uiController;

        public void CreateSmallNotification(UiType type, Transform parent)
        {
            var popupExists = _uiController.FindUi<SmallNotificationView>();
            if (popupExists != null)
                return;

            var popupData = _settings.Popups.First(x => x.Type == type);
            _container.InstantiatePrefabForComponent<SmallNotificationView>(popupData.Prefab, parent);
            _uiController.AddUi(this);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private List<PopupTypeObject> _popups;
            
            public List<PopupTypeObject> Popups => _popups;
        }
    }
    
    [Serializable]
    public class PopupTypeObject
    {
        public UiType Type;
        public GameObject Prefab;
    }
}
