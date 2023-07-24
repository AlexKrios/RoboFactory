using System;
using JetBrains.Annotations;
using RoboFactory.General.Asset;
using RoboFactory.General.Ui;
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
        [Inject] private readonly IUiController _uiController;

        public Button CreateButton(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<Button>(_settings.ButtonPrefab, parent);
        }
        
        public async void CreateMenu()
        {
            var menu = await _addressableService.LoadAssetAsync<GameObject>(_settings.MenuAsset);
            var canvasT = _uiController.GetCanvas(CanvasType.Ui).transform;
            _container.InstantiatePrefabForComponent<ConversionMenuView>(menu, canvasT);
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
