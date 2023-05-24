using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RoboFactory.General.Expedition;
using RoboFactory.General.Item;
using RoboFactory.General.Item.Production;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Level;
using RoboFactory.General.Location;
using RoboFactory.General.Money;
using RoboFactory.General.Order;
using RoboFactory.General.Unit;
using RoboFactory.Utils;
using Zenject;

namespace RoboFactory.General.User
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