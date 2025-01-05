using System.Threading.Tasks;
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
    private BoxCollider2D boxCollider;
    private GenericAudioPlayerLevels gapl;
    
    private void Start()
    {
        gm = GameObject.FindWithTag("GM");
        gms = gm.GetComponent<GameManager>();
        
        player = GameObject.FindWithTag("Player");
        playerCheat = player.gameObject.GetComponent<Cheats>();
        boxCollider = player.GetComponent<BoxCollider2D>();
        gapl = GetComponent<GenericAudioPlayerLevels>();
    }
    
    private async void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.gameObject.CompareTag("Player") && playerCheat.isKillable == true)
        {
            // the sound deons't get played since the death is istanteneous
            gapl.DJPPPPlayThatShid();
            //collider.enabled = false;
            //lazy way to get the sound to work and, allegedly, add a animaiton
            //await Task.Delay(30);
            gms.OnPlayerDied();
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
