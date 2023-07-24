using System;
using System.Collections.Generic;
using RoboFactory.General.Item.Products;
using UnityEngine;

namespace RoboFactory.General.Item
{
    [Serializable]
    public class RecipeObject
    {
        [SerializeField] private int _star;
        [SerializeField] private List<PartObject> _parts;
        [SerializeField] private int _cost;
        [SerializeField] private int _experience;
        [SerializeField] private int _craftTime;
        [SerializeField] private List<SpecObject> _specs;

        public int Star { get => _star; set => _star = value; }
        public List<PartObject> Parts { get => _parts; set => _parts = value; }
        public int Cost { get => _cost; set => _cost = value; }
        public int Experience { get => _experience; set => _experience = value; }
        public int CraftTime { get => _craftTime; set => _craftTime = value; }
        public List<SpecObject> Specs { get => _specs; set => _specs = value; }
    }
}
