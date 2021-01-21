using UnityEngine;

namespace Modules.Factory.Menu.Production.Queue.Result
{
    [AddComponentMenu("Scripts/Factory/Menu/Production/Queue/Result Button")]
    public class ResultButton : MonoBehaviour
    {
        /*[Inject] private readonly ResultCanvasFactory.Settings _resultCanvasSettings;

        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly IDisable _disable;

        [Inject(Id = "MainCamera")] private readonly Transform _mainCamera;
        [Inject(Id = "MenuCamera")] private readonly Transform _menuCamera;

        [Inject(Id = "UiCanvas")] private readonly RectTransform _uiCanvas;

        public void Click()
        {
            _mainCamera.gameObject.SetActive(true);
            _menuCamera.gameObject.SetActive(false);

            _mainCanvas.gameObject.SetActive(true);

            var result = _uiController.Find(_resultCanvasSettings.name);
            _uiController.Remove(result);

            _disable.Remove(DisableType.Camera);
        }*/
    }
}
