using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace RoboFactory.General.Ui
{
    [UsedImplicitly]
    public class UiController : IUiController
    {
        [Inject] private readonly DiContainer _container;
        
        private readonly Dictionary<CanvasType, GameObject> _canvasDictionary;

        public UiController()
        {
            _canvasDictionary = new Dictionary<CanvasType, GameObject>();
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
        }
    }
}
