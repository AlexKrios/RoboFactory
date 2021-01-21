using UnityEngine;

namespace Modules.General.Ui.Popup
{
    public interface IPopupController
    {
        void Add(PopupType key, GameObject value);
        void GetAll();
        
        GameObject Find(PopupType key);

        void Remove(PopupType key);
    }
}
