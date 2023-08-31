using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Services;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace RoboFactory.General.Scene
{
    [UsedImplicitly]
    public class SceneService : Service
    {
        private const string LoadSceneKey = "load_scene";

        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject(Id = Constants.ScreensParentKey)] private readonly Transform _screensParent;
        
        protected override string InitializeTextKey => "initialize_scenes";

        public StringReactiveProperty ProgressText { get; } = new(string.Empty);
        public FloatReactiveProperty ProgressMainValue { get; } = new();
        public FloatReactiveProperty ProgressSecondaryValue { get; } = new();

        public GameObject InstantiateLoader()
        {
            return _container.InstantiatePrefab(_settings.LoaderPrefab, _screensParent);
        }
        
        public async UniTask LoadScene(SceneName scene)
        {
            if (SceneManager.GetActiveScene().name == scene.ToString()) return;
            
            ProgressText.Value = LoadSceneKey;
            await UniTask.Delay(1000);
            await SceneManager.LoadSceneAsync(scene.ToString()).ToUniTask();
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private GameObject _loaderPrefab;
            
            public GameObject LoaderPrefab => _loaderPrefab;
        }
    }
}