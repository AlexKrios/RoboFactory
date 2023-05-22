using System;
using System.Collections.Generic;
using Components.Scripts.Modules.General.Expedition;
using Components.Scripts.Modules.General.Item;
using Components.Scripts.Modules.General.Item.Production;
using Components.Scripts.Modules.General.Item.Production.Object;
using Components.Scripts.Modules.General.Item.Raw;
using Components.Scripts.Modules.General.Item.Raw.Object;
using Components.Scripts.Modules.General.Level;
using Components.Scripts.Modules.General.Location;
using Components.Scripts.Modules.General.Money;
using Components.Scripts.Modules.General.Order.Object;
using Components.Scripts.Modules.General.Unit;
using Components.Scripts.Modules.General.Unit.Object;
using Components.Scripts.Utils;
using Newtonsoft.Json;
using Zenject;

namespace Components.Scripts.Modules.General.User
{
    [Serializable]
    public class UserProfile
    {
        [Inject] private readonly RawManager _rawManager;
        [Inject] private readonly UnitsManager _unitsManager;
        [Inject] private readonly ProductionManager _productionManager;
        [Inject] private readonly ExpeditionManager _expeditionManager;
        
        [JsonProperty("moneySection")] 
        public MoneyObject MoneySection { get; set; }
        
        [JsonProperty("levelSection")] 
        public LevelObject LevelSection { get; set; }
        
        [JsonProperty("storesSection")] 
        public StoresDto StoresSection { get; set; }
        
        [JsonProperty("locationsSection")] 
        public LocationSectionDto LocationsSection { get; set; }
        
        [JsonProperty("unitsSection")] 
        public UnitsLoadObject UnitsSection { get; set; }
        
        [JsonProperty("productionsSection")] 
        public ProductionSectionDto ProductionsSection { get; set; }
        
        [JsonProperty("expeditionsSection")] 
        public ExpeditionSectionDto ExpeditionsSection { get; set; }
        
        [JsonProperty("ordersSection")] 
        public OrdersLoadObject OrdersSection { get; set; } 

        public UserProfile GetStartUserProfile()
        {
            MoneySection = new MoneyObject
            {
                money = 0
            };

            LevelSection = new LevelObject
            {
                level = 1,
                experience = 0
            };

            StoresSection = new StoresDto
            {
                Raw = new Dictionary<string, RawDto>(_rawManager.GetAllRawDto())
            };

            UnitsSection = new UnitsLoadObject
            {
                Units = new Dictionary<string, UnitDto>(_unitsManager.GetAllUnitsDto())
            };

            ProductionsSection = new ProductionSectionDto
            {
                count = 1,
                level = 1,
                Production = new Dictionary<string, ProductionDto>(_productionManager.GetAllProductionDto())
            };

            ExpeditionsSection = new ExpeditionSectionDto
            {
                count = 1,
                Expeditions = new Dictionary<string, ExpeditionDto>(_expeditionManager.GetAllExpeditionDto())
            };

            OrdersSection = new OrdersLoadObject
            {
                Orders = new Dictionary<string, OrderDto>(),
                count = 1,
                timeRefresh = DateUtil.StartOfTheDay(DateTime.Now).ToFileTimeUtc()
            };

            return this;
        }
        
        public class Factory : PlaceholderFactory<UserProfile> { }
    }
}