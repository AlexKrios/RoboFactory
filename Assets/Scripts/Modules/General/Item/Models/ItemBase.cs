﻿using System;
using Modules.General.Item.Models.Recipe;
using Modules.General.Item.Models.Types;
using UnityEngine.AddressableAssets;

namespace Modules.General.Item.Models
{
    public abstract class ItemBase
    {
        public string Key { get; set; }
        
        public ItemType ItemType { get; set; }
        public AssetReference IconRef { get; set; }

        public int Count { get; set; }
        
        public RecipeObject Recipe { get; set; }
        
        public bool IsEmpty() => Count == 0;
        public bool IsEnoughCount(PartObject part) => Count - part.count >= 0;
        public void RemoveCount(PartObject part) => Count -= part.count;
        
        public void IncrementCount() => Count++;
        public void IncrementCount(int count) => Count += count;
        public void DecrementCount() => Count--;
        public void DecrementCount(int count) => Count -= count;
    }

    [Serializable]
    public class CountObject
    {
        public int value;

        public CountObject(int value)
        {
            this.value = value;
        }
    }
    
    [Serializable]
    public class ExperienceObject
    {
        public int value;

        public ExperienceObject(int value)
        {
            this.value = value;
        }
    }
}
