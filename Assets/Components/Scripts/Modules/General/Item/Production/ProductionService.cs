using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Api;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Profile;
using RoboFactory.General.Scriptable;
using RoboFactory.General.Services;
using Zenject;

namespace RoboFactory.General.Item.Production
{
    [UsedImplicitly]
    public class ProductionService : Service
    {
        [Inject] private readonly Settings _settings;
        [Inject] private readonly CommonProfile _commonProfile;
        [Inject] private readonly ApiService _apiService;
        [Inject] private readonly ManagersResolver _managersResolver;
        [Inject] private readonly ProductsService productsService;
        
        public override ServiceTypeEnum ServiceType => ServiceTypeEnum.NeedAuth;

        public Action OnProductionComplete { get; set; }
        
        private readonly Dictionary<Guid, ProductionObject> _productionData = new();
        public int Level { get; private set; }
        public int CellCount { get; private set; }

        public ProductionService()
        {
            Level = 1;
            CellCount = 1;
        }
        
        protected override UniTask InitializeAsync()
        {
            var productionData = _commonProfile.UserProfile.ProductionsSection;
            
            Level = productionData.Level;
            CellCount = productionData.Count;
            
            if (productionData.Production == null) return UniTask.CompletedTask;
            
            foreach (var dto in productionData.Production)
            {
                var production = new ProductionObject().SetLoadData(dto.Value);
                _productionData.Add(production.Id, production);
            }
            
            return UniTask.CompletedTask;
        }
        
        public async UniTask AddLevel()
        {
            Level++;
            await _apiService.SetUserProductionLevel(Level);
        }

        public bool IsHaveFreeCell()
        {
            return _productionData.Count < CellCount;
        }
        
        public Dictionary<string, ProductionDto> GetAllProductionDto()
        {
            var rawDto = new Dictionary<string, ProductionDto>();
            _productionData.ToList()
                .ForEach(x => rawDto.Add(x.Key.ToString(), x.Value.ToDto()));
            
            return rawDto;
        } 

        public async UniTask AddProduction(ProductionObject data)
        {
            _productionData.Add(data.Id, data);
            await _apiService.AddUserProduction(data.Id, data.ToDto());
            
            RemoveParts(data);
        }
        
        public List<ProductionObject> GetAllProduction() => _productionData.Values.ToList();
        public ProductionObject GetProduction(Guid id) => 
            _productionData.First(x => x.Key == id).Value;
        public async void RemoveProduction(Guid id)
        {
            _productionData.Remove(id);
            await _apiService.RemoveUserProduction(id);
            
            OnProductionComplete?.Invoke();
        }

        public UpgradeDataObject GetUpgradeQueueData()
        {
            return _settings._upgradeQueueData.Data.First(x => x.Count == CellCount);
        }

        public async UniTask IncreaseQueueCount()
        {
            CellCount++;
            await _apiService.SetUserProductionQueueCount(CellCount);
        }
        
        public UpgradeDataObject GetUpgradeQualityData()
        {
            return _settings._upgradeQualityData.Data.First(x => x.Count == Level);
        }

        public bool IsEnoughParts(ProductionObject productionObj)
        {
            var recipe = productsService.GetProduct(productionObj.Key).Recipe;
            foreach (var partObj in recipe.Parts)
            {
                var data = partObj.Data;
                var store = _managersResolver.GetManagerByType(data.ItemType);
                var isEnough = store.GetItem(data.Key).IsEnoughCount(partObj);

                if (!isEnough)
                    return false;
            }

            return true;
        }

        private void RemoveParts(ProductionObject productionObj)
        {
            if (productionObj.IsLoad)
                return;
            
            var recipe = productsService.GetProduct(productionObj.Key).Recipe;
            foreach (var partObj in recipe.Parts)
            {
                var data = partObj.Data;
                var store = _managersResolver.GetManagerByType(data.ItemType);
                store.RemoveItem(partObj.Data.Key, partObj.Count);
            }
        }

        [Serializable]
        public class Settings
        {
            public UpgradeDataScriptable _upgradeQueueData;
            public UpgradeDataScriptable _upgradeQualityData;
        }
    }
}
