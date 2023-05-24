using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Api;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Scriptable;
using Zenject;

namespace RoboFactory.General.Item.Production
{
    [UsedImplicitly]
    public class ProductionManager
    {
        #region Zenject

        [Inject] private readonly ApiManager _apiManager;
        [Inject] private readonly ManagersResolver managersResolver;
        [Inject] private readonly ProductsManager _productsManager;
        [Inject] private readonly Settings _settings;

        #endregion

        #region Variables

        public Action OnProductionComplete { get; set; }
        
        private readonly Dictionary<Guid, ProductionObject> _productionData;
        public int Level { get; private set; }
        public int CellCount { get; private set; }

        #endregion

        public ProductionManager()
        {
            _productionData = new Dictionary<Guid, ProductionObject>();

            Level = 1;
            CellCount = 1;
        }
        
        public void LoadData(ProductionSectionDto data)
        {
            Level = data.level;
            CellCount = data.count;
            
            if (data.Production == null)
                return;
            
            foreach (var dto in data.Production)
            {
                var production = new ProductionObject().SetLoadData(dto.Value);
                _productionData.Add(production.Id, production);
            }
        }
        
        public async UniTask AddLevel()
        {
            Level++;
            await _apiManager.SetUserProductionLevel(Level);
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
            await _apiManager.AddUserProduction(data.Id, data.ToDto());
            
            RemoveParts(data);
        }
        
        public List<ProductionObject> GetAllProduction() => _productionData.Values.ToList();
        public ProductionObject GetProduction(Guid id) => 
            _productionData.First(x => x.Key == id).Value;
        public async void RemoveProduction(Guid id)
        {
            _productionData.Remove(id);
            await _apiManager.RemoveUserProduction(id);
            
            OnProductionComplete?.Invoke();
        }

        public UpgradeDataObject GetUpgradeQueueData()
        {
            return _settings.upgradeQueueData.Data.First(x => x.Count == CellCount);
        }

        public async UniTask IncreaseQueueCount()
        {
            CellCount++;
            await _apiManager.SetUserProductionQueueCount(CellCount);
        }
        
        public UpgradeDataObject GetUpgradeQualityData()
        {
            return _settings.upgradeQualityData.Data.First(x => x.Count == Level);
        }

        public bool IsEnoughParts(ProductionObject productionObj)
        {
            var recipe = _productsManager.GetProduct(productionObj.Key).Recipe;
            foreach (var partObj in recipe.Parts)
            {
                var data = partObj.data;
                var store = managersResolver.GetManagerByType(data.ItemType);
                var isEnough = store.GetItem(data.Key).IsEnoughCount(partObj);

                if (!isEnough)
                    return false;
            }

            return true;
        }

        public void RemoveParts(ProductionObject productionObj)
        {
            if (productionObj.IsLoad)
                return;
            
            var recipe = _productsManager.GetProduct(productionObj.Key).Recipe;
            foreach (var partObj in recipe.Parts)
            {
                var data = partObj.data;
                var store = managersResolver.GetManagerByType(data.ItemType);
                store.RemoveItem(partObj.data.Key, partObj.count);
            }
        }

        [Serializable]
        public class Settings
        {
            public UpgradeDataScriptable upgradeQueueData;
            public UpgradeDataScriptable upgradeQualityData;
        }
    }
}
