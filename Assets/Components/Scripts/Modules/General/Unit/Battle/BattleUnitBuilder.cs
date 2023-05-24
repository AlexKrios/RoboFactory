using System.Collections.Generic;
using RoboFactory.General.Item.Products;

namespace RoboFactory.General.Unit.Battle
{
    public class BattleUnitBuilder
    {
        public BattleUnitObject Create(UnitObject data)
        {
            return new BattleUnitObject
            {
                CurrentSpecification = new Dictionary<SpecType, int>(data.Specs),
                
                Info = data
            };
        }
    }
}