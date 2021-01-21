using System;
using System.Linq;
using Modules.General.Asset;
using Modules.General.Audio.Models;
using Modules.General.Settings;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace Modules.General.Audio
{
    public class AudioController : IAudioController
    {
        [Inject] private readonly ISettingsController _settingsController;
        [Inject] private readonly Settings _settings;

        private AudioSource _musicSource;

        public void InitMusicSource(AudioSource audioSource)
        {
            _musicSource = audioSource;
        }
        
        public void ChangeAudioVolume(float value)
        {
            _settingsController.AudioVolume = value;
        }
        
        public void ChangeMusicVolume(float value)
        {
            _settingsController.MusicVolume = value;
            _musicSource.volume = value / 10;
        }
        
        public async void PlayMusic()
        {
            var random = new Random().Next(0, _settings.musicList.Music.Count);
            var clipRef = _settings.musicList.Music[random].ClipRef;
            var audioClip = await AssetsController.LoadAsset<AudioClip>(clipRef);
            
            Debug.Log($"<color=#ffb3ff>Audio Manager: Playing sound {audioClip.name}</color>");
            _musicSource.volume = _settingsController.MusicVolume / 10;
            _musicSource.PlayOneShot(audioClip);
        }
        
        public async void PlayAudio(AudioClipType type)
        {
            var clipRef = _settings.audioList.Audio.First(x => x.Type == type).ClipRef;
            var audioClip = await AssetsController.LoadAsset<AudioClip>(clipRef);
            
            AudioSource.PlayClipAtPoint(audioClip, Vector3.zero, _settingsController.AudioVolume / 10);
        }
        
        [Serializable]
        public class Settings
        {
            public MusicScriptable musicList;
            public AudioScriptable audioList;
        }
    }
}
