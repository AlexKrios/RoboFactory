using Modules.General.Audio.Models;
using UnityEngine;

namespace Modules.General.Audio
{
    public interface IAudioController
    {
        void InitMusicSource(AudioSource audioSource);
        
        void ChangeAudioVolume(float value);
        void ChangeMusicVolume(float value);
        
        void PlayMusic();
        void PlayAudio(AudioClipType type);
    }
}