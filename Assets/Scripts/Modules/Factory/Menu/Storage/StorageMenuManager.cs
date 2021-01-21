using JetBrains.Annotations;
using Modules.Factory.Menu.Storage.Header;
using Modules.Factory.Menu.Storage.Items;
using Modules.Factory.Menu.Storage.Sidebar;
using Modules.Factory.Menu.Storage.Tab;
using Modules.General.Item.Products.Models.Types;

namespace Modules.Factory.Menu.Storage
{
    [UsedImplicitly]
    public class StorageMenuManager
    {
        public ProductGroup ActiveProductGroup { get; set; }

        public HeaderView Header { get; set; }
        public TabsSectionView Tabs { get; set; }
        public ItemsSectionView Items { get; set; }
        public SidebarView Sidebar { get; set; }
        
        public bool IsDefault { get; set; }

        public bool IsItemEmpty => Items.Items.Count == 0;

        public StorageMenuManager()
        {
            Reset();
        }

        public void Reset()
        {
            ActiveProductGroup = ProductGroup.All;
        }
    }
}
