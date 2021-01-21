using UnityEngine;

namespace Modules.General.Ui
{
    public interface IUiController
    {
        void AddCanvas(CanvasType type, GameObject canvas);
        GameObject FindCanvas(CanvasType type);
        void ClearCanvas();
        
        void AddUi(UiType type, GameObject canvas);
        GameObject FindUi(UiType key);
        T FindUi<T>(UiType key);
        void RemoveUi(UiType key, float timeout = 0f);
    }
}