using System;
using JetBrains.Annotations;
using Modules.General.Ui;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Settings
{
    [UsedImplicitly]
    public class SettingsMenuFactory
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly IUiController _uiController;

        #endregion

        public void CreateMenu()
        {
            var canvasT = _uiController.GetCanvas(CanvasType.Ui).transform;
            var menu = _container.InstantiatePrefabForComponent<SettingsMenuView>(_settings.menuPrefab, canvasT);
            menu.name = _settings.menuName;
        }

        [Serializable]
        public class Settings
        {
            public string menuName;
            public GameObject menuPrefab;
        }
    }
}
