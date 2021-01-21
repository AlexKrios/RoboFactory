using JetBrains.Annotations;
using Modules.Factory.Menu.Units.Header;
using Modules.Factory.Menu.Units.Info;
using Modules.Factory.Menu.Units.Roster;
using Modules.Factory.Menu.Units.Selection;
using Modules.Factory.Menu.Units.Sidebar;
using Modules.Factory.Menu.Units.Tab;
using Modules.General.Unit.Models.Type;

namespace Modules.Factory.Menu.Units
{
    [UsedImplicitly]
    public class UnitsMenuManager
    {
        public const int DefaultStar = 1;
        
        public UnitType ActiveUnitType { get; set; }

        public HeaderView Header { get; set; }
        public TabsSectionView Tabs { get; set; }
        public RosterSectionView Roster { get; set; }
        public InfoView Info { get; set; }
        public SidebarView Sidebar { get; set; }
        
        public SelectionPopupView Selection { get; set; }

        public UnitsMenuManager()
        {
            Reset();
        }

        public void Reset()
        {
            ActiveUnitType = UnitType.None;
        }
    }
}
