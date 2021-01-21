using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modules.General.Ui.Popup
{
    public class PopupController : IPopupController
    {
        private readonly Dictionary<PopupType, GameObject> _popupDictionary;

        public PopupController()
        {
            _popupDictionary = new Dictionary<PopupType, GameObject>();
        }

        public void Add(PopupType key, GameObject value)
        {
            _popupDictionary.Add(key, value);
        }

        public void GetAll()
        {
            _popupDictionary.Keys.ToList().ForEach(x => Debug.Log(_popupDictionary[x]));
        }

        public GameObject Find(PopupType key)
        {
            return _popupDictionary.FirstOrDefault(x => x.Key == key).Value;
        }

        public void Remove(PopupType key)
        {
            Object.Destroy(_popupDictionary[key]);
            _popupDictionary.Remove(key);
        }
    }
}