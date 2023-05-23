using System;
using System.Collections.Generic;
using Components.Scripts.Modules.General.Expedition;
using Components.Scripts.Modules.General.Item.Production.Object;
using Components.Scripts.Modules.General.Item.Products.Object.Types;
using Components.Scripts.Modules.General.Item.Raw.Object;
using Components.Scripts.Modules.General.Level;
using Components.Scripts.Modules.General.Money;
using Components.Scripts.Modules.General.Order.Object;
using Components.Scripts.Modules.General.Unit.Object;
using Cysharp.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using UserProfile = Components.Scripts.Modules.General.User.UserProfile;

namespace Components.Scripts.Modules.Factory.Api
{
    [UsedImplicitly]
    public class ApiManager
    {
        private const string UsersRoot = "users";
        private static readonly string UserRoot = $"{UsersRoot}/{FirebaseUser.UserId}";
        private static readonly string MoneyRoot = $"{UserRoot}/moneySection";
        private static readonly string LevelRoot = $"{UserRoot}/levelSection";
        private static readonly string RawRoot = $"{UserRoot}/storesSection/raw";
        private static readonly string ProductsRoot = $"{UserRoot}/storesSection/products";
        private static readonly string UnitsRoot = $"{UserRoot}/unitsSection/units";
        private static readonly string ProductionsRoot = $"{UserRoot}/productionsSection";
        private static readonly string ProductionQueueRoot = $"{ProductionsRoot}/queue";
        private static readonly string ProductionCountRoot = $"{ProductionsRoot}/count";
        private static readonly string ProductionLevelRoot = $"{ProductionsRoot}/level";
        private static readonly string ExpeditionRoot = $"{UserRoot}/expeditionSection";
        private static readonly string ExpeditionQueueRoot = $"{ExpeditionRoot}/queue";
        private static readonly string ExpeditionCountRoot = $"{ExpeditionRoot}/count";
        private static readonly string OrdersRoot = $"{UserRoot}/ordersSection";
        private static readonly string OrdersListRoot = $"{OrdersRoot}/orders";
        private static readonly string OrdersCountRoot = $"{OrdersRoot}/count";
        private static readonly string OrdersLevelRoot = $"{OrdersRoot}/level";

        private static FirebaseDatabase FirebaseDatabase => FirebaseDatabase.DefaultInstance;
        private static FirebaseUser FirebaseUser => FirebaseAuth.DefaultInstance.CurrentUser;

        public ApiManager()
        {
            FirebaseDatabase.SetPersistenceEnabled(false);
        }
        
        public async UniTask<UserProfile> GetUserProfile()
        {
            var dataSnapshotTask = FirebaseDatabase.RootReference.Child(UserRoot).GetValueAsync();
            await UniTask.WaitUntil(() => dataSnapshotTask.IsCompleted);
            Debug.LogWarning(dataSnapshotTask.Result.GetRawJsonValue());
            return dataSnapshotTask.Result.Exists 
                ? JsonConvert.DeserializeObject<UserProfile>(dataSnapshotTask.Result.GetRawJsonValue()) 
                : null;
        }

        public async UniTask SetStartUserProfile(UserProfile profile)
        {
            await FirebaseDatabase.DefaultInstance.RootReference
                .Child(UserRoot)
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(profile));
        }

