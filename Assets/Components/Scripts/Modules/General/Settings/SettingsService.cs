using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Services;
using UnityEngine;

namespace RoboFactory.General.Settings
{
    [UsedImplicitly]
    public class SettingsService : Service
    {
        protected override string LoadingTextKey => string.Empty;
        
        private const string LanguageName = "Language";
        private const string GraphicsName = "Graphics";
        private const string MusicVolumeName = "MusicVolume";
        private const string AudioVolumeName = "AudioVolume";

        public string Version { get; set; }
        
        public LanguageType Language { get; set; }
        public GraphicsType Graphics { get; set; }
        
        public float AudioVolume { get; private set; }
        public float MusicVolume { get; private set; }
        
        protected override UniTask InitializeAsync()
        {
            Language = (LanguageType) PlayerPrefs.GetInt(LanguageName, 0);
            Graphics = (GraphicsType) PlayerPrefs.GetInt(GraphicsName, 0);

            MusicVolume = PlayerPrefs.GetInt(MusicVolumeName, 10);
            AudioVolume = PlayerPrefs.GetInt(AudioVolumeName, 10);

            QualitySettings.SetQualityLevel((int) Graphics);

            return UniTask.CompletedTask;
        }

        public void SetLanguage(LanguageType value)
        {
            Language = value;
            PlayerPrefs.SetInt(LanguageName, (int) value);
        }
        
        public void SetGraphics(GraphicsType value)
        {
            Graphics = value;
            PlayerPrefs.SetInt(GraphicsName, (int) value);
            QualitySettings.SetQualityLevel((int) value);
        }

        public void SetMusicVolume(float value)
        {
            MusicVolume = value;
            PlayerPrefs.SetInt(MusicVolumeName, (int) value);
        }
        
        public void SetAudioVolume(float value)
        {
            AudioVolume = value;
            PlayerPrefs.SetInt(AudioVolumeName, (int) value);
        }
    }
}