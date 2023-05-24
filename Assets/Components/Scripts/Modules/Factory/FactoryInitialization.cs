using RoboFactory.Factory.Cameras;
using RoboFactory.General.Audio;
using RoboFactory.General.Ui;
using UnityEngine;
using Zenject;
using CameraType = RoboFactory.General.Ui.CameraType;

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

        [Header("Camera")]
        [SerializeField] private GameObject mainCamera;
        [SerializeField] private GameObject uiCamera;
        
        [Header("Canvas")]
        [SerializeField] private GameObject adminCanvas;
        [SerializeField] private GameObject hudCanvas;
        [SerializeField] private GameObject uiCanvas;

        #endregion
        
        private void Awake()
        {
            _uiController.AddCamera(CameraType.Main, mainCamera);
            _uiController.AddCamera(CameraType.Ui, uiCamera);
            
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
            _uiController.ClearCamera();
            _uiController.ClearCanvas();
        }
    }
}