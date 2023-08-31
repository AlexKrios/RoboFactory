using RoboFactory.Factory.Menu.Production;
using RoboFactory.General.Scene;
using RoboFactory.General.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace RoboFactory.Auth
{
    public class AuthMediator : MonoBehaviour
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly SceneService _sceneService;
        [Inject] private readonly AuthFactory _authFactory;
        [Inject] private readonly AuthService _authService;
        
        private readonly CompositeDisposable _disposable = new();
        
        private GameObject _loaderScreen;
        private GameObject _currentAuthForm;

        private async void Awake()
        {
            _loaderScreen = _sceneService.InstantiateLoader();
            _authService.AuthStatus.Subscribe(AuthFormHandle).AddTo(_disposable);

            var services = _container.ResolveAll<Service>()
                .FindAll(x => x.ServiceType != ServiceTypeEnum.NotNeedAuth);
            foreach (var service in services)
            {
                await service.Initialize();
                _sceneService.ProgressMainValue.Value++;
            }
            
            await _sceneService.LoadScene(SceneName.Factory);
            Destroy(_loaderScreen);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        private void AuthFormHandle(AuthStatusEnum status)
        {
            switch (status)
            {
                case AuthStatusEnum.Failure:
                    _currentAuthForm = _authFactory.CreateSignInForm().gameObject;
                    break;
                
                case AuthStatusEnum.Success:
                    Destroy(_currentAuthForm);
                    break;
            }
        }
    }
}