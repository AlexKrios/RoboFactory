using System.Collections.Generic;
using Components.Scripts.Modules.General.Item.Products.Types;
using Components.Scripts.Modules.General.Unit.Object;

namespace Components.Scripts.Modules.General.Unit.Battle.Models
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