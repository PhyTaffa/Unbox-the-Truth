using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
 
    [SerializeField] private string sceneName;
    private genericAudioPlayerLevels gapl;

    private void Start()
    {
        gapl = GetComponent<genericAudioPlayerLevels>();
    }
    private async void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                gapl.DJPPPPlayThatShid();
                
                await Task.Delay(150);
                
                SceneManager.LoadScene(sceneName);
            }
        }
    }



}

