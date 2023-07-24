using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RoboFactory.General.Item
{
    public class ItemScriptable : ScriptableObject
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private string _key;
        [SerializeField] private AssetReference _iconRef;

        public ItemType ItemType { get => _itemType; set => _itemType = value; }
        public string Key { get => _key; set => _key = value; }
        public AssetReference IconRef { get => _iconRef; set => _iconRef = value; }
    }
}