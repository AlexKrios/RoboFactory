using System.Collections.Generic;
using Components.Scripts.Modules.General.Item.Products.Object.Types;
using Components.Scripts.Modules.General.Item.Products.Types;
using Components.Scripts.Modules.General.Unit.Type;

namespace Components.Scripts.Modules.General.Item.Products.Object
{
    public class ItemPath
    {
        public readonly List<string> PathList = new()
        {
            $"Products/{UnitType.Trooper}/{ProductGroup.Weapon}/{WeaponType.Star1}",
            $"Products/{UnitType.Defender}/{ProductGroup.Weapon}/{WeaponType.Star1}",
            $"Products/{UnitType.Support}/{ProductGroup.Weapon}/{WeaponType.Star1}",
            $"Products/{UnitType.Sniper}/{ProductGroup.Weapon}/{WeaponType.Star1}",

            $"Products/{UnitType.Trooper}/{ProductGroup.Armor}/{WeaponType.Star1}",
            $"Products/{UnitType.Defender}/{ProductGroup.Armor}/{WeaponType.Star1}",
            $"Products/{UnitType.Support}/{ProductGroup.Armor}/{WeaponType.Star1}",
            $"Products/{UnitType.Sniper}/{ProductGroup.Armor}/{WeaponType.Star1}",

            $"Products/{UnitType.Trooper}/{ProductGroup.Engine}/{WeaponType.Star1}",
            $"Products/{UnitType.Defender}/{ProductGroup.Engine}/{WeaponType.Star1}",
            $"Products/{UnitType.Support}/{ProductGroup.Engine}/{WeaponType.Star1}",
            $"Products/{UnitType.Sniper}/{ProductGroup.Engine}/{WeaponType.Star1}",

            $"Products/{UnitType.Trooper}/{ProductGroup.Battery}/{WeaponType.Star1}",
            $"Products/{UnitType.Defender}/{ProductGroup.Battery}/{WeaponType.Star1}",
            $"Products/{UnitType.Support}/{ProductGroup.Battery}/{WeaponType.Star1}",
            $"Products/{UnitType.Sniper}/{ProductGroup.Battery}/{WeaponType.Star1}"
        };
    }
}