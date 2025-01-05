using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAudioPlayerRotation : MonoBehaviour
{
    private AudioSource soundToPlay;
    private Dictionary<Action, AudioClip> actionSoundMap;
    public List<ActionSound> actionSounds;
    
    [System.Serializable]
    public class ActionSound
    {
        public Action action;
        public AudioClip clip;
    }
    
    // Enum for different actions
    public enum Action
    {
        Left,
        Right
    }
    void Start()
    {
        soundToPlay = GetComponent<AudioSource>();
        
        //fetch a recolleciton of sudioClip from the Resource fodler
        
        //soundDicitonary.Add(0, Resources.Load<AudioClip>("Sounds/button click"));
        
        // Initialize the dictionary
        actionSoundMap = new Dictionary<Action, AudioClip>();
        foreach (var actionSound in actionSounds)
        {
            actionSoundMap[actionSound.action] = actionSound.clip;
        }
        
    }

    internal void PlaySpecificSoundRotation(Action action)
    {
        if (actionSoundMap.TryGetValue(action, out AudioClip clip))
        {
            soundToPlay.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"No sound assigned for action: {action}");
        }
    }
}
