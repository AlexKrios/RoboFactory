using System;
using JetBrains.Annotations;
using RoboFactory.General.Asset;
using RoboFactory.General.Ui;
using UnityEngine.AddressableAssets;
using Zenject;

namespace RoboFactory.Factory.Menu.Settings
{
    [UsedImplicitly]
    public class SettingsMenuFactory
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly AssetsManager _assetsManager;
        [Inject] private readonly IUiController _uiController;

        #endregion

        public async void CreateMenu()
        {
            var canvasT = _uiController.GetCanvas(CanvasType.Ui).transform;
            var menuOriginal = await _assetsManager.InstantiateAssetAsync(_settings.menuAsset, canvasT);
            var menu = _container.InjectGameObjectForComponent<SettingsMenuView>(menuOriginal);
            menu.Initialize();
            
            _assetsManager.ReleaseAsset(_settings.menuAsset);
        }

        [Serializable]
        public class Settings
        {
            public AssetReference menuAsset;
        }
    }
}
