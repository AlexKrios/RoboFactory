using System.Collections.Generic;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Unit.Models.Scriptable;

namespace Modules.General.Unit.Models.Object
{
    public class UnitBuilder
    {
        public UnitObject Create(UnitScriptable data)
        {
            return new UnitObject
            {
                Key = data.Key,

                UnitType = data.UnitType,
                AttackType = data.AttackType,

                IconRef = data.IconRef,
                Model = data.Model,
                
                Specs = CreateSpecificationDictionary(data.Specifications),

                Experience = 0,
                Level = 1,

                IsLocked = false,
                
                Outfit = new List<string>
                {
                    Constants.EmptyOutfit, 
                    Constants.EmptyOutfit, 
                    Constants.EmptyOutfit, 
                    Constants.EmptyOutfit
                }
            };
        }

        private Dictionary<SpecType, int> CreateSpecificationDictionary(List<int> specsValue)
        {
            var dict = new Dictionary<SpecType, int>();
            for (var i = 0; i < specsValue.Count; i++)
            {
                dict.Add((SpecType) i, specsValue[i]);
            }

            return dict;
        }
    }
}
