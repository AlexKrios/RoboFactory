using Modules.General.Item.Models;
using Modules.General.Item.Models.Types;

namespace Modules.General.Item
{
    public interface IGetItem
    {
        ItemType ItemType { get; }

        ItemBase GetItem(string key);
    }
}