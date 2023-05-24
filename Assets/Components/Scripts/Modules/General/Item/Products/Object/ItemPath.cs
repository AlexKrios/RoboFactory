using System.Collections.Generic;
using RoboFactory.General.Unit;

namespace RoboFactory.General.Item.Products
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