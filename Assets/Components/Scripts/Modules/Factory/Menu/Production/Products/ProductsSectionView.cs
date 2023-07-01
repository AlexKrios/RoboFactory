using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Products Section View")]
    public class ProductsSectionView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ProductsManager _productsManager;
        [Inject] private readonly ProductionMenuFactory _productionMenuFactory;
        
        #endregion
        
        #region Components

        [SerializeField] private List<ProductCellView> products;

        #endregion

        #region Variables

        public Action OnProductClickEvent { get; set; }

        private ProductionMenuView _menu;
        private ProductCellView _activeProduct;
        private ProductCellView ActiveProduct
        {
            get => _activeProduct;
            set 
            {
                if (_activeProduct != null)
                    _activeProduct.SetInactive();

                _activeProduct = value;
                _menu.ActiveProduct = value.Data;
                _activeProduct.SetActive();
            }
        }
        
        #endregion

        public void Initialize()
        {
            _menu = _uiController.FindUi<ProductionMenuView>();

            CreateProductCells();
        }

        public void CreateProductCells()
        {
            if (products.Count != 0)
                RemoveProductCells();
            
            var productsWithComponents = _productsManager.GetAllProducts()
                .Where(x =>
                    x.ProductGroup == _menu.ActiveProductGroup &&
                    x.UnitType == _menu.ActiveUnitType &&
                    x.ProductType == _menu.ActiveProductType
                ).OrderBy(x => x.Index).ToList();

            foreach (var productData in productsWithComponents)
            {
                var product = _productionMenuFactory.CreateProduct(transform);
                product.SetProductData(productData);
                product.OnClickEvent += OnTabClick;
                products.Add(product);
            }
            
            ActiveProduct = products.First();
        }

        private void RemoveProductCells()
        {
            products.ForEach(x => Destroy(x.gameObject));
            products.Clear();
        }
        
        private void OnTabClick(ProductCellView tab, int productType)
        {
            if (ActiveProduct == tab)
                return;

            _menu.ActiveProductType = productType;
            _menu.ActiveProduct = tab.Data;

            ActiveProduct = tab;

            OnProductClickEvent?.Invoke();
        }
    }
}