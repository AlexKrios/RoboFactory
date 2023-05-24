using UnityEngine;

namespace RoboFactory.General.Ui
{
    public interface IUiController
    {
        void AddCamera(CameraType type, GameObject camera);
        GameObject GetCamera(CameraType type);
        void SetCameraActive(CameraType type, bool value = true);
        void ClearCamera();
        
        void AddCanvas(CanvasType type, GameObject canvas);
        GameObject GetCanvas(CanvasType type);
        void SetCanvasActive(CanvasType type, bool value = true);
        void ClearCanvas();

        void AddUi<T>(T element) where T : class;
        T FindUi<T>();
        void RemoveUi<T>(T element, GameObject gameObject, float timeout = 0f);
    }
}