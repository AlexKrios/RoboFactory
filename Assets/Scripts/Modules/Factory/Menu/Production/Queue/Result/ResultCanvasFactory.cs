namespace Modules.Factory.Menu.Production.Queue.Result
{
    public class ResultCanvasFactory/* : IFactory<CraftObject, ResultCanvas>*/
    {
        /*[Inject] private readonly DiContainer _container;

        [Inject] private readonly ResultModel.Factory _resultModelFactory;
        [Inject] private readonly Settings _settings;

        [Inject] private readonly IUiController _uiController;
        [Inject] private readonly IProductStore _producStore;
        [Inject] private readonly IDisable _disable;

        [Inject(Id = "MainCamera")] private readonly Transform _mainCamera;
        [Inject(Id = "MenuCamera")] private readonly Transform _menuCamera;

        [Inject(Id = "UiCanvas")] private readonly RectTransform _uiCanvas;

        public ResultCanvas Create(CraftObject obj)
        {
            _mainCamera.gameObject.SetActive(false);
            _menuCamera.gameObject.SetActive(true);

            _mainCanvas.gameObject.SetActive(false);

            var result = _container.InstantiatePrefabForComponent<ResultCanvas>(_settings.prefab);
            result.name = _settings.name;

            var modelPrefab = _producStore.GetProduct(obj.name).Model;
            var model = _resultModelFactory.Create(modelPrefab);
            model.SetInitTransform(result.transform);

            _disable.Add(DisableType.Camera);

            _uiController.Add(_settings.name, result.gameObject);

            return result;
        }

        [Serializable]
        public class Settings
        {
            public string name;
            public GameObject prefab;

            public float fadeTime;
        }*/
    }
}
