using RoboFactory.General.Scene;
using RoboFactory.General.Services;
using UnityEngine;
using Zenject;

namespace RoboFactory.General
{
    public class LauncherMediator : MonoBehaviour
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly SceneService _sceneService;

        private async void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            
            var services = _container.ResolveAll<Service>()
                .FindAll(x => x.ServiceType != ServiceTypeEnum.NeedAuth);
            foreach (var service in services)
            {
                await service.Initialize();
                _sceneService.ProgressMainValue.Value++;
            }
            
            await _sceneService.LoadScene(SceneName.Auth);
        }
    }
}