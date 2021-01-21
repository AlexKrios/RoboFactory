using Modules.General.Item.Models.Recipe;
using Modules.General.Item.Models.Scriptable;
using Modules.General.Item.Models.Types;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Unit.Models.Type;
using UnityEngine;

namespace Modules.General.Item.Products.Models.Scriptable
{
    public class ProductScriptable : ItemScriptable
    {
        public ProductScriptable()
        {
            ItemType = ItemType.Item;
        }
        
        [SerializeField] private int index;
        [SerializeField] private string productName;
        [SerializeField] private int star;
        [SerializeField] private UnitType unitType;
        [SerializeField] private ProductGroup productGroup;
        [SerializeField] private int productType;
        [SerializeField] private bool isProduct;
        [SerializeField] private GameObject model;
        [SerializeField] private RecipeObject recipe;
        
        public int Index { get => index; set => index = value; }
        public string ProductName { get => productName; set => productName = value; }
        public int Star { get => star; set => star = value; }
        public UnitType UnitType { get => unitType; set => unitType = value; }
        public ProductGroup ProductGroup { get => productGroup; set => productGroup = value; }
        public int ProductType { get => productType; set => productType = value; }
        public bool IsProduct { get => isProduct; set => isProduct = value; }
        public GameObject Model { get => model; set => model = value; }
        public RecipeObject Recipe { get => recipe; set => recipe = value; }
    }
}