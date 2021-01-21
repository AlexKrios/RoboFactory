﻿using System.Collections.Generic;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Item.Raw.Models.Type;
using Modules.General.Ui;
using Modules.General.Unit.Models.Type;

namespace Modules.General.Localisation.Models
{
    public static class LocalisationKeys
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

        static LocalisationKeys()
        {
            RawKeys = new Dictionary<RawType, string>
            {
                {RawType.All, "raw_all"},
                {RawType.Skill, "raw_robotic"},
                {RawType.Weapon, "raw_weapon"},
                {RawType.Armor, "raw_armor"},
                {RawType.Equipment, "raw_bionic"}
            };

            ProductKeys = new Dictionary<ProductGroup, string>
            {
                {ProductGroup.All, "group_type_all"},
                {ProductGroup.Skill, "group_type_robotic"},
                {ProductGroup.Weapon, "group_type_weapon"},
                {ProductGroup.Armor, "group_type_armor"},
                {ProductGroup.Equipment, "group_type_equipment"}
            };
            
            UnitKeys = new Dictionary<UnitType, string>
            {
                {UnitType.None, "unit_all"},
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