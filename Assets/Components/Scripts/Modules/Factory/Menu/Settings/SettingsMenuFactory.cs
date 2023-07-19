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
        [Inject] private readonly AddressableService addressableService;
        [Inject] private readonly IUiController _uiController;

        #endregion

        public async void CreateMenu()
        {
            var parent = _uiController.GetCanvas(CanvasType.Ui);
            var menuOriginal = await addressableService.InstantiateAssetAsync(_settings.MenuAsset, parent.transform);
            var menu = _container.InjectGameObjectForComponent<SettingsMenuView>(menuOriginal);
            menu.Initialize();
            
            addressableService.ReleaseAsset(_settings.MenuAsset);
        }

        [Serializable]
        public class Settings
        {
            public AssetReference _menuAsset;
            
            public AssetReference MenuAsset => _menuAsset;
        }
    }
}
