using System;

namespace Modules.General.Settings.Models
{
    [Serializable]
    public class SettingsLoadObject
    {
        public string language;
        public string graphics;
        public float musicVolume;
        public float audioVolume;
    }
}