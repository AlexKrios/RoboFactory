using System;
using Modules.General.Item;
using Modules.General.Item.Production.Models.Load;
using Modules.General.Level.Models;
using Modules.General.Location.Model;
using Modules.General.Money.Models;
using Modules.General.Order.Models.Load;
using Modules.General.Settings.Models;
using Modules.General.Unit;

namespace Modules.General
{
    [Serializable]
    public class LoadObject
    {
        public SettingsLoadObject settingsInfo;
        public MoneyObject moneyInfo;
        public LevelObject levelInfo;
        public StoresLoadObject storesInfo;
        public UnitsLoadObject unitsInfo;
        public ProductionsLoadObject productionsInfo;
        public ExpeditionsLoadObject expeditionsInfo;
        public OrdersLoadObject ordersInfo;
    }
}
