using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Components.Scripts.Modules.General.Asset
{
    [UsedImplicitly]
    public class AssetsManager
    {
        public static async UniTask<T> LoadAsset<T>(AssetReference assetRef) where T : class
        {
            if (assetRef == null)
            {
                Debug.Log("Asset missed or null");
                return null;
            }
            
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

            return operation.Result;
        }
    }
}