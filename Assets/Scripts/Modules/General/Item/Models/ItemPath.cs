using System.Collections.Generic;
using Modules.General.Item.Products.Models.Object.Types;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Unit.Models.Type;

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
            $"Data/Products/{UnitType.Trooper}/{ProductGroup.Equipment}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Defender}/{ProductGroup.Equipment}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Support}/{ProductGroup.Equipment}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Sniper}/{ProductGroup.Equipment}/{WeaponType.Star1}",
            /* ----- Skill ----- */
            $"Data/Products/{UnitType.Trooper}/{ProductGroup.Skill}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Defender}/{ProductGroup.Skill}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Support}/{ProductGroup.Skill}/{WeaponType.Star1}",
            $"Data/Products/{UnitType.Sniper}/{ProductGroup.Skill}/{WeaponType.Star1}"
        };
    }
}