using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IPlayer : MonoBehaviour, IPressurePlateTrigger
{
    Movement move;
    GameObject player;

    private bool CanRotateWorld;
    
    //[SerializeField] float distance = 1.5f;

    void Start()
    {
        move = GetComponent<Movement>();
        player = GameObject.FindGameObjectWithTag("Player"); 
        CanRotateWorld = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
