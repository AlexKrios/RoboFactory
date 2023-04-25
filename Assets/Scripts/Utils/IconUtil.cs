using System.Linq;
using Modules.General;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Item.Raw.Models.Type;
using Modules.General.Unit.Type;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Utils
{
    public class IconUtil : MonoBehaviour
    {
        [SerializeField] private System.Collections.Generic.List<KeyValuePair<RawType, AssetReference>> rawIcons;
        [SerializeField] private System.Collections.Generic.List<KeyValuePair<UnitType, AssetReference>> unitIcons;
        [SerializeField] private System.Collections.Generic.List<KeyValuePair<ProductGroup, AssetReference>> productGroupIcons;
        [SerializeField] private System.Collections.Generic.List<KeyValuePair<SpecType, AssetReference>> specsIcons;

        public System.Collections.Generic.List<KeyValuePair<RawType, AssetReference>> RawIcons => rawIcons;
        public System.Collections.Generic.List<KeyValuePair<UnitType, AssetReference>> UnitIcons => unitIcons;
        public System.Collections.Generic.List<KeyValuePair<ProductGroup, AssetReference>> ProductGroupIcons => productGroupIcons;
        public System.Collections.Generic.List<KeyValuePair<SpecType, AssetReference>> SpecsIcons => specsIcons;

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
