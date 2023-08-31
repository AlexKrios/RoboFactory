using System;
using JetBrains.Annotations;
using RoboFactory.General.Asset;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Conversion
{
    [UsedImplicitly]
    public class ConversionMenuFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly AddressableService _addressableService;
        [Inject(Id = Constants.ScreensParentKey)] private readonly Transform _screensParent;

        public Button CreateButton(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<Button>(_settings.ButtonPrefab, parent);
        }
        
        public async void CreateMenu()
        {
            var menu = await _addressableService.LoadAssetAsync<GameObject>(_settings.MenuAsset);
            _container.InstantiatePrefabForComponent<ConversionMenuView>(menu, _screensParent);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private GameObject _buttonPrefab;

            [Space]
            [SerializeField] private AssetReference _menuAsset;
            
            public AssetReference MenuAsset => _menuAsset;
            public GameObject ButtonPrefab => _buttonPrefab;
        }
    }
}
