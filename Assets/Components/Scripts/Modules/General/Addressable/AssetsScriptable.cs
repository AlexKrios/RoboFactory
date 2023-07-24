using System;
using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Unit;
using UnityEngine;
using UnityEngine.AddressableAssets;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Asset
{
    [CreateAssetMenu(fileName = "AssetsData", menuName = "Scriptable/General/Assets Data", order = 1)]
    public class AssetsScriptable : ScriptableObject
    {
        [SerializeField] private List<RawIconObject> _rawIcons;
        [SerializeField] private List<UnitIconObject> _unitIcons;
        [SerializeField] private List<ProductGroupIconObject> _productGroupIcons;
        [SerializeField] private List<SpecIconObject> _specsIcons;

        public List<RawIconObject> RawIcons => _rawIcons;
        public List<UnitIconObject> UnitIcons => _unitIcons;
        public List<ProductGroupIconObject> ProductGroupIcons => _productGroupIcons;
        public List<SpecIconObject> SpecsIcons => _specsIcons;

        public AssetReference GetRawIcon(RawType raw) => _rawIcons.First(x => x.Type == raw).IconRef;

        public AssetReference GetUnitIcon(UnitType type) => _unitIcons.First(x => x.Type == type).IconRef;
        
        public AssetReference GetProductGroupIcon(ProductGroup group) => _productGroupIcons.First(x => x.Type == group).IconRef;
        
        public AssetReference GetSpecIcon(SpecType spec) => _specsIcons.First(x => x.Type == spec).IconRef;
    }

    [Serializable]
    public class RawIconObject
    {
        public RawType Type;
        public AssetReference IconRef;
    }
    
    [Serializable]
    public class UnitIconObject
    {
        public UnitType Type;
        public AssetReference IconRef;
    }
    
    [Serializable]
    public class ProductGroupIconObject
    {
        public ProductGroup Type;
        public AssetReference IconRef;
    }
    
    [Serializable]
    public class SpecIconObject
    {
        public SpecType Type;
        public AssetReference IconRef;
    }
}
