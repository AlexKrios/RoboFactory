using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RoboFactory.General.Audio
{
    [CreateAssetMenu(fileName = "MusicData", menuName = "Scriptable/General/Music List", order = 201)]
    public class MusicScriptable : ScriptableObject
    {
        [SerializeField] private List<MusicObject> _music;

        public List<MusicObject> Music => _music;
    }
    
    [Serializable]
    public struct MusicObject
    {
        [SerializeField] private AssetReference _clipRef;
        [SerializeField] private MusicClipType _type;

        public AssetReference ClipRef => _clipRef;
        public MusicClipType Type => _type;
    }
}