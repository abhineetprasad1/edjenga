using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;


public class RemoteDataService : IService
{
    public void OnInit()
    {

    }

    public void OnDestroy()
    {

    }

    public async Task<List<T>> GetRemoteData<T>(string url)
    {
        var tcs = new TaskCompletionSource<List<T>>();

        var webRequest = UnityWebRequest.Get(url);
        var asyncOperation = webRequest.SendWebRequest();
       

        asyncOperation.completed += (ao) =>
        {
            try
            {
                if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                    webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError($"Error: {webRequest.error}");
                    tcs.SetException(new Exception(webRequest.error));
                    return;
                }

                string json = webRequest.downloadHandler.text;
                List<T> dataList = JsonConvert.DeserializeObject<List<T>>(json);

                tcs.SetResult(dataList);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        };

        return await tcs.Task;

    }
    
}
