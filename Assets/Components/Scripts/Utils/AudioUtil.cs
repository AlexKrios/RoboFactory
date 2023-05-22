using System;
using System.Collections.Generic;
using System.Linq;
using Components.Scripts.Modules.General.Audio;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Components.Scripts.Utils
{
    public class AudioUtil : MonoBehaviour
    {
        [SerializeField] private List<AssetReference> musicClips;
        [SerializeField] private List<TypeAndAudioObject> audioClips;

        public AssetReference GetMusicClipRef(int index)
        {
            return musicClips[index];
        }
        
        public AssetReference GetAudioClipRef(AudioClipType type)
        {
            return audioClips.First(x => x.type == type).audio;
        }
    }
    
    [Serializable]
    public class TypeAndMusicObject
    {
        public AudioClipType type;
        public AssetReference audio;
    }

    [Serializable]
    public class TypeAndAudioObject
    {
        public AudioClipType type;
        public AssetReference audio;
    }
}
