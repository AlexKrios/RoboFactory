using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Api;
using RoboFactory.General.Level;
using RoboFactory.General.Profile;
using RoboFactory.General.Scriptable;
using RoboFactory.General.Services;
using RoboFactory.General.Unit;
using UnityEngine;
using Zenject;

namespace RoboFactory.General.Item.Products
{
    [UsedImplicitly]
    public class ProductsService : Service, IItemManager
    {
        public ItemType ItemType { get; }
        
        [Inject] private readonly Settings _settings;
        [Inject] private readonly CommonProfile _commonProfile;
        [Inject] private readonly ApiService _apiService;
        [Inject] private readonly ExperienceService _experienceService;
        
        public override ServiceTypeEnum ServiceType => ServiceTypeEnum.NeedAuth;

        private readonly Dictionary<string, ProductObject> _productsDictionary = new();

        public ProductsService(Settings settings)
        {
            ItemType = ItemType.Product;
        }
        
        protected override UniTask InitializeAsync()
        {
            if (_productsDictionary.Count != 0)
                _productsDictionary.Clear();
            
            foreach (var product in _settings.Items)
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
                    item.Caps = _settings.Levels.Caps;
                    _productsDictionary.Add(item.Key, item);
                }
            }
            
            var storeData = _commonProfile.UserProfile.StoresSection;
            if (storeData?.Products == null) return UniTask.CompletedTask;

            var productsData = storeData.Products;
            foreach (var productData in productsData)
            {
                var product = _productsDictionary[productData.Key];
                product.Count = productData.Value.Count;
                product.Experience = productData.Value.Experience;
            }
            
            return UniTask.CompletedTask;
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
            _experienceService.SetExperience(product.Recipe.Experience);
            product.IncrementCount();
            product.IncrementExperience();
            await _apiService.SetUserProductSingle(key, product.ToDto());
        }

        public async UniTask AddItem(string key, int count = 1)
        {
            var product = _productsDictionary[key];
            product.IncrementCount(count);
            await _apiService.SetUserProductSingle(key, product.ToDto());
        }

        public async UniTask RemoveItem(string key, int count = 1)
        {
            var product = _productsDictionary[key];
            product.DecrementCount(count);
            await _apiService.SetUserProductSingle(key, product.ToDto());
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private List<ProductScriptable> _items;
            [SerializeField] private LevelCapsScriptable _levels;
            
            public List<ProductScriptable> Items => _items;
            public LevelCapsScriptable Levels => _levels;
        }
    }
}
