using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnPlayerDied : UnityEvent { }

public class KillPlayerOnTouch : MonoBehaviour
{
    
    public OnPlayerDied onPlayerDiedEvent;
    private GameObject gm;
    protected GameManager gms;

    private GameObject player;
    private Cheats playerCheat;
    
    private void Start()
    {
        gm = GameObject.FindWithTag("GM");
        gms = gm.GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player");
        playerCheat = player.gameObject.GetComponent<Cheats>();
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {


        if (collider.gameObject.CompareTag("Player") && playerCheat.isKillable == true)
        {
            gms.OnPlayerDied();
            //onPlayerDiedEvent.Invoke();
            //Debug.Log("Game Over");
        }
    }

    void Update()
    {
        if(Physics2D.OverlapCircle(player.transform.position, 0.5f, LayerMask.GetMask("Kill")) && playerCheat.isKillable == true)
        {
            gms.OnPlayerDied();
        }
    }



}
