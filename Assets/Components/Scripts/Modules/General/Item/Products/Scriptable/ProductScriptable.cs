using RoboFactory.General.Item.Products;
using RoboFactory.General.Unit;
using UnityEngine;

namespace RoboFactory.General.Item
{
    public class ProductScriptable : ItemScriptable
    {
        public ProductScriptable()
        {
            ItemType = ItemType.Product;
        }
        
        [SerializeField] private int _index;
        [SerializeField] private string _productName;
        [SerializeField] private int _star;
        [SerializeField] private UnitType _unitType;
        [SerializeField] private ProductGroup _productGroup;
        [SerializeField] private int _productType;
        [SerializeField] private bool _isProduct;
        [SerializeField] private GameObject _model;
        [SerializeField] private RecipeObject _recipe;
        
        public int Index { get => _index; set => _index = value; }
        public string ProductName { get => _productName; set => _productName = value; }
        public int Star { get => _star; set => _star = value; }
        public UnitType UnitType { get => _unitType; set => _unitType = value; }
        public ProductGroup ProductGroup { get => _productGroup; set => _productGroup = value; }
        public int ProductType { get => _productType; set => _productType = value; }
        public bool IsProduct { get => _isProduct; set => _isProduct = value; }
        public GameObject Model { get => _model; set => _model = value; }
        public RecipeObject Recipe { get => _recipe; set => _recipe = value; }
    }
}