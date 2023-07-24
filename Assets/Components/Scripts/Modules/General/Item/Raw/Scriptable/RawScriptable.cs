using UnityEngine;

namespace RoboFactory.General.Item.Raw
{
    [CreateAssetMenu(fileName = "RawData", menuName = "Scriptable/General/Raw/Data", order = 61)]
    public class RawScriptable : ItemScriptable
    {
        public RawScriptable()
        {
            ItemType = ItemType.Raw;
        }

        [SerializeField] private string _rawName;
        [SerializeField] private RawType _rawType;
        [SerializeField] private RecipeObject _recipe;

        public int Index { get; set; }
        public string RawName { get => _rawName; set => _rawName = value; }
        public RawType RawType { get => _rawType; set => _rawType = value; }
        public RecipeObject Recipe { get => _recipe; set => _recipe = value; }
    }
}