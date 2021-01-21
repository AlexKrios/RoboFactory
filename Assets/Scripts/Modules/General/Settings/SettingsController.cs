using Modules.General.Settings.Models;
using UnityEngine;
using Utils;

namespace Modules.General.Settings
{
    public class SettingsController : ISettingsController
    {
        public string Version { get; set; }
        
        public LanguageType Language { get; set; }
        public GraphicsType Graphics { get; set; }
        
        public float AudioVolume { get; set; }
        public float MusicVolume { get; set; }
        
        public void LoadSettingsData(SettingsLoadObject data)
        {
            Language = EnumParse.ParseStringToEnum<LanguageType>(data.language);
            Graphics = EnumParse.ParseStringToEnum<GraphicsType>(data.graphics);

            AudioVolume = data.audioVolume;
            MusicVolume = data.musicVolume;
            
            QualitySettings.SetQualityLevel((int) Graphics);
        }
    }
}