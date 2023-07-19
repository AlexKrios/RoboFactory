using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using JetBrains.Annotations;
using Newtonsoft.Json;
using RoboFactory.General.Expedition;
using RoboFactory.General.Item.Production;
using RoboFactory.General.Item.Products;
using RoboFactory.General.Item.Raw;
using RoboFactory.General.Level;
using RoboFactory.General.Money;
using RoboFactory.General.Order;
using RoboFactory.General.Services;
using RoboFactory.General.Unit;
using UserProfile = RoboFactory.General.User.UserProfile;

namespace RoboFactory.General.Api
{
    [UsedImplicitly]
    public class ApiService : Service
    {
        private const string UsersRoot = "users";
        private const string MoneyRoot = "moneySection";
        private const string LevelRoot = "levelSection";
        private const string RawRoot = "storesSection/raw";
        private const string ProductsRoot = "storesSection/products";
        private const string UnitsRoot = "unitsSection/units";
        private const string ProductionQueueRoot = "productionsSection/queue";
        private const string ProductionCountRoot = "productionsSection/count";
        private const string ProductionLevelRoot = "productionsSection/level";
        private const string ExpeditionQueueRoot = "expeditionSection/queue";
        private const string ExpeditionCountRoot = "expeditionSection/count";
        private const string OrdersListRoot = "ordersSection/orders";
        private const string OrdersCountRoot = "ordersSection/count";
        private const string OrdersLevelRoot = "ordersSection/level";

        private static FirebaseDatabase FirebaseDatabase => FirebaseDatabase.DefaultInstance;
        private static string UserId => FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        public ApiService()
        {
            FirebaseDatabase.SetPersistenceEnabled(false);
        }
        
        public async UniTask<UserProfile> GetUserProfile()
        {
            var dataSnapshotTask = FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}")
                .GetValueAsync();
            
            await UniTask.WaitUntil(() => dataSnapshotTask.IsCompleted);
            return dataSnapshotTask.Result.Exists 
                ? JsonConvert.DeserializeObject<UserProfile>(dataSnapshotTask.Result.GetRawJsonValue()) 
                : null;
        }

        public async UniTask SetStartUserProfile(UserProfile profile)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(profile));
        }

        public async UniTask SetUserMoney(MoneyObject moneyData)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{MoneyRoot}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(moneyData));
        }

        public async UniTask SetUserExperience(LevelObject levelData)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{LevelRoot}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(levelData));
        }

        #region Raw

        public async UniTask SetUserRawSingle(string key, RawDto data)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{RawRoot}/{key}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(data));
        }
        
        public async UniTask SetUserRaw(Dictionary<string, RawDto> rawData)
        {
            var tasks = new List<UniTask>();
            foreach (var data in rawData)
            {
                var task = FirebaseDatabase.RootReference
                    .Child($"{UsersRoot}/{UserId}/{RawRoot}/{data.Key}")
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
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ProductsRoot}/{key}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(data));
        }
        
        public async UniTask SetUserProducts(Dictionary<string, ProductDto> productsData)
        {
            var tasks = new List<UniTask>();
            foreach (var data in productsData)
            {
                var task = FirebaseDatabase.RootReference
                    .Child($"{UsersRoot}/{UserId}/{ProductsRoot}/{data.Key}")
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
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{UnitsRoot}/{key}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(data));
        }

        #endregion
        
        #region Production

        public async UniTask SetUserProductionLevel(int value)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ProductionLevelRoot}")
                .SetValueAsync(value);
        }
        
        public async UniTask SetUserProductionQueueCount(int value)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ProductionCountRoot}")
                .SetValueAsync(value);
        }
        
        public async UniTask AddUserProduction(Guid id, ProductionDto data)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ProductionQueueRoot}/{id.ToString()}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(data));
        }
        
        public async UniTask RemoveUserProduction(Guid id)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ProductionQueueRoot}/{id.ToString()}")
                .SetValueAsync(null);
        }

        #endregion

        #region Expedition

        public async UniTask SetUserExpeditionQueueCount(int value)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ExpeditionCountRoot}")
                .SetValueAsync(value);
        }
        
        public async UniTask AddUserExpedition(Guid id, ExpeditionDto data)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ExpeditionQueueRoot}/{id.ToString()}")
                .SetRawJsonValueAsync(JsonConvert.SerializeObject(data));
        }
        
        public async UniTask RemoveUserExpedition(Guid id)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{ExpeditionQueueRoot}/{id.ToString()}")
                .SetValueAsync(null);
        }

        #endregion

        #region Orders

        public async UniTask SetUserOrders(Dictionary<string, OrderDto> ordersData)
        {
            var tasks = new List<UniTask>();
            foreach (var data in ordersData)
            {
                var task = FirebaseDatabase.RootReference
                    .Child($"{UsersRoot}/{UserId}/{OrdersListRoot}/{data.Key}")
                    .SetRawJsonValueAsync(JsonConvert.SerializeObject(data.Value))
                    .AsUniTask();
                
                tasks.Add(task);
            }

            await UniTask.WhenAll(tasks);
        }
        
        public async UniTask SetUserOrdersCount(int value)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{OrdersCountRoot}")
                .SetValueAsync(value);
        }
        
        public async UniTask SetUserOrdersLevel(int value)
        {
            await FirebaseDatabase.RootReference
                .Child($"{UsersRoot}/{UserId}/{OrdersLevelRoot}")
                .SetValueAsync(value);
        }

        #endregion
    }
}
