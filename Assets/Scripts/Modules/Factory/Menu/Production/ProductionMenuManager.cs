using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Factory.Menu.Production.Header;
using Modules.Factory.Menu.Production.Parts;
using Modules.Factory.Menu.Production.Products;
using Modules.Factory.Menu.Production.Sidebar;
using Modules.Factory.Menu.Production.Tab;
using Modules.General.Item.Production.Models.Object;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Unit.Models.Type;
using Utils;

namespace Modules.Factory.Menu.Production
{
    [UsedImplicitly]
    public class ProductionMenuManager
    {
        public const int DefaultStar = 1;
        
        public ProductGroup ActiveProductGroup { get; set; }
        public UnitType ActiveUnitType { get; set; }
        public int ActiveProductType { get; set; }
        
        public ProductionMenuView Menu { get; set; }
        public HeaderView Header { get; set; }
        public StarButtonView Star { get; set; }
        public TabsSectionView Tabs { get; set; }
        public ProductsSectionView Products { get; set; }
        public PartsSectionView Parts { get; set; }
        public SidebarView Sidebar { get; set; }
        public CreateButtonView Create { get; set; }
        
        private readonly Dictionary<ProductGroup, ProductionSettingsObject> _productionSettings;

        public ProductionMenuManager()
        {
            _productionSettings = new Dictionary<ProductGroup, ProductionSettingsObject>();
            
            var groupArray = Enum.GetNames(typeof(ProductGroup));
            foreach (var group in groupArray)
            {
                var settings = new ProductionSettingsObject
                {
                    Key = group,
                    Level = 1
                };
                    
                _productionSettings.Add(EnumParse.ParseStringToEnum<ProductGroup>(group), settings);
            }
            
            Reset();
        }

        public void Reset()
        {
            ActiveProductGroup = ProductGroup.Weapon;
            ActiveUnitType = UnitType.Trooper;
            ActiveProductType = 1;
        }
    }
}
