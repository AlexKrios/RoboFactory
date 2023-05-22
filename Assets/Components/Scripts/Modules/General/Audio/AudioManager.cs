using System;
using System.Linq;
using Components.Scripts.Modules.General.Asset;
using Components.Scripts.Modules.General.Settings;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace Components.Scripts.Modules.General.Audio
{
    [UsedImplicitly]
    public class AudioManager
    {
        [Inject] private readonly SettingsManager _settingsManager;
        [Inject] private readonly Settings _settings;

        private AudioSource _musicSource;

        public void InitMusicSource(AudioSource audioSource)
        {
            _musicSource = audioSource;
        }
        
        public void ChangeAudioVolume(float value)
        {
            _settingsManager.SetAudioVolume(value);
        }
        
        public void ChangeMusicVolume(float value)
        {
            _settingsManager.SetMusicVolume(value);
            _musicSource.volume = value / 10;
        }
        
        public async void PlayMusic()
        {
            var random = new Random().Next(0, _settings.musicList.Music.Count);
            var clipRef = _settings.musicList.Music[random].ClipRef;
            var audioClip = await AssetsManager.LoadAsset<AudioClip>(clipRef);
            
            Debug.Log($"<color=#ffb3ff>Audio Manager: Playing sound {audioClip.name}</color>");
            _musicSource.volume = _settingsManager.MusicVolume / 10;
            _musicSource.PlayOneShot(audioClip);
        }
        
        public async void PlayAudio(AudioClipType type)
        {
            var clipRef = _settings.audioList.Audio.First(x => x.Type == type).ClipRef;
            var audioClip = await AssetsManager.LoadAsset<AudioClip>(clipRef);
            
            AudioSource.PlayClipAtPoint(audioClip, Vector3.zero, _settingsManager.AudioVolume / 10);
        }
        
        [Serializable]
        public class Settings
        {
            public MusicScriptable musicList;
            public AudioScriptable audioList;
        }
    }
}
