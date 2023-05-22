using System;
using System.Collections.Generic;
using Components.Scripts.Modules.General.Item.Products.Object;
using UnityEngine;

namespace Components.Scripts.Modules.General.Item.Models.Recipe
{
    [Serializable]
    public class RecipeObject
    {
        [SerializeField] private int star;
        [SerializeField] private List<PartObject> parts;
        [SerializeField] private int cost;
        [SerializeField] private int experience;
        [SerializeField] private int craftTime;
        [SerializeField] private List<SpecObject> specs;

        public int Star { get => star; set => star = value; }
        public List<PartObject> Parts { get => parts; set => parts = value; }
        public int Cost { get => cost; set => cost = value; }
        public int Experience { get => experience; set => experience = value; }
        public int CraftTime { get => craftTime; set => craftTime = value; }
        public List<SpecObject> Specs { get => specs; set => specs = value; }
    }
}
