using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace Modules.General.Scene
{
    [UsedImplicitly]
    public class SceneController
    {
        public float TimeLoad { get; set; }
        public SceneName SceneLoad { get; set; }

        public void LoadScene()
        {
            SceneManager.LoadScene(SceneLoad.ToString());
        }
        
        public void LoadScene(SceneName name, float time = 0f)
        {
            SceneLoad = name;
            TimeLoad = time;
            SceneManager.LoadScene(SceneName.Loader.ToString());
        }
    }
}