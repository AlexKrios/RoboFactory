using System.Collections.Generic;
using UnityEngine;

namespace Modules.General.Ui
{
    public class UiController : IUiController
    {
        private readonly Dictionary<CanvasType, GameObject> _canvasDictionary;
        private readonly Dictionary<UiType, GameObject> _uiDictionary;

        public UiController()
        {
            _canvasDictionary = new Dictionary<CanvasType, GameObject>();
            _uiDictionary = new Dictionary<UiType, GameObject>();
        }

        public void AddCanvas(CanvasType type, GameObject canvas)
        {
            if (_canvasDictionary.ContainsKey(type))
                _canvasDictionary[type] = canvas;
            else
                _canvasDictionary.Add(type, canvas);
        }

        public GameObject FindCanvas(CanvasType type)
        {
            return _canvasDictionary[type];
        }

        public void ClearCanvas()
        {
            _canvasDictionary.Clear();
        }
        
        public void AddUi(UiType type, GameObject canvas)
        {
            if (_uiDictionary.Count == 0)
                _canvasDictionary[CanvasType.HUD].SetActive(false);
            
            _uiDictionary.Add(type, canvas);
        }

        public GameObject FindUi(UiType key)
        {
            return _uiDictionary[key];
        }
        public T FindUi<T>(UiType key)
        {
            return _uiDictionary[key].GetComponent<T>();
        }

        public void RemoveUi(UiType key, float timeout = 0f)
        {
            Object.Destroy(_uiDictionary[key], timeout);
            _uiDictionary.Remove(key);

            if (_uiDictionary.Count == 0)
                _canvasDictionary[CanvasType.HUD].SetActive(true);
        }
    }
}
