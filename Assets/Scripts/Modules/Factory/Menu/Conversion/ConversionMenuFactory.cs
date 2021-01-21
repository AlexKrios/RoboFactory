using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Conversion
{
    public class ConversionMenuFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject(Id = "MenuCanvas")] private readonly RectTransform _menuCanvas;

        public Button CreateButton(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<Button>(_settings.buttonPrefab, parent);
        }
        
        public void CreateMenu()
        {
            _container.InstantiatePrefabForComponent<ConversionMenuView>(_settings.menuPrefab, _menuCanvas);
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
