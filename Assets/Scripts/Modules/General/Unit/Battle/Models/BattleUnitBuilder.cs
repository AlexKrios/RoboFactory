using System.Collections.Generic;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Unit.Models.Object;

namespace Modules.General.Unit.Battle.Models
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