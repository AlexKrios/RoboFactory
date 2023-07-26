using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Api;
using RoboFactory.General.Profile;
using RoboFactory.General.Scriptable;
using RoboFactory.General.Services;
using UnityEngine;
using Zenject;

namespace RoboFactory.General.Level
{
    [UsedImplicitly]
    public class ExperienceService : Service
    {
        protected override string InitializeTextKey => "initialize_2";
        public override ServiceTypeEnum ServiceType => ServiceTypeEnum.NeedAuth;
        
        [Inject] private readonly Settings _settings;
        [Inject] private readonly CommonProfile _commonProfile;
        [Inject] private readonly ApiService _apiService;

        private const int DefaultExperience = 0;
        private const int DefaultLevel = 1;
        
        public Action OnExperienceSet { get; set; }
        public Action OnLevelSet { get; set; }

        public int Experience { get; private set; }
        public int Level { get; private set; }

        public ExperienceService()
        {
            Experience = DefaultExperience;
            Level = DefaultLevel;
        }
        
        protected override UniTask InitializeAsync()
        {
            var data = _commonProfile.UserProfile.LevelSection;

            Experience = data.Experience;
            Level = GetCurrentLevel();

            OnExperienceSet?.Invoke();
            OnLevelSet?.Invoke();
            
            return UniTask.CompletedTask;
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
            return _settings.Data.Caps.First(x => x.Experience > Experience).Level;
        }
        
        public int GetPreviousLevelCap()
        {
            return Level == 1 ? 0 : _settings.Data.Caps.First(x => x.Level == Level - 1).Experience;
        }

        public int GetCurrentLevelCap()
        {
            return _settings.Data.Caps.First(x => x.Level == Level).Experience;
        }
        
        private async UniTask SendLevelData()
        {
            var levelObject = new LevelObject
            {
                Level = Level,
                Experience = Experience
            };
            
            await _apiService.SetUserExperience(levelObject);
        }
        
        [Serializable]
        public class Settings
        {
            [SerializeField] private LevelCapsScriptable _data;
            
            public LevelCapsScriptable Data => _data;
        }
    }
}