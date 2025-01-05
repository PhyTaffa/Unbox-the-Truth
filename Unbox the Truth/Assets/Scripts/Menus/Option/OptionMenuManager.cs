using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization; // For scene loading
using UnityEngine.UI;

public class OptionMenuManager : MonoBehaviour
{
    // Add public buttons to connect them in the Inspector
    [Header("Button Navigation")]
    [SerializeField] private Button quitToMMButton;

    // Start is called before the first frame update
    void Start()
    {

        //gap = GetComponent<genericAudioPlayer>();

        quitToMMButton.onClick.AddListener(OnQuitToMMButtonClicked);

    }

    private void OnQuitToMMButtonClicked()
    {
        //gap.m_MyAudioSource.Play();
        SceneManager.LoadScene("MainMenu");
    }
    
}
