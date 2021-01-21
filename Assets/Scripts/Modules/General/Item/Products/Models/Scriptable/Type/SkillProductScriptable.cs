using Modules.General.Item.Products.Models.Types;
using UnityEngine;

namespace Modules.General.Item.Products.Models.Scriptable.Type
{
    [CreateAssetMenu(fileName = "SkillProductData", menuName = "Scriptable/General/Product/Skill", order = 74)]
    public class SkillProductScriptable : ProductScriptable
    {
        public SkillProductScriptable()
        {
            ProductGroup = ProductGroup.Skill;
        }
    }
}