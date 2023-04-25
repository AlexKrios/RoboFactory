using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Modules.General.Item.Models;
using Modules.General.Item.Models.Recipe;
using Modules.General.Item.Models.Types;
using Modules.General.Item.Products.Models;
using Modules.General.Item.Products.Models.Load;
using Modules.General.Item.Products.Models.Object;
using Modules.General.Item.Products.Models.Scriptable;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Scriptable;
using Modules.General.Unit.Type;
using UnityEngine;

namespace Modules.General.Item.Products
{
    [UsedImplicitly]
    public class ProductsController : IProductsController, IGetItem
    {
        public ItemType ItemType { get; }
        private readonly Dictionary<string, ProductObject> _productsDictionary;

        public ProductsController(Settings settings)
        {
            ItemType = ItemType.Product;
            _productsDictionary = new Dictionary<string, ProductObject>();
            
            foreach (var product in settings.items)
            {
                var item = new ProductFactory().Create(product);
                _productsDictionary.Add(item.Key, item);
            }
            
            var pathList = new ItemPath().PathList;
            foreach (var path in pathList)
            {
                var files = Resources.LoadAll<ProductScriptable>(path)
                    .OrderBy(x => x.Index);
                
                foreach (var file in files)
                {
                    var item = new ProductFactory().Create(file);
                    item.Caps = settings.levels.Caps;
                    _productsDictionary.Add(item.Key, item);
                }
            }
        }
        
        public void LoadProductsData(List<ProductLoadObject> products)
        {
            foreach (var productData in products)
            {
                var product = _productsDictionary[productData.key];
                
                product.Count = productData.count;
                product.Experience = productData.experience;
            }
        }

        public ItemBase GetItem(string key) => _productsDictionary[key];
        public ProductObject GetProduct(string key) => _productsDictionary[key];
        public List<ProductObject> GetAllProducts() => _productsDictionary.Values.ToList();

        public List<ProductObject> GetUnitDefaultProducts(UnitType unit)
        {
            return _productsDictionary.Values
                .Where(x => x.UnitType == unit && x.ProductType == 0)
                .ToList();
        }
        
        public ProductObject GetDefaultProduct(ProductGroup group, UnitType unit)
        {
            return _productsDictionary.Values
                .First(x => x.ProductGroup == group && x.UnitType == unit && x.ProductType == 0);
        }

        public bool IsEnoughProduct(List<PartObject> parts)
        {
            return parts.All(x => x.count <= _productsDictionary[x.data.Key].Count);
        }

        [Serializable]
        public class Settings
        {
            public List<ProductScriptable> items;
            public LevelCapsScriptable levels;
        }
    }
}
