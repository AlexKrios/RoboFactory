using Modules.Factory.Menu.Expedition.Locations;
using Modules.Factory.Menu.Expedition.Selection;
using Modules.Factory.Menu.Expedition.Sidebar;
using Modules.Factory.Menu.Expedition.Units;

namespace Modules.Factory.Menu.Expedition
{
    public class ExpeditionMenuManager
    {
        public const int DefaultStar = 1;
        
        public UnitsSectionView Units { get; set; }
        public LocationsSectionView Locations { get; set; }
        public SelectionPopupView Selection { get; set; }
        public StarButtonView Star { get; set; }
    }
}
