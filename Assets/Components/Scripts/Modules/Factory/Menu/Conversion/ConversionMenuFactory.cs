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
        [Inject] private readonly AssetsManager _assetsManager;
        [Inject] private readonly IUiController _uiController;

        public Button CreateButton(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<Button>(_settings.buttonPrefab, parent);
        }
        
        public async void CreateMenu()
        {
            var menu = await _assetsManager.LoadAssetAsync<GameObject>(_settings.menuAsset);
            var canvasT = _uiController.GetCanvas(CanvasType.Ui).transform;
            _container.InstantiatePrefabForComponent<ConversionMenuView>(menu, canvasT);
        }

        [Serializable]
        public class Settings
        {
            public AssetReference menuAsset;
            
            [Space]
            public GameObject buttonPrefab;
        }
    }
}
