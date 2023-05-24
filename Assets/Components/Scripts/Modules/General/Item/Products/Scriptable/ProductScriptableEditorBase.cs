using System.Collections.Generic;
using UnityEditor;

#if UNITY_EDITOR
namespace RoboFactory.General.Item.Products
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