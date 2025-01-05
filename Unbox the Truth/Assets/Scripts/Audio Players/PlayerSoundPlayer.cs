using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundPlayer : MonoBehaviour
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
        Jump,
        PickUp,
        Throw,
        StartMove,
        StopMove,
        Land,
        StartDisguise,
        StopDisguise,
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

    internal void PlaySpecificSound(Action action)
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

    internal void PlayOnLoop(Action action)
    {
        soundToPlay.loop = true;
        soundToPlay.Play();
    }

    internal void StopOnLoop(Action action)
    {
        soundToPlay.loop = false;
        soundToPlay.Stop();
    }
}
