using Components.Scripts.Modules.General.Item.Models;
using Components.Scripts.Modules.General.Item.Models.Types;
using Cysharp.Threading.Tasks;

namespace Components.Scripts.Modules.General.Item
{
    public interface IItemManager
    {
        ItemType ItemType { get; }

        ItemBase GetItem(string key);
        
        UniTask AddItem(string key, int count = 1);
        UniTask RemoveItem(string key, int count = 1);
    }
}