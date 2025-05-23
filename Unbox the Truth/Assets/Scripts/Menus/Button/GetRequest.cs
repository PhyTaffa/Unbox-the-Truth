using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetRequest : MonoBehaviour
{
    private GameObject codeGO;
    private EndpointCalls endpointCalls;
    private Button button;
    [SerializeField] private TMP_InputField codeInput;
    private TMP_InputField ip;
    private SkinsSpawningManager ssm;
    
    // Start is called before the first frame update
    void Start()
    {
        codeGO = GameObject.FindGameObjectWithTag("Input Code");
        ip = codeGO.GetComponent<TMP_InputField>();
        //Debug.Log(codeGO.name);
        endpointCalls = new EndpointCalls();
        
        button = GetComponent<Button>();
        button.onClick.AddListener(GetCode);
        ssm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SkinsSpawningManager>();
        
        //inputField.onEndEdit.AddListener(OnEndEdit);
    }

    async void GetCode()
    {
        string uniqueId = ip.text;
        
        //needs timer if it's already running, waits for the end to do it again.
        try
        {
            bool[] challengeMet = new bool[]{ false };
            //TestCompanionApp cpa = GetComponent<TestCompanionApp>();
            challengeMet = await endpointCalls.GetNumberChallengesWithUserUniqueId(int.Parse(uniqueId));
            ssm.EnableButton(challengeMet);
            
        }
        catch (Exception ex)
        {
            Debug.Log($"Error in GET request: {ex.Message}");
        }
    }
}
