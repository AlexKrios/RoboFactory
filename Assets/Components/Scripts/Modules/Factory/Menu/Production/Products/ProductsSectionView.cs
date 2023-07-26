using System;
using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Ui;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Production
{
    public class ProductsSectionView : MonoBehaviour
    {
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly ProductsService productsService;
        [Inject] private readonly ProductionMenuFactory _productionMenuFactory;
        
        [SerializeField] private List<ProductCellView> _products;

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

        public void Initialize()
        {
            _menu = _uiController.FindUi<ProductionMenuView>();

            CreateProductCells();
        }

        public void CreateProductCells()
        {
            if (_products.Count != 0)
                RemoveProductCells();
            
            var productsWithComponents = productsService.GetAllProducts()
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
                _products.Add(product);
            }
            
            ActiveProduct = _products.First();
        }

        private void RemoveProductCells()
        {
            _products.ForEach(x => Destroy(x.gameObject));
            _products.Clear();
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