using Components.Scripts.Modules.General.Scene;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Components.Scripts.Modules.Loader
{ 
    [AddComponentMenu("Scripts/Loader", 0)]
    public class LoaderView : MonoBehaviour
    {
        [Inject] private readonly SceneController _sceneController;
        
        [SerializeField] private Image loadingBar;

        private void Awake()
        {
            loadingBar.fillAmount = 0f;
            loadingBar.DOFillAmount(1f, _sceneController.TimeLoad)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => _sceneController.LoadScene());
        }
    }
}