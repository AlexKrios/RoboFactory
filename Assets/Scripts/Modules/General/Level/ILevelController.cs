using System;
using Modules.General.Level.Models;

namespace Modules.General.Level
{
    public interface ILevelController
    {
        public Action OnExperienceSet { get; set; }
        public Action OnLevelSet { get; set; }
        
        int Experience { get; }
        int Level { get; }

        void LoadData(LevelObject obj);
        void SetExperience(int experience);
        int GetPreviousLevelCap();
        int GetCurrentLevelCap();
    }
}
