using System;
using System.Linq;
using JetBrains.Annotations;
using RoboFactory.General.Asset;
using RoboFactory.General.Settings;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace RoboFactory.General.Audio
{
    [UsedImplicitly]
    public class AudioManager
    {
        [Inject] private readonly Settings _settings;
        [Inject] private readonly AssetsManager _assetsManager;
        [Inject] private readonly SettingsManager _settingsManager;

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
            var audioClip = await _assetsManager.LoadAssetAsync<AudioClip>(clipRef);
            
            Debug.Log($"<color=#ffb3ff>Audio Manager: Playing sound {audioClip.name}</color>");
            _musicSource.volume = _settingsManager.MusicVolume / 10;
            _musicSource.PlayOneShot(audioClip);
            
            _assetsManager.ReleaseAsset(clipRef);
        }
        
        public async void PlayAudio(AudioClipType type)
        {
            var clipRef = _settings.audioList.Audio.First(x => x.Type == type).ClipRef;
            var audioClip = await _assetsManager.LoadAssetAsync<AudioClip>(clipRef);
            
            AudioSource.PlayClipAtPoint(audioClip, Vector3.zero, _settingsManager.AudioVolume / 10);
            
            _assetsManager.ReleaseAsset(clipRef);
        }
        
        [Serializable]
        public class Settings
        {
            public MusicScriptable musicList;
            public AudioScriptable audioList;
        }
    }
}
