using System;
using System.Collections.Generic;
using System.Linq;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Item.Raw.Models.Type;
using Modules.General.Unit.Models.Type;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Utils
{
    public class IconUtil : MonoBehaviour
    {
        [SerializeField] private List<RawIconKeyObject> rawIcons;
        [SerializeField] private List<UnitTypeIconKeyObject> unitIcons;
        [SerializeField] private List<ProductGroupIconKeyObject> productGroupIcons;
        [SerializeField] private List<SpecificationIconKeyObject> specificationIcons;

        public List<RawIconKeyObject> RawIcons => rawIcons;
        public List<UnitTypeIconKeyObject> UnitIcons => unitIcons;
        public List<ProductGroupIconKeyObject> ProductGroupIcons => productGroupIcons;
        public List<SpecificationIconKeyObject> SpecificationIcons => specificationIcons;

        public AssetReference GetRawIcon(RawType raw)
        {
            return rawIcons.First(x => x.type == raw).iconRef;
        }

        public Sprite GetUnitIcon(UnitType type)
        {
            return unitIcons.First(x => x.type == type).icon;
        }
        
        public Sprite GetProductGroupIcon(ProductGroup group)
        {
            return productGroupIcons.First(x => x.type == group).icon;
        }
        
        public Sprite GetSpecIcon(SpecType spec)
        {
            return specificationIcons.First(x => x.type == spec).icon;
        }
    }

    [Serializable]
    public class RawIconKeyObject
    {
        public RawType type;
        public AssetReference iconRef;
    }

    [Serializable]
    public class UnitTypeIconKeyObject
    {
        public UnitType type;
        public Sprite icon;
    }
    
    [Serializable]
    public class ProductGroupIconKeyObject
    {
        public ProductGroup type;
        public Sprite icon;
    }

    [Serializable]
    public class SpecificationIconKeyObject
    {
        public SpecType type;
        public Sprite icon;
    }
}
