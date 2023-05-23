using System.Collections.Generic;
using Components.Scripts.Modules.General.Item.Models.Recipe;
using UnityEditor;

#if UNITY_EDITOR
namespace Components.Scripts.Modules.General.Item.Products.Scriptable
{
    public class ProductScriptableEditorBase : Editor
    {
        protected void AddPart(List<PartObject> parts)
        {
            parts.Add(new PartObject());
        }

        protected void RemoveLastPart(List<PartObject> parts)
        {
            parts.RemoveAt(parts.Count - 1);
        }
    }
}
#endif