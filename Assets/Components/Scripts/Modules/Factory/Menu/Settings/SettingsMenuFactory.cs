using System;
using Components.Scripts.Modules.General.Asset;
using Components.Scripts.Modules.General.Ui;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Settings
{
    [UsedImplicitly]
    public class SettingsMenuFactory
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly IUiController _uiController;

        #endregion

        public async void CreateMenu()
        {
            var menu = await AssetsManager.LoadAsset<GameObject>(_settings.menuAsset);
            var canvasT = _uiController.GetCanvas(CanvasType.Ui).transform;
            _container.InstantiatePrefabForComponent<SettingsMenuView>(menu, canvasT);
        }

        [Serializable]
        public class Settings
        {
            public AssetReference menuAsset;
        }
    }
}
