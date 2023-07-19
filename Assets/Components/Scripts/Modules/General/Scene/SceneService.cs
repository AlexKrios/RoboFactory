using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Services;
using UniRx;
using UnityEngine.SceneManagement;

namespace RoboFactory.General.Scene
{
    [UsedImplicitly]
    public class SceneService : Service
    {
        protected override string LoadingTextKey => "initialize_scenes";

        public StringReactiveProperty ProgressText { get; }
        public FloatReactiveProperty ProgressValue { get; }
        public ReactiveProperty<SceneLoadState> LoadState { get; }

        public float TimeLoad { get; set; }
        public SceneName SceneLoad { get; set; }

        public SceneService()
        {
            ProgressText = new StringReactiveProperty();
            ProgressValue = new FloatReactiveProperty();

            ProgressText.Value = string.Empty;
        }
        
        public async void LoadScene(SceneName scene)
        {
            SceneManager.LoadScene(SceneName.Loader.ToString(), LoadSceneMode.Additive);
            LoadState.Value = SceneLoadState.Loading;
            SceneLoad = scene;

            await UniTask.WaitUntil(() => LoadState.Value == SceneLoadState.Finish);
            
            SceneManager.LoadScene(SceneLoad.ToString());
        }
        
        public void LoadScene(SceneName name, float time = 0f)
        {
            SceneLoad = name;
            TimeLoad = time;
            SceneManager.LoadScene(SceneLoad.ToString());
        }
    }
}