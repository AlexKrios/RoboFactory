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
        protected virtual string LoadingTextKey => string.Empty;

        public async UniTask Initialize()
        {
            try
            {
                _sceneService.ProgressText.Value = LoadingTextKey;
                
                SetState(ServiceState.Initializing);
                await InitializeAsync();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                SetState(ServiceState.Failed);
            }
        }
        
        public async UniTask Load()
        {
            try
            {
                await LoadAsync();
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

        protected virtual UniTask LoadAsync()
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