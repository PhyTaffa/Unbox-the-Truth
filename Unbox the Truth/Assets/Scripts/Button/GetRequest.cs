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
    
    // Start is called before the first frame update
    void Start()
    {
        codeGO = GameObject.FindGameObjectWithTag("Input Code");
        //Debug.Log(codeGO.name);
        endpointCalls = new EndpointCalls();
        
        button = GetComponent<Button>();
        button.onClick.AddListener(GetCode);
        
        //inputField.onEndEdit.AddListener(OnEndEdit);
    }

    public void GetCode()
    {
        TMP_InputField  ip = codeGO.GetComponent<TMP_InputField>();
        //Debug.Log(ip.text);
        string uniqueId = ip.text;

        //needs timer
        endpointCalls.GetNumberChallengesWithUserUniqueId(int.Parse(uniqueId));

    }
}
