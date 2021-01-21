using Modules.Factory.Menu.Conversion.Components;
using Modules.Factory.Menu.Conversion.Parts;
using Modules.Factory.Menu.Conversion.Tabs;

namespace Modules.Factory.Menu.Conversion
{
    public class ConversionMenuManager
    {
        public const int DefaultStar = 2;
        
        public StarButtonView Star { get; set; }
        public TabsSectionView Tabs { get; set; }
        public PartsSectionView Parts { get; set; }
        public ConvertButtonView Convert { get; set; }
    }
}
