using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Services;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Object = UnityEngine.Object;

namespace RoboFactory.General.Scene
{
    [UsedImplicitly]
    public class SceneService : Service
    {
        protected override string LoadingTextKey => "initialize_scenes";
        
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly Settings _settings;
        [Inject(Id = Constants.ScreenParentKey)] private readonly Transform _sceneParent;

        public StringReactiveProperty ProgressText { get; } = new(string.Empty);
        public FloatReactiveProperty ProgressMainValue { get; } = new();
        public FloatReactiveProperty ProgressSecondaryValue { get; } = new();
        public ReactiveProperty<SceneLoadState> LoadState { get; } = new();

        private GameObject _loaderScreen;

        public async void LoadScene(SceneName scene)
        {
            _loaderScreen = _container.InstantiatePrefab(_settings.LoaderPrefab, _sceneParent);
            LoadState.Value = SceneLoadState.Loading;

            await UniTask.WaitUntil(() => LoadState.Value == SceneLoadState.Finish);
            await SceneManager.LoadSceneAsync(scene.ToString());
            
            Object.Destroy(_loaderScreen);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] private GameObject _loaderPrefab;
            
            public GameObject LoaderPrefab => _loaderPrefab;
        }
    }
}