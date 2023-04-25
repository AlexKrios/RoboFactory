using System.Collections.Generic;
using Modules.General.Item.Products.Models.Object.Types;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Unit.Type;

namespace Modules.General.Item.Models
{
    public class ItemPath
    {
        public readonly List<string> PathList = new()
        {
            /* ----- Weapon ----- */
            $"Data/Products/{UnitType.Trooper}/{ProductGroup.Weapon}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Defender}/{ProductGroup.Weapon}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Support}/{ProductGroup.Weapon}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Sniper}/{ProductGroup.Weapon}/{WeaponType.Star1}",
            /* ----- Armor ----- */
            $"Data/Products/{UnitType.Trooper}/{ProductGroup.Armor}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Defender}/{ProductGroup.Armor}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Support}/{ProductGroup.Armor}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Sniper}/{ProductGroup.Armor}/{WeaponType.Star1}",
            /* ----- Equipment ----- */
            $"Data/Products/{UnitType.Trooper}/{ProductGroup.Engine}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Defender}/{ProductGroup.Engine}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Support}/{ProductGroup.Engine}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Sniper}/{ProductGroup.Engine}/{WeaponType.Star1}",
            /* ----- Skill ----- */
            $"Data/Products/{UnitType.Trooper}/{ProductGroup.Battery}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Defender}/{ProductGroup.Battery}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Support}/{ProductGroup.Battery}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Sniper}/{ProductGroup.Battery}/{WeaponType.Star1}"
        };
    }
}