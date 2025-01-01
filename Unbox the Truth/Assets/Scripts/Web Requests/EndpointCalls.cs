using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class UserData
{
    public int ui_id;
    public string ui_name;
    public string ui_email;
    public int ui_pfp;
    public int ui_activeChallRef;
    public int ui_uniqueId;
}

[Serializable]
public class Challenge
{
    public int sc_id;                // Challenge ID
    public string sc_title;          // Title of the challenge
    public string sc_description;    // Description of the challenge
    public int sc_assets;            // Asset ID
    public int sc_skin;              // Skin ID
    public int sc_difficulty;        // Difficulty level
    public int sc_stepsToReach;      // Steps required to complete
    public int sc_timeLimit;         // Time limit for the challenge
}
[System.Serializable]
public class ChallengeList
{
    public Challenge[] challenges;
}


public class EndpointCalls
{
    private const string BaseUrl = "https://serve-the-truth.vercel.app/api/";

    // private async void Start()
    // {
    //     // Example GET request
    //     GetUserByIdAsync(4);
    //
    //     // Example POST request
    //     //var formData = new Dictionary<string, string> { { "userId", "4" } };
    //     //PostDataAsync($"{BaseUrl}/", formData);
    //
    //     int n = await GetNumberChallenges();
    //     
    //     if (n != -1)
    //     {
    //         Debug.Log($"Number of challenges: {n}");
    //         
    //         await Task.Delay(1000);
    //         Debug.Log(" a second waited");
    //     }
    //     else
    //     {
    //         Debug.LogError("Failed to retrieve the number of challenges.");
    //     }
    // }

    private async void GetUserByIdAsync(int userId)
    {
        string endpoint = $"{BaseUrl}userInfo/getById/?userId={userId}";
        try
        {
            string jsonResponse = await SendGetRequestAsync(endpoint);
            UserData userData = JsonUtility.FromJson<UserData>(jsonResponse);
            
            // Deserialize JSON into a dictionary
            Dictionary<string, object> jsonDict = JsonUtility.FromJson<Dictionary<string, object>>(jsonResponse);
            jsonDict.Add("id", userData);
            
            Debug.Log($"GET Response: {jsonResponse}");
            // // Count properties
            // int propertyCount = jsonDict.Count;
            // Debug.Log($"Number of properties: {propertyCount}");
            // Debug.Log($"MessageID: {userData.ui_email}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in GET request: {ex.Message}");
            //should change some ui elements to let the user know that the requests has gone wrong
        }
    }

    // private async void PostDataAsync(string endpoint, Dictionary<string, string> formData)
    // {
    //     // try
    //     // {
    //     //     string jsonResponse = await SendPostRequestAsync(endpoint, formData);
    //     //     Debug.Log($"POST Response: {jsonResponse}");
    //     // }
    //     // catch (Exception ex)
    //     // {
    //     //     Debug.LogError($"Error in POST request: {ex.Message}");
    //     // }
    // }

    private async Task<string> SendGetRequestAsync(string uri)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            var operation = request.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();

            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new Exception($"GET request failed: {request.error}");
            }

            return request.downloadHandler.text;
        }
    }

    // private async Task<string> SendPostRequestAsync(string uri, Dictionary<string, string> formData)
    // {
    //     WWWForm form = new WWWForm();
    //     foreach (var field in formData)
    //     {
    //         form.AddField(field.Key, field.Value);
    //     }
    //
    //     using (UnityWebRequest request = UnityWebRequest.Post(uri, form))
    //     {
    //         var operation = request.SendWebRequest();
    //         while (!operation.isDone)
    //             await Task.Yield();
    //
    //         if (request.result != UnityWebRequest.Result.Success)
    //         {
    //             throw new Exception($"POST request failed: {request.error}");
    //         }
    //
    //         return request.downloadHandler.text;
    //     }
    // }

    internal async Task<int> GetNumberChallenges()
    {
        string endpoint = $"{BaseUrl}/challenge/all";

        try
        {
            string jsonResponse = await SendGetRequestAsync(endpoint);
            
            // Wrap the JSON array in a single object
            string json = $"{{ \"challenges\": {jsonResponse} }}";

            // Deserialize JSON into ChallengeList
            ChallengeList challengeList = JsonUtility.FromJson<ChallengeList>(json);

            //Debug.Log($"GET Response Length: {challengeList.challenges.Length}");
            // foreach (Challenge challenge in challengeList.challenges)
            // {
            //     Debug.Log("Challenge Title: " + challenge.sc_title);
            // }
            return challengeList.challenges.Length;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in GET request: {ex.Message}");
            //should change some ui elements to let the user know that the requests has gone wrong
            return -1;
        }
    }
}
