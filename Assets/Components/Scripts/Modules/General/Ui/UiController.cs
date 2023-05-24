using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace RoboFactory.General.Ui
{
    [UsedImplicitly]
    public class UiController : IUiController
    {
        [Inject] private readonly DiContainer _container;
        
        private readonly Dictionary<CameraType, GameObject> _cameraDictionary;
        private readonly Dictionary<CanvasType, GameObject> _canvasDictionary;

        public UiController()
        {
            _cameraDictionary = new Dictionary<CameraType, GameObject>();
            _canvasDictionary = new Dictionary<CanvasType, GameObject>();
        }

        public void AddCamera(CameraType type, GameObject camera)
        {
            if (_cameraDictionary.ContainsKey(type))
                _cameraDictionary[type] = camera;
            else
                _cameraDictionary.Add(type, camera);
        }
        
        public GameObject GetCamera(CameraType type)
        {
            return _cameraDictionary[type];
        }

        public void SetCameraActive(CameraType type, bool value = true)
        {
            _cameraDictionary[type].SetActive(value);
        }
        
        public void ClearCamera()
        {
            if (_cameraDictionary.Count != 0)
                _cameraDictionary.Clear();
        }

        public void AddCanvas(CanvasType type, GameObject canvas)
        {
            if (_canvasDictionary.ContainsKey(type))
                _canvasDictionary[type] = canvas;
            else
                _canvasDictionary.Add(type, canvas);
        }

        public GameObject GetCanvas(CanvasType type)
        {
            return _canvasDictionary[type];
        }

        public void SetCanvasActive(CanvasType type, bool value = true)
        {
            _canvasDictionary[type].SetActive(value);
        }

        public void ClearCanvas()
        {
            if (_canvasDictionary.Count != 0)
                _canvasDictionary.Clear();
        }
        
        public void AddUi<T>(T element) where T : class
        {
            if (_container.TryResolve<T>() != null)
                _container.Unbind<T>();
            
            _container.BindInstance(element);
        }
        
        public T FindUi<T>()
        {
            return _container.Resolve<T>();
        }

        public void RemoveUi<T>(T element, GameObject gameObject, float timeout = 0f)
        {
            _container.Unbind<T>();
            Object.Destroy(gameObject, timeout);
            Addressables.ReleaseInstance(gameObject);
        }
    }
}
