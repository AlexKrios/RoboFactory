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
        protected virtual string LoadingTextKey => string.Empty;

        public virtual async UniTask Initialize()
        {
            try
            {
                _sceneService.ProgressText.Value = LoadingTextKey;
                
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
        
        public virtual async UniTask Load()
        {
            try
            {
                await LoadAsync();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
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