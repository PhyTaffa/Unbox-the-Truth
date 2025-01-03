using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization; // For scene loading
using UnityEngine.UI;

public class CompanionAppMenuManager : MonoBehaviour
{
    // Add public buttons to connect them in the Inspector
    [Header("Button Navigation")]
    [SerializeField] private Button skinDefaultButton;
    [SerializeField] private Button quitToMMButton;
    private genericAudioPlayer gap;
    
    private Dictionary<int, Sprite> playerSpriteDictionary;
    private bool a;
    // Start is called before the first frame update
    void Start()
    {

        gap = GetComponent<genericAudioPlayer>();
        
        skinDefaultButton.onClick.AddListener(OnSkinDefaultButtonClicked);
        quitToMMButton.onClick.AddListener(OnQuitToMMButtonClicked);

    }

    private void OnSkinDefaultButtonClicked()
    {
        SpriteManagerSingleton.Instance.SelectedSprite = Resources.Load<Sprite>("Images/player/player_default_v2");
    }
    
    private void OnQuitToMMButtonClicked()
    {
        gap.m_MyAudioSource.Play();
        SceneManager.LoadScene("MainMenu");
    }
    
}
