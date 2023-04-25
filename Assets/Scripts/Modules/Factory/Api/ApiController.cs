using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.Networking;

namespace Modules.Factory.Api
{
    [UsedImplicitly]
    public class ApiController : IApiController
    {
        private const string BaseURL = "http://robo-factory.bubeha.com/api";

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
