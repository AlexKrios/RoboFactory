using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RoboFactory.General.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace RoboFactory.General.Asset
{
    [UsedImplicitly]
    public class AddressableService : Service
    {
        private enum DownloadResult
        {
            Success,
            NoInternetError,
            UnknownError
        }
        
        protected override string LoadingTextKey => "initialize_addressables";
        
        private const string CoreLabel = "Core";
        private const string MusicLabel = "Music";
        private const string AudioLabel = "Audio";

        private static readonly HashSet<AssetReference> AssetCache = new();

        protected override async UniTask InitializeAsync()
        {
            await Addressables.InitializeAsync();

            await TryDownloadAsync(MusicLabel);
            await TryDownloadAsync(AudioLabel);
            await TryDownloadAsync(CoreLabel);

            await Addressables.LoadAssetsAsync<GameObject>(CoreLabel, null);
            await Addressables.LoadAssetsAsync<Sprite>(CoreLabel, null);
            
            await Addressables.LoadAssetsAsync<AudioClip>(MusicLabel, null);
            await Addressables.LoadAssetsAsync<AudioClip>(AudioLabel, null);
        }
        
        private async UniTask<DownloadResult> TryDownloadAsync(object key)
        {
            var cached = await IsCachedLocally(key);
            if (cached) return DownloadResult.Success;

            var downloadOperation = Addressables.DownloadDependenciesAsync(key);
            return await InternalDownloadAsync(downloadOperation);
        }
        
        private async UniTask<bool> IsCachedLocally(object assetKey)
        {
            long size = -1;
            var downloadSizeOperation = Addressables.GetDownloadSizeAsync(assetKey);
            await downloadSizeOperation.Task;

            if (downloadSizeOperation.Status is AsyncOperationStatus.Succeeded)
            {
                size = downloadSizeOperation.Result;
            }

            return size == 0;
        }

        private async UniTask<DownloadResult> InternalDownloadAsync(AsyncOperationHandle downloadOperation)
        {
            var progressValue = 0f;
            while (downloadOperation.Status == AsyncOperationStatus.None)
            {
                var percentageComplete = downloadOperation.GetDownloadStatus().Percent;
                if (percentageComplete > progressValue)
                {
                    progressValue = percentageComplete;
                }

                await UniTask.Yield();
            }
            
            return DownloadResult.Success;
        }

        public async UniTask<GameObject> InstantiateAssetAsync(AssetReference assetRef, Transform parent)
        {
            var assetOriginal = await LoadAssetAsync<GameObject>(assetRef);
            var asset = Object.Instantiate(assetOriginal, parent);

            return asset;
        }
        
        public async UniTask<T> LoadAssetAsync<T>(AssetReference assetRef) where T : class
        {
            if (assetRef.Asset)
                return assetRef.Asset as T;
            
            if (!assetRef.OperationHandle.IsDone)
            {
                await assetRef.OperationHandle.Task;
                return assetRef.OperationHandle.Result as T;
            }
            
            var operation = assetRef.LoadAssetAsync<T>();
            await operation.Task;
            
            if (operation.Status == AsyncOperationStatus.Failed)
                throw new Exception();
            
            AddHandleToCache(assetRef);

            return operation.Result;
        }
        
        private void AddHandleToCache(AssetReference assetRef)
        {
            AssetCache.Add(assetRef);
        }
        
        public void ReleaseAllAsset()
        {
            AssetCache.ToList().ForEach(ReleaseAsset);
        }

        public void ReleaseAsset(AssetReference assetRef)
        {
            ReleaseAddressableAsset(assetRef);
            ReleaseFromCache(assetRef);
        }
        
        private void ReleaseAddressableAsset(AssetReference assetRef)
        {
            Addressables.Release(assetRef.OperationHandle);
        }

        private void ReleaseFromCache(AssetReference assetRef)
        {
            AssetCache.Remove(assetRef);
        }
    }
}