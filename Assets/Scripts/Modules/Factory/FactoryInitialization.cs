using Modules.Factory.Building;
using Modules.Factory.Cameras;
using Modules.General.Audio;
using Modules.General.Ui;
using UnityEngine;
using Zenject;

namespace Modules.Factory
{
    [AddComponentMenu("Scripts/Factory/Factory Initialization", 0)]
    public class FactoryInitialization : MonoBehaviour
    {
        [Inject] private readonly IAudioController _audioController;
        [Inject] private readonly IUiController _uiController;

        [Inject] private readonly IFactoryBuildingController _factoryBuildingController;
        [Inject] private readonly CameraController _cameraController;

        [Inject(Id = "UiCanvas")] private RectTransform _uiCanvas;
        [Inject(Id = "MenuCanvas")] private RectTransform _menuCanvas;

        private void Awake()
        {
            _uiController.AddCanvas(CanvasType.HUD, _uiCanvas.gameObject);
            _uiController.AddCanvas(CanvasType.Menu, _menuCanvas.gameObject);
        }

        private void Start()
        {
            _factoryBuildingController.Init();
            _cameraController.Init();

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
            _uiController.ClearCanvas();
        }
    }
}