using System;
using System.Collections.Generic;
using System.Linq;
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

[Serializable]
public class ChallengeCompleted
{
    public bool challengeMet;
}
[Serializable]
public class ChallengeCompletedArray
{
    public ChallengeCompleted[] challengesMetArr;
}

public class EndpointCalls
{
    private const string BaseUrl = "https://serve-the-truth.vercel.app/api/";
    private int challengesLength = 7;
    
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
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in GET request: {ex.Message}");
            //should change some ui elements to let the user know that the requests has gone wrong
        }
    }

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

            challengesLength = challengeList.challenges.Length;
            
            return challengeList.challenges.Length;
        }
        catch (Exception ex)
        {
            
            Debug.LogError($"Error in GET request: {ex.Message}");
            //should change some ui elements to let the user know that the requests has gone wrong
            return -1;
        }
    }

    internal async Task<bool[]> GetNumberChallengesWithUserUniqueId(int userUniqueId)
    {
        string endpoint = $"{BaseUrl}/challenge/getCompletedChallengesByUniqueId/?uiUniqueId={userUniqueId}";
        bool[] challengesMetArray = new bool[challengesLength];
        bool[] challengeDefaultValue = { false, false, false, false, false, false, false};
        
        try
        {
            string jsonResponse = await SendGetRequestAsync(endpoint);

            if (!string.IsNullOrEmpty(jsonResponse))
            {
                
                //Debug.Log($"GET Response: {jsonResponse}");

                string json = $"{{ \"challengesMetArr\": {jsonResponse} }}";
            
                Debug.Log($"GET Response Wrapped: {json}");

                //THIS ARRAY OF SHIT IS NULL, im going to commit arson.
                ChallengeCompletedArray challengesCompleted = JsonUtility.FromJson<ChallengeCompletedArray>(json);
            
                // Update challengesMetArray based on the received data
                for (int i = 0; i < challengesLength; i++)
                {
                    Debug.Log(challengesCompleted.challengesMetArr[i]);
                    challengesMetArray[i] = challengesCompleted.challengesMetArr[i].challengeMet;
                }
            
                return challengesMetArray;
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"Error in GET request: {ex.Message}");

            //return challengeDefaultValue;
        }
        
        return challengeDefaultValue;
    }
    
    
}