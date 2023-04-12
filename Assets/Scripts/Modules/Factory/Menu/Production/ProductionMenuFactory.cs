using System;
using JetBrains.Annotations;
using Modules.Factory.Menu.Production.Products;
using Modules.Factory.Menu.Production.Queue.Upgrade;
using Modules.Factory.Menu.Production.Upgrade;
using Modules.General.Ui;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Factory.Menu.Production
{
    [UsedImplicitly]
    public class ProductionMenuFactory
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
            _container.InstantiatePrefabForComponent<ProductionMenuView>(_settings.menuPrefab, canvasT);
        }
        
        public ProductCellView CreateProduct(Transform parent)
        {
            return _container.InstantiatePrefabForComponent<ProductCellView>(_settings.productPrefab, parent);
        }
        
        public void CreateUpgradePopup(Transform parent)
        {
            _container.InstantiatePrefabForComponent<UpgradePopupView>(_settings.upgradePrefab, parent);
        }
        
        public void CreateUpgradeQueuePopup(Transform parent)
        {
            _container.InstantiatePrefabForComponent<UpgradeQueuePopupView>(_settings.queuePrefab, parent);
        }

        [Serializable]
        public class Settings
        {
            public GameObject menuPrefab;
            
            [Space]
            public GameObject buttonPrefab;
            
            [Space]
            public GameObject productPrefab;
            
            [Space]
            public GameObject upgradePrefab;
            
            [Space]
            public GameObject queuePrefab;
        }
    }
}
