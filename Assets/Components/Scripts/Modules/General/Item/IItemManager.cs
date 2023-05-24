using Cysharp.Threading.Tasks;

namespace RoboFactory.General.Item
{
    public interface IItemManager
    {
        ItemType ItemType { get; }

        ItemBase GetItem(string key);
        
        UniTask AddItem(string key, int count = 1);
        UniTask RemoveItem(string key, int count = 1);
    }
}