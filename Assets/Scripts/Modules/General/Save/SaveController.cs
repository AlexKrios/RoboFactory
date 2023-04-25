using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Modules.General.Item;
using Modules.General.Item.Production;
using Modules.General.Item.Production.Models.Load;
using Modules.General.Item.Products;
using Modules.General.Item.Products.Models.Load;
using Modules.General.Item.Raw;
using Modules.General.Item.Raw.Models.Load;
using Modules.General.Level;
using Modules.General.Level.Models;
using Modules.General.Location;
using Modules.General.Location.Model;
using Modules.General.Money;
using Modules.General.Money.Models;
using Modules.General.Order;
using Modules.General.Order.Models.Load;
using Modules.General.Save.Models;
using Modules.General.Settings;
using Modules.General.Settings.Models;
using Modules.General.Unit;
using Newtonsoft.Json;
using Zenject;

namespace Modules.General.Save
{
    [UsedImplicitly]
    public class SaveController : ISaveController
    {
        [Inject] private readonly ISettingsController _settingsController;

        [Inject] private readonly IMoneyController _moneyController;
        [Inject] private readonly ILevelController _levelController;
        
        [Inject] private readonly IRawController _rawController;
        [Inject] private readonly IProductsController _productsController;
        [Inject] private readonly IUnitsController _unitsController;
        
        [Inject] private readonly IExpeditionController _expeditionController;
        [Inject] private readonly IProductionController _productionController;
        [Inject] private readonly IOrderController _orderController;

        private readonly SaveObject _saveObject;

        public SaveController()
        {
            _saveObject = new SaveObject();
        }

        public void SaveSettings(bool isInit = false)
        {
            var settings = new SettingsLoadObject
            {
                graphics = _settingsController.Graphics.ToString(), 
                language = _settingsController.Language.ToString(),
                
                musicVolume = _settingsController.MusicVolume,
                audioVolume = _settingsController.AudioVolume
            };

            _saveObject.settingsInfo = settings;
            
            if (!isInit)
                Save();
        }
        
        public void SaveMoney(bool isInit = false)
        {
            var money = new MoneyObject
            {
                money = _moneyController.Money
            };

            _saveObject.moneyInfo = money;
            
            if (!isInit)
                Save();
        }
        
        public void SaveLevel(bool isInit = false)
        {
            var level = new LevelObject
            {
                level = _levelController.Level,
                experience = _levelController.Experience
            };

            _saveObject.levelInfo = level;
            
            if (!isInit)
                Save();
        }
        
        public void SaveStores(bool isInit = false)
        {
            var rawList = new List<RawLoadObject>();
            foreach (var raw in _rawController.GetAllRaw())
            {
                var rawObject = new RawLoadObject
                {
                    key = raw.Key,
                    count = raw.Count,
                    level = raw.Level
                };
                
                rawList.Add(rawObject);
            }
            
            var productList = new List<ProductLoadObject>();
            foreach (var product in _productsController.GetAllProducts())
            {
                //TODO Убрать после имплементации уровней
                if (product.IsEmpty())
                    continue;
                
                var itemObject = new ProductLoadObject
                {
                    key = product.Key,
                    count = product.Count,
                    experience = product.Experience
                };
                
                productList.Add(itemObject);
            }
            
            var stores = new StoresLoadObject
            {
                raw = rawList,
                products = productList
            };

            _saveObject.storesInfo = stores;
            
            if (!isInit)
                Save();
        }
        
        public void SaveUnits(bool isInit = false)
        {
            var unitsList = new List<UnitLoadObject>();
            foreach (var unit in _unitsController.GetUnits())
            {
                var unitObject = new UnitLoadObject
                {
                    key = unit.Key,
                    experience = unit.Experience,
                    level = unit.Level,
                    isLocked = unit.IsLocked,
                    outfit = unit.Outfit
                };
                
                unitsList.Add(unitObject);
            }
            
            var units = new UnitsLoadObject
            {
                groupCount = _unitsController.GroupCount,
                units = unitsList
            };

            _saveObject.unitsInfo = units;
            
            if (!isInit)
                Save();
        }
        
        public void SaveExpeditions(bool isInit = false)
        {
            var expeditionList = new List<ExpeditionLoadObject>();
            foreach (var expeditionData in _expeditionController.GetAllExpeditions())
            {
                var expeditionObject = new ExpeditionLoadObject
                {
                    id = expeditionData.Id,
                    key = expeditionData.Key,
                    star = expeditionData.Star,
                    units = expeditionData.Units,
                    timeEnd = expeditionData.TimeEnd
                };
                
                expeditionList.Add(expeditionObject);
            }

            var expeditions = new ExpeditionsLoadObject
            {
                count = _expeditionController.CellCount,
                expeditions = expeditionList,
            };

            _saveObject.expeditionsInfo = expeditions;
            
            if (!isInit)
                Save();
        }
        
        public void SaveProduction(bool isInit = false)
        {
            var productionList = new List<ProductionLoadObject>();
            foreach (var productionData in _productionController.GetAllProduction())
            {
                var productionObject = new ProductionLoadObject
                {
                    id = productionData.Id,
                    key = productionData.Key,
                    star = productionData.Star,
                    timeEnd = productionData.TimeEnd
                };
                
                productionList.Add(productionObject);
            }

            var productions = new ProductionsLoadObject
            {
                level = _productionController.Level,
                count = _productionController.CellCount,
                production = productionList,
            };

            _saveObject.productionsInfo = productions;
            
            if (!isInit)
                Save();
        }
        
        public void SaveOrders(bool isInit = false)
        {
            var ordersList = new List<OrderDataLoadObject>();
            var sortedList = _orderController.OrdersList
                .Where(x => x.isActive || x.isComplete).ToList();
            foreach (var orderData in sortedList)
            {
                var orderObject = new OrderDataLoadObject
                {
                    key = orderData.key,
                    isActive = orderData.isActive,
                    isComplete = orderData.isComplete
                };
                
                ordersList.Add(orderObject);
            }
            
            var orders = new OrdersLoadObject
            {
                orders = ordersList,
                count = _orderController.OrderCount,
                timeRefresh = _orderController.RefreshTime
            };

            _saveObject.ordersInfo = orders;
            
            if (!isInit)
                Save();
        }

        public void InitSave()
        {
            SaveSettings(true);
            SaveMoney(true);
            SaveLevel(true);
            SaveStores(true);
            SaveUnits(true);
            SaveExpeditions(true);
            SaveProduction(true);
            SaveOrders(true);
        }

        public void CreateSave()
        {
            InitSave();
            Save();
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(_saveObject);
            File.WriteAllText(Constants.SavePath, json);
        }
    }
}