using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Modules.General.Asset
{
    public class AssetsController
    {
        public static async Task<T> LoadAsset<T>(AssetReference assetRef, bool release = false) where T : class
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

            var result = operation.Result;
            
            if (release)
                Addressables.Release(operation);

            return result;
        }
    }
}