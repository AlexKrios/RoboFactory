using System.Collections.Generic;
using System.Linq;
using Components.Scripts.Modules.General.Item.Models.Types;
using JetBrains.Annotations;
using Zenject;

namespace Components.Scripts.Modules.General.Item
{
    [UsedImplicitly]
    public class ManagersResolver
    {
        [Inject] private readonly List<IItemManager> _itemManagers;

        public IItemManager GetManagerByType(ItemType itemType)
        {
            return _itemManagers.First(x => x.ItemType == itemType);
        }
    }
}
