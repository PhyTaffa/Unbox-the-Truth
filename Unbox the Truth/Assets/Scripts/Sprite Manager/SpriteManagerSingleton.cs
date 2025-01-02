using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManagerSingleton : MonoBehaviour
{
    public static SpriteManagerSingleton Instance { get; private set; }

    public Sprite SelectedSprite { get; set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        //default sprite
        SelectedSprite = Resources.Load<Sprite>("Images/player/player_default_v2");
    }
}
