using System;
using Modules.General.Ui;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Settings
{
    public class SettingsMenuFactory
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject(Id = "UiCamera")] private readonly Transform _uiCamera;
        [Inject(Id = "MenuCamera")] private readonly Transform _menuCamera;
        [Inject(Id = "UiCanvas")] private readonly RectTransform _uiCanvas;
        [Inject(Id = "MenuCanvas")] private readonly RectTransform _menuCanvas;

        #endregion

        public void CreateMenu()
        {
            _uiCamera.gameObject.SetActive(false);
            _menuCamera.gameObject.SetActive(true);
            
            _uiCanvas.gameObject.SetActive(false);
            _menuCanvas.gameObject.SetActive(true);
            
            var menu = _container.InstantiatePrefabForComponent<SettingsMenuView>(_settings.menuPrefab, _menuCanvas);
            menu.name = _settings.menuName;
        }

        [Serializable]
        public class Settings
        {
            public UiType menuType;
            public string menuName;
            public GameObject menuPrefab;
        }
    }
}
