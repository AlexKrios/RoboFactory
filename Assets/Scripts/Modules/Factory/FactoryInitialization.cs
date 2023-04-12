using Modules.Factory.Cameras;
using Modules.General.Audio;
using Modules.General.Ui;
using UnityEngine;
using Zenject;
using CameraType = Modules.General.Ui.CameraType;

namespace Modules.Factory
{
    [AddComponentMenu("Scripts/Factory/Factory Initialization", 0)]
    public class FactoryInitialization : MonoBehaviour
    {
        #region Zenject

        [Inject] private readonly IAudioController _audioController;
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
            _factoryCameraController.Init();
            _audioController.PlayMusic();
            
            /*var request = UnityWebRequest.Get("http://api.localhost/api/products");
            //request.SetRequestHeader("Content-Type", "application/json");
            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            var result = request.downloadHandler.text;
            Debug.LogWarning(result);*/
        }

        private void OnDestroy()
        {
            _uiController.ClearCamera();
            _uiController.ClearCanvas();
        }
    }
}