        public async UniTask SetUserMoney(MoneyObject moneyData)
        {
            await FirebaseDatabase.DefaultInstance.RootReference
                .Child(MoneyRoot)
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(moneyData));
        }

        public async UniTask SetUserExperience(LevelObject levelData)
        {
            await FirebaseDatabase.DefaultInstance.RootReference
                .Child(LevelRoot)
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(levelData));
        }

        #region Raw

        public async UniTask SetUserRawSingle(string key, RawDto data)
        {
            await FirebaseDatabase.DefaultInstance.RootReference
                .Child(RawRoot)
                .Child(key)
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(data));
        }
        
        public async UniTask SetUserRaw(Dictionary<string, RawDto> rawData)
        {
            var tasks = new List<UniTask>();
            foreach (var data in rawData)
            {
                var task = FirebaseDatabase.DefaultInstance.RootReference
                    .Child(RawRoot)
                    .Child(data.Key)
                    .SetRawJsonValueAsync(JsonConvert.SerializeObject(data.Value))
                    .AsUniTask();
                
                tasks.Add(task);
            }

            await UniTask.WhenAll(tasks);
        }

        #endregion

        #region Product

        public async UniTask SetUserProductSingle(string key, ProductDto data)
        {
            await FirebaseDatabase.DefaultInstance.RootReference
                .Child(ProductsRoot)
                .Child(key)
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(data));
        }
        
        public async UniTask SetUserProducts(Dictionary<string, ProductDto> productsData)
        {
            var tasks = new List<UniTask>();
            foreach (var data in productsData)
            {
                var task = FirebaseDatabase.DefaultInstance.RootReference
                    .Child(ProductsRoot)
                    .Child(data.Key)
                    .SetRawJsonValueAsync(JsonConvert.SerializeObject(data.Value))
                    .AsUniTask();
                
                tasks.Add(task);
            }

            await UniTask.WhenAll(tasks);
        }

        #endregion

        #region Units

        public async UniTask SetUserUnitSingle(string key, UnitDto data)
        {
            await FirebaseDatabase.DefaultInstance.RootReference
                .Child(UnitsRoot)
                .Child(key)
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(data));
        }

        #endregion
        
        #region Production

        public async UniTask SetUserProductionLevel(int value)
        {
            await FirebaseDatabase.DefaultInstance.RootReference
                .Child($"{ProductionLevelRoot}")
                .SetValueAsync(value);
        }
        
        public async UniTask SetUserProductionQueueCount(int value)
        {
            await FirebaseDatabase.DefaultInstance.RootReference
                .Child($"{ProductionCountRoot}")
                .SetValueAsync(value);
        }
        
        public async UniTask AddUserProduction(Guid id, ProductionDto data)
        {
            await FirebaseDatabase.DefaultInstance.RootReference
                .Child(ProductionQueueRoot)
                .Child(id.ToString())
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(data));
        }
        
        public async UniTask RemoveUserProduction(Guid id)
        {
            await FirebaseDatabase.DefaultInstance.RootReference
                .Child(ProductionQueueRoot)
                .Child(id.ToString())
                .SetValueAsync(null);
        }

        #endregion

        #region Expedition

        public async UniTask SetUserExpeditionQueueCount(int value)
        {
            await FirebaseDatabase.DefaultInstance.RootReference
                .Child($"{ExpeditionCountRoot}")
                .SetValueAsync(value);
        }
        
        public async UniTask AddUserExpedition(Guid id, ExpeditionDto data)
        {
            await FirebaseDatabase.DefaultInstance.RootReference
                .Child(ExpeditionQueueRoot)
                .Child(id.ToString())
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(data));
        }
        
        public async UniTask RemoveUserExpedition(Guid id)
        {
            await FirebaseDatabase.DefaultInstance.RootReference
                .Child(ExpeditionQueueRoot)
                .Child(id.ToString())
                .SetValueAsync(null);
        }

        #endregion

        #region Orders

        public async UniTask SetUserOrders(Dictionary<string, OrderDto> ordersData)
        {
            var tasks = new List<UniTask>();
            foreach (var data in ordersData)
            {
                var task = FirebaseDatabase.DefaultInstance.RootReference
                    .Child(OrdersListRoot)
                    .Child(data.Key)
                    .SetRawJsonValueAsync(JsonConvert.SerializeObject(data.Value))
                    .AsUniTask();
                
                tasks.Add(task);
            }

            await UniTask.WhenAll(tasks);
        }
        
        public async UniTask SetUserOrdersCount(int value)
        {
            await FirebaseDatabase.DefaultInstance.RootReference
                .Child($"{OrdersCountRoot}")
                .SetValueAsync(value);
        }
        
        public async UniTask SetUserOrdersLevel(int value)
        {
            await FirebaseDatabase.DefaultInstance.RootReference
                .Child($"{OrdersLevelRoot}")
                .SetValueAsync(value);
        }

        #endregion
    }
}
