using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Item.Products;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Production.Products
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Products Section View")]
    public class ProductsSectionView : MonoBehaviour
    {
        #region Zenject
        
        [Inject] private readonly IProductsController _productsController;
        [Inject] private readonly ProductionMenuManager _productionMenuManager;
        [Inject] private readonly ProductionMenuFactory _productionMenuFactory;
        
        #endregion
        
        #region Components

        [SerializeField] private List<ProductCellView> products;

        #endregion

        #region Variables

        public Action OnProductClickEvent { get; set; }

        private ProductCellView _activeProduct;
        public ProductCellView ActiveProduct
        {
            get => _activeProduct;
            private set 
            {
                if (_activeProduct != null)
                    _activeProduct.SetInactive();

                _activeProduct = value;
                _activeProduct.SetActive();
            }
        }
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            _productionMenuManager.Products = this;

            CreateProductCells();
        }

        #endregion

        public void CreateProductCells()
        {
            if (products.Count != 0)
                RemoveProductCells();
            
            var productsWithComponents = _productsController.GetAllProducts()
                .Where(x =>
                    x.ProductGroup == _productionMenuManager.ActiveProductGroup &&
                    x.UnitType == _productionMenuManager.ActiveUnitType &&
                    x.ProductType == _productionMenuManager.ActiveProductType
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

            _productionMenuManager.ActiveProductType = productType;

            ActiveProduct = tab;

            OnProductClickEvent?.Invoke();
        }
    }
}