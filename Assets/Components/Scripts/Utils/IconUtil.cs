using System.Collections.Generic;
using System.Linq;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Unit;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RoboFactory.Utils
{
    public class IconUtil : MonoBehaviour
    {
        [SerializeField] private List<RoboFactory.General.KeyValuePair<RawType, AssetReference>> rawIcons;
        [SerializeField] private List<RoboFactory.General.KeyValuePair<UnitType, AssetReference>> unitIcons;
        [SerializeField] private List<RoboFactory.General.KeyValuePair<ProductGroup, AssetReference>> productGroupIcons;
        [SerializeField] private List<RoboFactory.General.KeyValuePair<SpecType, AssetReference>> specsIcons;

        public List<RoboFactory.General.KeyValuePair<RawType, AssetReference>> RawIcons => rawIcons;
        public List<RoboFactory.General.KeyValuePair<UnitType, AssetReference>> UnitIcons => unitIcons;
        public List<RoboFactory.General.KeyValuePair<ProductGroup, AssetReference>> ProductGroupIcons => productGroupIcons;
        public List<RoboFactory.General.KeyValuePair<SpecType, AssetReference>> SpecsIcons => specsIcons;

        public AssetReference GetRawIcon(RawType raw)
        {
            return rawIcons.First(x => x.Key == raw).Value;
        }

        public AssetReference GetUnitIcon(UnitType type)
        {
            return unitIcons.First(x => x.Key == type).Value;
        }
        
        public AssetReference GetProductGroupIcon(ProductGroup group)
        {
            return productGroupIcons.First(x => x.Key == group).Value;
        }
        
        public AssetReference GetSpecIcon(SpecType spec)
        {
            return specsIcons.First(x => x.Key == spec).Value;
        }
    }
}
