using Cysharp.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace Components.Scripts.Modules.General.User
{
    [UsedImplicitly]
    public class UserManager
    {
        private const string BaseURL = "http://robo-factory.bubeha.com/api";
        
        private const string UsersRoot = "users";
        //private const string SettingsRoot = "settingsSection";

        private static FirebaseDatabase FirebaseDatabase => FirebaseDatabase.DefaultInstance;
        private static FirebaseUser FirebaseUser => FirebaseAuth.DefaultInstance.CurrentUser;
        
        public async UniTask<UserProfile> GetUserData()
        {
            var dataSnapshotTask = FirebaseDatabase.RootReference
                .Child(UsersRoot)
                .Child(FirebaseUser.UserId)
                .GetValueAsync();
            
            await UniTask.WaitUntil(() => dataSnapshotTask.IsCompleted);

            return JsonConvert.DeserializeObject<UserProfile>(dataSnapshotTask.Result.GetRawJsonValue());
        }
        
        public async UniTask<string> LoadData() 
        {
            const string requestUrl = BaseURL + "/units";
            var request = UnityWebRequest.Get(requestUrl);

            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError ||
                !request.isDone
            )
                return null;
                
            return request.downloadHandler.text;
        }
    }
}
