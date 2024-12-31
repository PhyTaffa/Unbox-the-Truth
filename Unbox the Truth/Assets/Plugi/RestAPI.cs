using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

struct UnJasonedData
{
    public string MessageID;
}

public class RestAPI : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetRequest("https://serve-the-truth.vercel.app/api/userInfo/getById/?userId=4"));
        WWWForm formData = new WWWForm();
        formData.AddField("userId", 4);

        StartCoroutine(PostRequest("https://serve-the-truth.vercel.app/api/userInfo/getById/?userId=4", formData));
    }

    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest WebRequest = UnityWebRequest.Get(uri);
        yield return WebRequest.SendWebRequest();

        if (WebRequest.result == UnityWebRequest.Result.Success)
        {
            // Parse Information
            Debug.Log(WebRequest.downloadHandler.text);

            UnJasonedData data = (UnJasonedData)JsonUtility.FromJson(WebRequest.downloadHandler.text, typeof(UnJasonedData));

            Debug.Log(data.MessageID);
        }
        else
        {
            Debug.Log("Error While Sending: " + WebRequest.error);
        }
    }

    IEnumerator PostRequest(string uri, WWWForm composedMessage)
    {
        UnityWebRequest WebRequest = UnityWebRequest.Post(uri, composedMessage);
        yield return WebRequest.SendWebRequest();

        if (WebRequest.result == UnityWebRequest.Result.Success)
        {
            // Parse Information
            Debug.Log(WebRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error While Sending: " + WebRequest.error);
        }
    }


}