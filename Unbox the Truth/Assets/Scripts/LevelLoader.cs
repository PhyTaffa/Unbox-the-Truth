using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
 
    [SerializeField] private string sceneName;

    private void OnTriggerStay2D(Collider2D col)
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Debug.Log("Load Level");
                SceneManager.LoadScene(sceneName);
            }
        }
        
    }



}

