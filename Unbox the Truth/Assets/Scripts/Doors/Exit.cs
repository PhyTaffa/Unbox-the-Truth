using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class OnLevelExit : UnityEvent { }

public class Exit : MonoBehaviour
{
    [SerializeField] public OnLevelExit onLevelExitEvent;
    [SerializeField] private LayerMask playerLayer;  // Specify the player layer

    private GenericAudioPlayerLevels gapl;
    // private GameObject player;
    // private Cheats cheats;

    private void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player");
        // cheats = player.GetComponent<Cheats>();
        gapl = GetComponent<GenericAudioPlayerLevels>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        // Check if the player is in the trigger and the correct key is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            gapl.DJPPPPlayThatShid();
            // Check if the object entering the trigger is on the playerLayer
            if (((1 << col.gameObject.layer) & playerLayer) != 0 && col.CompareTag("Player"))
            {
                onLevelExitEvent?.Invoke();
                //Debug.Log("Exit");
            }
            else if (((1 << col.gameObject.layer) & playerLayer) == 0)
            {
                // If it's not the player (e.g., the circle), ignore the interaction
                //Debug.Log("Ignoring non-player interaction.");
            }
        }
    }
}