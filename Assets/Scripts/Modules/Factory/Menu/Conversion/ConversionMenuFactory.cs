using System;
using JetBrains.Annotations;
using Modules.General.Ui;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Conversion
{
    [UsedImplicitly]
    public class ConversionMenuFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly IUiController _uiController;

        public Button CreateButton(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<Button>(_settings.buttonPrefab, parent);
        }
        
        public void CreateMenu()
        {
            var canvasT = _uiController.GetCanvas(CanvasType.Ui).transform;
            _container.InstantiatePrefabForComponent<ConversionMenuView>(_settings.menuPrefab, canvasT);
        }

        [Serializable]
        public class Settings
        {
            [Space]
            public GameObject menuPrefab;
            
            [Space]
            public GameObject buttonPrefab;
        }
    }
}
