using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RoboFactory.General.Audio
{
    [CreateAssetMenu(fileName = "MusicData", menuName = "Scriptable/General/Music List", order = 201)]
    public class MusicScriptable : ScriptableObject
    {
        [SerializeField] private List<MusicObject> music;

        public List<MusicObject> Music => music;
    }
    
    [Serializable]
    public struct MusicObject
    {
        [SerializeField] private AssetReference clipRef;
        [SerializeField] private MusicClipType type;

        public AssetReference ClipRef => clipRef;
        public MusicClipType Type => type;
    }
}