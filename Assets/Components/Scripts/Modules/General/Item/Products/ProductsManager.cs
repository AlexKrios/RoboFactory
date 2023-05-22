using System;
using System.Collections.Generic;
using System.Linq;
using Components.Scripts.Modules.Factory.Api;
using Components.Scripts.Modules.General.Item.Models;
using Components.Scripts.Modules.General.Item.Models.Types;
using Components.Scripts.Modules.General.Item.Products.Object;
using Components.Scripts.Modules.General.Item.Products.Object.Types;
using Components.Scripts.Modules.General.Item.Products.Scriptable;
using Components.Scripts.Modules.General.Item.Products.Types;
using Components.Scripts.Modules.General.Level;
using Components.Scripts.Modules.General.Scriptable;
using Components.Scripts.Modules.General.Unit.Type;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.General.Item.Products
{
    [UsedImplicitly]
    public class ProductsManager : IItemManager
    {
        [Inject] private readonly ApiManager _apiManager;
        [Inject] private readonly LevelManager _levelManager;
        
        public ItemType ItemType { get; }
        private readonly Dictionary<string, ProductObject> _productsDictionary;

        public ProductsManager(Settings settings)
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
        
        public void LoadData(Dictionary<string, ProductDto> products)
        {
            if (products == null)
                return;
            
            foreach (var productData in products)
            {
                var product = _productsDictionary[productData.Key];
                product.Count = productData.Value.count;
                product.Experience = productData.Value.experience;
            }
        }

        public ItemBase GetItem(string key) => _productsDictionary[key];
        public ProductObject GetProduct(string key) => _productsDictionary[key];
        public List<ProductObject> GetAllProducts() => _productsDictionary.Values.ToList();
        public Dictionary<string, ProductDto> GetAllProductsDto()
        {
            var rawDto = new Dictionary<string, ProductDto>();
            _productsDictionary.ToList().ForEach(x => rawDto.Add(x.Key, x.Value.ToDto()));
            return rawDto;
        } 

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

        public async void CreateProduct(string key)
        {
            var product = _productsDictionary[key];
            _levelManager.SetExperience(product.Recipe.Experience);
            product.IncrementCount();
            product.IncrementExperience();
            await _apiManager.SetUserProductSingle(key, product.ToDto());
        }

        public async UniTask AddItem(string key, int count = 1)
        {
            var product = _productsDictionary[key];
            product.IncrementCount(count);
            await _apiManager.SetUserProductSingle(key, product.ToDto());
        }

        public async UniTask RemoveItem(string key, int count = 1)
        {
            var product = _productsDictionary[key];
            product.DecrementCount(count);
            await _apiManager.SetUserProductSingle(key, product.ToDto());
        }

        [Serializable]
        public class Settings
        {
            public List<ProductScriptable> items;
            public LevelCapsScriptable levels;
        }
    }
}
