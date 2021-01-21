/*using System;
using System.Collections.Generic;
using Modules.General.Item.Models;
using Modules.General.Item.Models.Recipe;
using Modules.General.Item.Models.Scriptable;
using Modules.General.Item.Products.Models.Object.Spec;
using Modules.General.Item.Products.Models.Types;
using Modules.General.Unit.Models.Type;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Modules.General.Item.Products.Models.Scriptable
{
    [CustomEditor(typeof(ProductScriptable))]
    [CanEditMultipleObjects]
    public class ProductScriptableEditor : Editor
    {
        private ProductScriptable _product;

        private int _productQualityCount;
        private bool _recipeSectionFoldout;
        private List<bool> _recipesFoldouts;
        private List<bool> _partsFoldouts;
        private List<bool> _specificationsFoldouts;

        private void Awake()
        {
            _productQualityCount = Enum.GetValues(typeof(ItemQuality)).Length;
            _recipesFoldouts = new List<bool>();
            _partsFoldouts = new List<bool>();
            _specificationsFoldouts = new List<bool>();
            
            _product = (ProductScriptable) target;

            if (_product.Recipes == null)
                _product.Recipes = new List<RecipeObject>();

            for (var i = 0; i < 5; i++)
            {
                _recipesFoldouts.Add(false);
                _partsFoldouts.Add(false);
                _specificationsFoldouts.Add(false);
            }
            
            EditorUtility.SetDirty(_product);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginVertical("Box");
            CreateProduct();
            EditorGUILayout.EndVertical();

            EditorUtility.SetDirty(_product);
        }

        private void CreateProduct()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            EditorGUILayout.LabelField($"{_product.ProductGroup}: {_product.ProductName}");
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            _product.ProductName = EditorGUILayout.TextField("Name:", _product.ProductName);
            _product.Key = EditorGUILayout.TextField("Key:", _product.Key);
            //_product.Icon = (Sprite) EditorGUILayout.ObjectField("Icon:", _product.Icon, typeof(Sprite), false);
            
            EditorGUILayout.Space(10);
            
            _product.UnitType = (UnitType) EditorGUILayout.EnumPopup("Character Type:", _product.UnitType);
            _product.ProductGroup = (ProductGroup) EditorGUILayout.EnumPopup("Product Group:", _product.ProductGroup);
            _product.ProductType = (ProductType) EditorGUILayout.EnumPopup("Type:", _product.ProductType);
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            _product.IsProduct = EditorGUILayout.Toggle("Is product:", _product.IsProduct);
            if (_product.IsProduct)
            {
                _product.Model = (GameObject) EditorGUILayout.ObjectField("Model:", _product.Model, typeof(GameObject), false);
            }
            EditorGUILayout.EndVertical();

            CreateRecipeSection();
        }
        
        private void CreateRecipeSection()
        {
            var recipes = _product.Recipes;
            
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginHorizontal(EditorStyles.objectField);
            _recipeSectionFoldout = EditorGUILayout.Foldout(_recipeSectionFoldout, $"Recipes: {recipes.Count}");
            EditorGUILayout.EndHorizontal();
            
            if (!_recipeSectionFoldout)
                return;
            
            for (var i = 0; i < recipes.Count; i++)
            {
                EditorGUI.indentLevel = 1;
                
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                _recipesFoldouts[i] = EditorGUILayout.Foldout(_recipesFoldouts[i], recipes[i].Star.ToString());
                if (_recipesFoldouts[i])
                {
                    GUILayout.Space(5);

                    EditorGUILayout.BeginHorizontal();
                    recipes[i].Star = EditorGUILayout.IntField("Star:", recipes[i].Star);
                    EditorGUILayout.EndHorizontal();
                    
                    GUILayout.Space(5); 
                    
                    EditorGUILayout.BeginHorizontal();
                    recipes[i].Cost = EditorGUILayout.IntField("Cost:", recipes[i].Cost);
                    EditorGUILayout.EndHorizontal();
                    
                    EditorGUILayout.BeginHorizontal();
                    recipes[i].Experience = EditorGUILayout.IntField("Experience:", recipes[i].Experience);
                    EditorGUILayout.EndHorizontal();
                    
                    EditorGUILayout.BeginHorizontal();
                    recipes[i].CraftTime = EditorGUILayout.IntField("Craft time:", recipes[i].CraftTime);
                    EditorGUILayout.EndHorizontal();
                    
                    GUILayout.Space(5);
                    
                    CreatePartSection(i);
                    CreateSpecificationSection(i);
                }
                
                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.BeginHorizontal();
            if (recipes.Count != 0)
            {
                if(GUILayout.Button("Remove last recipe"))
                    RemoveLastRecipe();
            }
            if (recipes.Count < _productQualityCount)
            {
                if(GUILayout.Button("Add new recipe"))
                    AddRecipe();
            }
            EditorGUILayout.EndHorizontal();
        }
        
        private void AddRecipe()
        {
            _recipesFoldouts.Add(false);
            _partsFoldouts.Add(false);
            _specificationsFoldouts.Add(false);
            _product.Recipes.Add(new RecipeObject
            {
                Parts = new List<PartObject>(),
                Specs = new List<SpecObject>()
            });
        }

        private void RemoveLastRecipe()
        {
            _recipesFoldouts.RemoveAt(_recipesFoldouts.Count - 1);
            _product.Recipes.RemoveAt(_product.Recipes.Count - 1);
        }
        
        private void CreatePartSection(int index)
        {
            var parts = _product.Recipes[index].Parts;
            
            EditorGUI.indentLevel = 0;
            
            EditorGUILayout.BeginHorizontal("Box");
            EditorGUI.indentLevel++;
            _partsFoldouts[index] = EditorGUILayout.Foldout(_partsFoldouts[index], $"Parts count: {parts.Count}");
            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();
            
            if (!_partsFoldouts[index])
                return;
            
            for (var i = 0; i < parts.Count; i++)
            {
                EditorGUILayout.BeginVertical("Box");
                GUILayout.Space(5);
                EditorGUILayout.BeginHorizontal();
                parts[i].data = (ItemScriptable) EditorGUILayout.ObjectField("Data:", parts[i].data, typeof(ItemScriptable), true);
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                parts[i].count = EditorGUILayout.IntField("Count:", parts[i].count);
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                parts[i].star = EditorGUILayout.IntField("Star:", parts[i].star);
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(5);
                EditorGUILayout.EndVertical();

                if (i != parts.Count - 1)
                    GUILayout.Space(1);
            }
            
            EditorGUI.indentLevel = 0;
            if (parts.Count != 0)
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            
            EditorGUILayout.BeginHorizontal();
            if (parts.Count != 0)
            {
                if (GUILayout.Button("Remove last part"))
                    RemoveLastPart(parts);
            }

            if (parts.Count < 4)
            {
                if(GUILayout.Button("Add new part"))
                    AddPart(parts);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel = 1;
        }
        
        private void AddPart(List<PartObject> parts)
        {
            parts.Add(new PartObject());
        }
        private void RemoveLastPart(List<PartObject> parts)
        {
            parts.RemoveAt(parts.Count - 1);
        }

        private void CreateSpecificationSection(int index)
        {
            if (!_product.IsProduct)
                return;
            
            EditorGUI.indentLevel = 0;
            
            EditorGUILayout.BeginHorizontal("Box");
            EditorGUI.indentLevel++;
            _specificationsFoldouts[index] = EditorGUILayout.Foldout(_specificationsFoldouts[index], $"Specification: {_product.Recipes[index].Specs.Count}");
            EditorGUI.indentLevel--;
            EditorGUILayout.EndHorizontal();
            
            if (!_specificationsFoldouts[index])
                return;

            var specifications = _product.Recipes[index].Specs;
            foreach (var specification in specifications)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.Space(5);
                EditorGUILayout.BeginHorizontal();
                specification.type = (SpecType) EditorGUILayout.EnumPopup("Type:", specification.type);
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                specification.value = EditorGUILayout.IntField("Value:", specification.value);
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(5);
                EditorGUILayout.EndVertical();
            }
            
            EditorGUI.indentLevel = 0;
            if (specifications.Count != 0)
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            
            EditorGUILayout.BeginHorizontal();
            if (specifications.Count != 0)
            {
                if (GUILayout.Button("Remove last specification"))
                    RemoveLastSpecification(specifications);
            }

            if (specifications.Count < 4)
            {
                if(GUILayout.Button("Add new specification"))
                    AddSpecification(specifications);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel = 1;
        }
        
        private void AddSpecification(List<SpecObject> specification)
        {
            specification.Add(new SpecObject());
        }
        private void RemoveLastSpecification(List<SpecObject> specification)
        {
            specification.RemoveAt(specification.Count - 1);
        }
    }
}
#endif*/