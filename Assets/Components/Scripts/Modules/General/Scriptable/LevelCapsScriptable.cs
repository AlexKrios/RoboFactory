using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components.Scripts.Modules.General.Scriptable
{
    [CreateAssetMenu(fileName = "LevelCaps", menuName = "Scriptable/General/Level Caps", order = 0)]
    public class LevelCapsScriptable : ScriptableObject
    {
        [SerializeField] private List<LevelCap> caps;

        public List<LevelCap> Caps => caps;
    }

    [Serializable]
    public class LevelCap
    {
        public int level;
        public int experience;
    }
}
