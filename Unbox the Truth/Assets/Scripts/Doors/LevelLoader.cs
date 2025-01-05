using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
 
    [SerializeField] private string sceneName;
    private GenericAudioPlayerLevels gapl;

    private void Start()
    {
        gapl = GetComponent<GenericAudioPlayerLevels>();
    }
    private async void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                gapl.DJPPPPlayThatShid();
                
                await Task.Delay(450);
                
                SceneManager.LoadScene(sceneName);
            }
        }
    }



}

