using System.Collections.Generic;
using System.Linq;
using Modules.General.Item.Models.Types;
using Zenject;

namespace Modules.General.Item
{
    public class ControllersResolver
    {
        [Inject] private readonly List<IGetItem> _getItems;

        public IGetItem GetStoreByType(ItemType itemType)
        {
            return _getItems.First(x => x.ItemType == itemType);
        }
    }
}
