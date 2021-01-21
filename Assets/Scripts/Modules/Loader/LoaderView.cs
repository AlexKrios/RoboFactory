using DG.Tweening;
using Modules.General.Scene;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Modules.Loader
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