using Components.Scripts.Modules.General.Item.Models.Types;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Components.Scripts.Modules.General.Item.Models.Scriptable
{
    public class ItemScriptable : ScriptableObject
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private string key;
        [SerializeField] private AssetReference iconRef;

        public ItemType ItemType { get => itemType; set => itemType = value; }
        public string Key { get => key; set => key = value; }
        public AssetReference IconRef { get => iconRef; set => iconRef = value; }
    }
}