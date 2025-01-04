using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericAudioPlayerLevels : MonoBehaviour
{
    internal AudioSource m_MyAudioSourceLevels;
    // Start is called before the first frame update
    void Start()
    {
        m_MyAudioSourceLevels = GetComponent<AudioSource>();
    }

    internal void DJPPPPlayThatShid()
    {
        m_MyAudioSourceLevels.Play();
    }
}
