using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace RoboFactory.General.Scriptable
{
    [CreateAssetMenu(fileName = "LevelCaps", menuName = "Scriptable/General/Level Caps", order = 0)]
    public class LevelCapsScriptable : ScriptableObject
    {
        [SerializeField] private List<LevelCap> _caps;

        public List<LevelCap> Caps => _caps;
    }

    [Serializable]
    public class LevelCap
    {
        public int Level;
        public int Experience;
    }
}
