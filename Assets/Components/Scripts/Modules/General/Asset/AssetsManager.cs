using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace RoboFactory.General.Asset
{
    [UsedImplicitly]
    public class AssetsManager
    {
        //private static readonly HashSet<AssetReference> AssetCache = new();
        private static readonly HashSet<AssetReference> TestCache = new();

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
            //AssetCache.Add(assetRef);
            TestCache.Add(assetRef);
        }
        
        public void ReleaseAllAsset()
        {
            //AssetCache.ToList().ForEach(ReleaseAsset);
            TestCache.ToList().ForEach(ReleaseAsset);
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
            //AssetCache.Remove(assetRef);
            TestCache.Remove(assetRef);
        }
    }
}