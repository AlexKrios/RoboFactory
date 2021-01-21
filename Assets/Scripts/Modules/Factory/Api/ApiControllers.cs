using System;
using System.Collections;
using UnityEngine.Networking;

namespace Modules.Factory.Api
{
    public class ApiControllers : IApiControllers
    {
        private const string BaseURL = "http://localhost:8080/api";

        public IEnumerator LoadData(Action<string> finishDelegate) 
        {
            const string requestUrl = BaseURL + "/admin/users";
            var request = UnityWebRequest.Get(requestUrl);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.ConnectionError &&
                request.result != UnityWebRequest.Result.ProtocolError &&
                request.isDone
            )
                yield return request.downloadHandler.text;

            finishDelegate(request.downloadHandler.text);
        }
    }
}
