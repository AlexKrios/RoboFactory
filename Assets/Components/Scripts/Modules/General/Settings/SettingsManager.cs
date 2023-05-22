using JetBrains.Annotations;
using UnityEngine;

namespace Components.Scripts.Modules.General.Settings
{
    [UsedImplicitly]
    public class SettingsManager
    {
        private const string LanguageName = "Language";
        private const string GraphicsName = "Graphics";
        private const string MusicVolumeName = "MusicVolume";
        private const string AudioVolumeName = "AudioVolume";
        
        public string Version { get; set; }
        
        public LanguageType Language { get; set; }
        public GraphicsType Graphics { get; set; }
        
        public float AudioVolume { get; set; }
        public float MusicVolume { get; set; }

        public void InitData()
        {
            PlayerPrefs.SetInt(LanguageName, 0);
            PlayerPrefs.SetInt(GraphicsName, 0);
            
            PlayerPrefs.SetInt(MusicVolumeName, 10);
            PlayerPrefs.SetInt(AudioVolumeName, 10);
        }
        
        public void LoadData()
        {
            Language = (LanguageType) PlayerPrefs.GetInt(LanguageName, 0);
            Graphics = (GraphicsType) PlayerPrefs.GetInt(GraphicsName, 0);

            MusicVolume = PlayerPrefs.GetInt(MusicVolumeName, 10);
            AudioVolume = PlayerPrefs.GetInt(AudioVolumeName, 10);

            QualitySettings.SetQualityLevel((int) Graphics);
        }
        
        public void SetLanguage(LanguageType value)
        {
            PlayerPrefs.SetInt(LanguageName, (int) value);
        }
        
        public void SetGraphics(GraphicsType value)
        {
            PlayerPrefs.SetInt(GraphicsName, (int) value);
            QualitySettings.SetQualityLevel((int) value);
        }

        public void SetMusicVolume(float value)
        {
            PlayerPrefs.SetInt(MusicVolumeName, (int) value);
        }
        
        public void SetAudioVolume(float value)
        {
            PlayerPrefs.SetInt(AudioVolumeName, (int) value);
        }
    }
}