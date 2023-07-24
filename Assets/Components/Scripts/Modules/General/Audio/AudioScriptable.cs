using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RoboFactory.General.Audio
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable/General/Audio List", order = 202)]
    public class AudioScriptable : ScriptableObject
    {
        [SerializeField] private List<AudioObject> _audio;

        public List<AudioObject> Audio => _audio;
    }
    
    [Serializable]
    public struct AudioObject
    {
        [SerializeField] private AssetReference _clipRef;
        [SerializeField] private AudioClipType _type;

        public AssetReference ClipRef => _clipRef;
        public AudioClipType Type => _type;
    }
}