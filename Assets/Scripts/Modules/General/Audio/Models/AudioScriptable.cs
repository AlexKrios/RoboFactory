using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Modules.General.Audio.Models
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable/General/Audio List", order = 202)]
    public class AudioScriptable : ScriptableObject
    {
        [SerializeField] private List<AudioObject> audio;

        public List<AudioObject> Audio => audio;
    }
    
    [Serializable]
    public struct AudioObject
    {
        [SerializeField] private AssetReference clipRef;
        [SerializeField] private AudioClipType type;

        public AssetReference ClipRef => clipRef;
        public AudioClipType Type => type;
    }
}