using UnityEngine;

public class BufferZoneAttachment : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float moveSpeed = 4.0f;
    //[SerializeField] private float minMoveSpeed = 4f;
    //[SerializeField] private float maxMoveSpeed = 8f;
    [SerializeField] private float chaseDurationSeconds = 0.5f;

    //useless stuff for now
    //private float speedDelta = 0.1f;
    private const float MaxChaseDuration = 2f;
    private const float MinChaseDuration = 1f;

    void Start()
    {
        //snaps the bufferzone to the player
        player = GameObject.FindWithTag("Player");
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, moveSpeed * Time.deltaTime / chaseDurationSeconds);

        // if (Input.GetKeyDown(KeyCode.N))
        // {
        //     seconds = 1f;
        // }
        //
        // if (Input.GetKeyDown(KeyCode.M))
        // {
        //     seconds = 10f;
        // }
    }
    //
    // private void OnTriggerEnter2D(Collider col)
    // {
    //     if (col.gameObject.CompareTag("Player"))
    //     {
    //         
    //     }
    // }
    //
    //
    // private void OnTriggerExit2D(Collider2D col)
    // {
    //     if (col.gameObject.CompareTag("Player"))
    //     {
    //         chaseDurationSeconds = MinChaseDuration;
    //         // StopCoroutine(nameof(DecreaseChaseSpeed));
    //         // StartCoroutine(IncreaseChaseSpeed());
    //     }
    // }
    // private void OnTriggerStay2D(Collider2D col)
    // {
    //     if (col.gameObject.CompareTag("Player"))
    //     {
    //         chaseDurationSeconds = MaxChaseDuration;
    //         //StopCoroutine(nameof(IncreaseChaseSpeed));
    //         //StartCoroutine(DecreaseChaseSpeed());
    //     }
    // }
    
    
    // private IEnumerator IncreaseChaseSpeed()
    // {
    //     
    //     while(moveSpeed < maxMoveSpeed)
    //     {
    //         //moveSpeed += speedDelta;
    //         
    //         moveSpeed = Mathf.Lerp(moveSpeed, maxMoveSpeed, speedDelta * Time.time / seconds);
    //         yield return null;
    //     }
    //     
    //     moveSpeed = maxMoveSpeed;
    //     
    // }
    //

    //
    // private IEnumerator DecreaseChaseSpeed()
    // {
    //     
    //     while(moveSpeed > minMoveSpeed)
    //     {
    //         moveSpeed = Mathf.Lerp(moveSpeed, minMoveSpeed, speedDelta * Time.time / seconds);
    //         
    //         //moveSpeed -= speedDelta;
    //         
    //         yield return null;
    //     }
    //     
    //     moveSpeed = minMoveSpeed;
    //     
    // }
}
