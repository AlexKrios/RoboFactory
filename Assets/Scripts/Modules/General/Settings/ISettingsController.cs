using Modules.General.Settings.Models;

namespace Modules.General.Settings
{
    public interface ISettingsController
    {
        string Version { get; set; }
        LanguageType Language { get; set; }
        GraphicsType Graphics { get; set; }
        
        float AudioVolume { get; set; }
        float MusicVolume { get; set; }

        void LoadSettingsData(SettingsLoadObject data);
    }
}
