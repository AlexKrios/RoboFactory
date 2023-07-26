using RoboFactory.Factory.Cameras;
using RoboFactory.General.Audio;
using RoboFactory.General.Ui;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory
{
    public class FactoryInitialization : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly AudioService _audioService;
        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly FactoryCameraController _factoryCameraController;

        #endregion

        #region Components

        [Header("Canvas")]
        [SerializeField] private GameObject _adminCanvas;
        [SerializeField] private GameObject _hudCanvas;
        [SerializeField] private GameObject _uiCanvas;

        #endregion
        
        private void Awake()
        {
            _uiController.AddCanvas(CanvasType.Admin, _adminCanvas);
            _uiController.AddCanvas(CanvasType.HUD, _hudCanvas);
            _uiController.AddCanvas(CanvasType.Ui, _uiCanvas);
        }

        private void Start()
        {
            /*FirebaseDatabase.DefaultInstance.GetReference("counter")
                .GetValueAsync().ContinueWith(task =>
                {
                    Debug.LogWarning(task.Result.Value);
                });*/
            
            _factoryCameraController.Init();
            _audioService.PlayMusic();
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