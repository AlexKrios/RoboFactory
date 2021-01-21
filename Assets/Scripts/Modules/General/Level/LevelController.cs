using System;
using System.Linq;
using Modules.General.Level.Models;
using Modules.General.Save;
using Modules.General.Scriptable;
using Zenject;

namespace Modules.General.Level
{
    public class LevelController : ILevelController
    {
        [Inject] private readonly ISaveController _saveController;
        [Inject] private readonly Settings _settings;

        private const int DefaultExperience = 0;
        private const int DefaultLevel = 1;
        
        public Action OnExperienceSet { get; set; }
        public Action OnLevelSet { get; set; }

        public int Experience { get; private set; }
        public int Level { get; private set; }

        public LevelController()
        {
            Experience = DefaultExperience;
            Level = DefaultLevel;
        }
        
        public void LoadData(LevelObject obj)
        {
            Experience = obj.experience;
            Level = GetCurrentLevel();

            OnExperienceSet?.Invoke();
            OnLevelSet?.Invoke();
        }
        
        public void SetExperience(int experience)
        {
            Experience += experience;
            if (Experience >= GetCurrentLevelCap())
            {
                Level++;
                OnLevelSet?.Invoke();
            }

            OnExperienceSet?.Invoke();
            _saveController.SaveLevel();
        }

        private int GetCurrentLevel()
        {
            return _settings.data.Caps.First(x => x.experience > Experience).level;
        }
        
        public int GetPreviousLevelCap()
        {
            return Level == 1 ? 0 : _settings.data.Caps.First(x => x.level == Level - 1).experience;
        }

        public int GetCurrentLevelCap()
        {
            return _settings.data.Caps.First(x => x.level == Level).experience;
        }
        
        [Serializable]
        public class Settings
        {
            public LevelCapsScriptable data;
        }
    }
}