using System;
using JetBrains.Annotations;
using Modules.Factory.Menu.Order.Components;
using Modules.General.Ui;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Order
{
    [UsedImplicitly]
    public class OrderMenuFactory
    {
        #region Zenject

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject] private readonly IUiController _uiController;

        #endregion
        
        public Button CreateButton(Transform parent)
        { 
            return _container.InstantiatePrefabForComponent<Button>(_settings.buttonPrefab, parent);
        }
        
        public void CreateMenu()
        { 
            var canvasT = _uiController.GetCanvas(CanvasType.Ui).transform;
            _container.InstantiatePrefabForComponent<OrderMenuView>(_settings.menuPrefab, canvasT);
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
