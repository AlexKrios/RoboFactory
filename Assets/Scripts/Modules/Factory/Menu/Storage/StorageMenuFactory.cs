using System;
using Modules.Factory.Menu.Storage.Items;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Storage
{
    public class StorageMenuFactory
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject(Id = "MenuCanvas")] private readonly RectTransform _menuCanvas;
        
        #endregion                

        public Button CreateButton(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<Button>(_settings.buttonPrefab, parent);
        }
        
        public void CreateMenu()
        {
            _container.InstantiatePrefabForComponent<StorageMenuView>(_settings.menuPrefab, _menuCanvas);
        }
        
        public ItemCellView CreateItem(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<ItemCellView>(_settings.itemPrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            public GameObject menuPrefab;
            
            [Space]
            public GameObject buttonPrefab;
            
            [Space]
            public GameObject itemPrefab;
        }
    }
}
