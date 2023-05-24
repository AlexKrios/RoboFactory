using System.Collections.Generic;
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

        [SerializeField] private string rawName;
        [SerializeField] private RawType rawType;
        [SerializeField] private bool isMain;
        [SerializeField] private List<RecipeObject> recipes;

        public int Index { get; set; }
        public string RawName { get => rawName; set => rawName = value; }
        public RawType RawType { get => rawType; set => rawType = value; }
        public bool IsMain { get => isMain; set => isMain = value; }
        public List<RecipeObject> Recipes { get => recipes; set => recipes = value; }
    }
}