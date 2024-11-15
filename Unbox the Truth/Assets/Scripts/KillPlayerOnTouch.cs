using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnPlayerDied : UnityEvent { }

public class KillPlayerOnTouch : MonoBehaviour
{
    
    public OnPlayerDied onPlayerDiedEvent;
    private GameObject gm;
    protected GameManager gms;
    
    private void Start()
    {
        gm = GameObject.FindWithTag("GM");
        gms = gm.GetComponent<GameManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && collider.gameObject.GetComponent<Cheats>().isKillable == true)
        {
            gms.OnPlayerDied();
            //onPlayerDiedEvent.Invoke();
            //Debug.Log("Game Over");
        }
    }
}
