using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Modules.General.Item.Production.Models.Load;
using Modules.General.Item.Production.Models.Object;
using Modules.General.Item.Products;
using Modules.General.Scriptable;
using Zenject;

namespace Modules.General.Item.Production
{
    [UsedImplicitly]
    public class ProductionController : IProductionController
    {
        #region Zenject

        [Inject] private readonly ControllersResolver _controllersResolver;
        [Inject] private readonly IProductsController _productsController;
        [Inject] private readonly Settings _settings;

        #endregion

        #region Variables

        public Action OnProductionComplete { get; set; }
        
        private readonly List<ProductionObject> _productionData;
        public int Level { get; set; }
        public int CellCount { get; set; }

        #endregion

        public ProductionController()
        {
            _productionData = new List<ProductionObject>();

            Level = 1;
            CellCount = 1;
        }
        
        public void LoadStoreData(ProductionsLoadObject data)
        {
            Level = data.level;
            CellCount = data.count;
            foreach (var prodObj in data.production)
            {
                var production = new ProductionObjectBuilder().Create(prodObj);
                AddProduction(production);
            }
        }

        public bool IsHaveFreeCell()
        {
            return _productionData.Count < CellCount;
        }

        public void AddProduction(ProductionObject prod)
        {
            _productionData.Add(prod);
            RemoveParts(prod);
        }
        public List<ProductionObject> GetAllProduction() => _productionData;
        public ProductionObject GetProduction(Guid id) => _productionData.First(x => x.Id == id);
        public UpgradeDataObject GetUpgradeData()
        {
            return _settings.upgradeData.Data.First(x => x.Count == CellCount);
        }

        public void RemoveProduction(Guid id)
        {
            var expedition = _productionData.First(x => x.Id == id);
            _productionData.Remove(expedition);
            OnProductionComplete?.Invoke();
        }

        public bool IsEnoughParts(ProductionObject productionObj)
        {
            var recipe = _productsController.GetProduct(productionObj.Key).Recipe;
            foreach (var partObj in recipe.Parts)
            {
                var data = partObj.data;
                var store = _controllersResolver.GetStoreByType(data.ItemType);
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
            
            var recipe = _productsController.GetProduct(productionObj.Key).Recipe;
            foreach (var partObj in recipe.Parts)
            {
                var data = partObj.data;
                var store = _controllersResolver.GetStoreByType(data.ItemType);
                store.GetItem(data.Key).RemoveCount(partObj);
            }
        }

        [Serializable]
        public class Settings
        {
            public UpgradeDataScriptable upgradeData;
        }
    }
}
