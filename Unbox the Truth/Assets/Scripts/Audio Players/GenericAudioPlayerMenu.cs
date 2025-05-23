using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericAudioPlayerMenu : MonoBehaviour
{
    internal AudioSource m_MyAudioSource;

    //Play the music
    internal bool m_Play;
    //Detect when you use the toggle, ensures music isn’t played multiple times
    private bool m_ToggleChange;

    private Button[] m_Button;
    void Start()
    {
        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = GetComponent<AudioSource>();
        //Ensure the toggle is set to true for the music to play at start-up
        m_Play = false;
        
        GenerateButtonArray();
        
        
        //on click event
        foreach (Button b in m_Button)
        {
            b.onClick.AddListener(() =>
            {
                m_MyAudioSource.Play();
            });
        }

    }

    internal void GenerateButtonArray()
    {
        m_Button = null;
        m_Button = GetComponentsInChildren<Button>();
    }

    internal void AddButton(Button button)
    {
        int i = m_Button.Length;
        m_Button[i] = button;
    }
    
    // void OnGUI()
    // {
    //     //Switch this toggle to activate and deactivate the parent GameObject
    //     m_Play = GUI.Toggle(new Rect(10, 10, 100, 30), m_Play, "Play Music");
    //
    //     //Detect if there is a change with the toggle
    //     if (GUI.changed)
    //     {
    //         //Change to true to show that there was just a change in the toggle state
    //         m_ToggleChange = true;
    //     }
    // }
}
