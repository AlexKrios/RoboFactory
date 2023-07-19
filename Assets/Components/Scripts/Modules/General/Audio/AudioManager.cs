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
        [Inject] private readonly AddressableService addressableService;
        [Inject] private readonly SettingsService settingsService;

        private AudioSource _musicSource;

        public void InitMusicSource(AudioSource audioSource)
        {
            _musicSource = audioSource;
        }
        
        public void ChangeAudioVolume(float value)
        {
            settingsService.SetAudioVolume(value);
        }
        
        public void ChangeMusicVolume(float value)
        {
            settingsService.SetMusicVolume(value);
            _musicSource.volume = value / 10;
        }
        
        public async void PlayMusic()
        {
            var random = new Random().Next(0, _settings.MusicList.Music.Count);
            var clipRef = _settings.MusicList.Music[random].ClipRef;
            var audioClip = await addressableService.LoadAssetAsync<AudioClip>(clipRef);
            
            Debug.Log($"<color=#ffb3ff>Audio Manager: Playing sound {audioClip.name}</color>");
            _musicSource.volume = settingsService.MusicVolume / 10;
            _musicSource.PlayOneShot(audioClip);
            
            addressableService.ReleaseAsset(clipRef);
        }
        
        public async void PlayAudio(AudioClipType type)
        {
            var clipRef = _settings.AudioList.Audio.First(x => x.Type == type).ClipRef;
            var audioClip = await addressableService.LoadAssetAsync<AudioClip>(clipRef);
            
            AudioSource.PlayClipAtPoint(audioClip, Vector3.zero, settingsService.AudioVolume / 10);
            
            addressableService.ReleaseAsset(clipRef);
        }
        
        [Serializable]
        public class Settings
        {
            public MusicScriptable _musicList;
            public AudioScriptable _audioList;
            
            public MusicScriptable MusicList => _musicList;
            public AudioScriptable AudioList => _audioList;
        }
    }
}
