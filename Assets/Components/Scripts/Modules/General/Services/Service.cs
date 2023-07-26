using System;
using Cysharp.Threading.Tasks;
using RoboFactory.General.Scene;
using UnityEngine;
using Zenject;

namespace RoboFactory.General.Services
{
    [Service]
    public class Service
    {
        [Inject] private readonly SceneService _sceneService;
        
        public ServiceState State { get; private set; } = ServiceState.Disabled;
        
        protected virtual string InitializeTextKey => string.Empty;
        public virtual ServiceTypeEnum ServiceType => ServiceTypeEnum.NotNeedAuth;

        public async UniTask Initialize()
        {
            try
            {
                _sceneService.ProgressText.Value = InitializeTextKey;
                
                SetState(ServiceState.Initializing);
                await InitializeAsync();
                SetState(ServiceState.Ready);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                SetState(ServiceState.Failed);
            }
        }

        protected virtual UniTask InitializeAsync()
        {
            return UniTask.CompletedTask;
        }

        private void SetState(ServiceState state)
        {
            if (state != State)
            {
                State = state;
            }
        }
    }
}