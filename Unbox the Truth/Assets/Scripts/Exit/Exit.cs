using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[System.Serializable] public class OnLevelExit: UnityEvent { }

public class Exit : MonoBehaviour
{
    public OnLevelExit onLevelExitEvent;

    private void OnTriggerStay2D(Collider2D col)
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            if (col.gameObject.CompareTag("Player"))
            {
                onLevelExitEvent?.Invoke();
                Debug.Log("Exit");
            }
        }
        
    }

}
