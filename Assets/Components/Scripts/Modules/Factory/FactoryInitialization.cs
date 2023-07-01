using RoboFactory.Factory.Cameras;
using RoboFactory.General.Audio;
using RoboFactory.General.Ui;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory
{
    [AddComponentMenu("Scripts/Factory/Factory Initialization", 0)]
    public class FactoryInitialization : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly AudioManager _audioController;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly FactoryCameraController _factoryCameraController;

        #endregion

        #region Components

        [Header("Canvas")]
        [SerializeField] private GameObject adminCanvas;
        [SerializeField] private GameObject hudCanvas;
        [SerializeField] private GameObject uiCanvas;

        #endregion
        
        private void Awake()
        {
            _uiController.AddCanvas(CanvasType.Admin, adminCanvas);
            _uiController.AddCanvas(CanvasType.HUD, hudCanvas);
            _uiController.AddCanvas(CanvasType.Ui, uiCanvas);
        }

        private void Start()
        {
            /*FirebaseDatabase.DefaultInstance.GetReference("counter")
                .GetValueAsync().ContinueWith(task =>
                {
                    Debug.LogWarning(task.Result.Value);
                });*/
            
            _factoryCameraController.Init();
            _audioController.PlayMusic();
            /*UnityWebRequest
                .Get("http://robo-factory.bubeha.com/api/units")
                .SendWebRequest()
                .AsAsyncOperationObservable()
                .Subscribe(result =>
                {
                    Debug.LogWarning(result.webRequest.downloadHandler.text);
                });*/
        }

        private void OnDestroy()
        {
            _uiController.ClearCanvas();
        }
    }
}