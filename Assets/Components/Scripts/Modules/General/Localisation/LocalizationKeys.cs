using System.Collections.Generic;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Ui;
using RoboFactory.General.Unit;

namespace RoboFactory.General.Localisation
{
    public static class LocalizationKeys
    {
        public static Dictionary<RawType, string> RawKeys { get; }
        public static Dictionary<ProductGroup, string> ProductKeys { get; }
        public static Dictionary<UnitType, string> UnitKeys { get; }
        public static Dictionary<UiType, string> UiKeys { get; }

        public const string SettingsMenuTitleKey = "settings_menu_title";
        public const string ProductionCompleteKey = "craft_cell_complete";
        public const string ProductionMenuTitleKey = "production_menu_title";
        public const string ProductionMenuButtonTextKey = "production_menu_button_title";
        public const string UpgradeTitleKey = "upgrade_title";
        public const string UpgradeButtonTitleKey = "upgrade_button_title";
        public const string StorageMenuTitleKey = "storage_menu_title";
        public const string UnitsMenuTitleKey = "units_menu_title";
        public const string UnitsMenuButtonTitleKey = "units_menu_button_title";
        public const string OrderMenuTitleKey = "order_menu_title";
        public const string OrderMenuButtonTitleKey = "order_menu_button_title";
        public const string ConversionMenuTitleKey = "conversion_menu_title";
        public const string ConversionMenuButtonTitleKey = "conversion_menu_button_title";
        
        public const string SelectButtonTitleKey = "select_button_title";

        static LocalizationKeys()
        {
            RawKeys = new Dictionary<RawType, string>
            {
                {RawType.None, "raw_none"},
                {RawType.All, "raw_all"},
                {RawType.Weapon, "raw_weapon"},
                {RawType.Armor, "raw_armor"},
                {RawType.Engine, "raw_engine"},
                {RawType.Battery, "raw_battery"}
            };

            ProductKeys = new Dictionary<ProductGroup, string>
            {
                {ProductGroup.None, "group_type_none"},
                {ProductGroup.All, "group_type_all"},
                {ProductGroup.Weapon, "group_type_weapon"},
                {ProductGroup.Armor, "group_type_armor"},
                {ProductGroup.Engine, "group_type_engine"},
                {ProductGroup.Battery, "group_type_battery"}
            };
            
            UnitKeys = new Dictionary<UnitType, string>
            {
                {UnitType.None, "unit_none"},
                {UnitType.All, "unit_all"},
                {UnitType.Trooper, "unit_trooper"},
                {UnitType.Defender, "unit_defender"},
                {UnitType.Support, "unit_support"},
                {UnitType.Sniper, "unit_sniper"}
            };
            
            UiKeys = new Dictionary<UiType, string>
            {
                {UiType.DontHaveProductionCells, "popup_dont_have_production_cells"},
                {UiType.DontHaveIngredients, "popup_dont_have_ingredients"},
                {UiType.StorageFull, "popup_storage_full"},
                {UiType.UpgradeProductionQueue, "popup_new_production_cell"}
            };
        }
    }
}