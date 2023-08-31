using System;
using JetBrains.Annotations;
using RoboFactory.General.Asset;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace RoboFactory.Factory.Menu.Settings
{
    [UsedImplicitly]
    public class SettingsMenuFactory
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly AddressableService _addressableService;
        [Inject(Id = Constants.PopupsParentKey)] private readonly Transform _popupsParent;

        public async void CreateMenu()
        {
            var menuOriginal = await _addressableService.InstantiateAssetAsync(_settings.MenuAsset, _popupsParent);
            var menu = _container.InjectGameObjectForComponent<SettingsMenuView>(menuOriginal);
            menu.Initialize();
            
            _addressableService.ReleaseAsset(_settings.MenuAsset);
        }

        [Serializable]
        public class Settings
        {
            public AssetReference _menuAsset;
            
            public AssetReference MenuAsset => _menuAsset;
        }
    }
}
