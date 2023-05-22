using System;
using System.Linq;
using Components.Scripts.Modules.Factory.Api;
using Components.Scripts.Modules.General.Scriptable;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Zenject;

namespace Components.Scripts.Modules.General.Level
{
    [UsedImplicitly]
    public class LevelManager
    {
        [Inject] private readonly Settings _settings;
        [Inject] private readonly ApiManager _apiManager;

        private const int DefaultExperience = 0;
        private const int DefaultLevel = 1;
        
        public Action OnExperienceSet { get; set; }
        public Action OnLevelSet { get; set; }

        public int Experience { get; private set; }
        public int Level { get; private set; }

        public LevelManager()
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
        
        public async void SetExperience(int experience)
        {
            Experience += experience;
            if (Experience >= GetCurrentLevelCap())
            {
                Level++;
                OnLevelSet?.Invoke();
            }

            await SendLevelData();

            OnExperienceSet?.Invoke();
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
        
        private async UniTask SendLevelData()
        {
            var levelObject = new LevelObject
            {
                level = Level,
                experience = Experience
            };
            
            await _apiManager.SetUserExperience(levelObject);
        }
        
        [Serializable]
        public class Settings
        {
            public LevelCapsScriptable data;
        }
    }
}