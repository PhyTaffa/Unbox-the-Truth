using UnityEngine;
public class MovingPt : MonoBehaviour
{
    [SerializeField] public float speed;
    public int startingPoint; 
    public Transform[] points;
    private int i;
    private genericAudioPlayerLevels gapl;

    private GameObject worldRoot;
    void Start()

    {
        i = startingPoint;
        
        worldRoot = GameObject.FindGameObjectWithTag("WorldRoot");
        
        gapl = GetComponent<genericAudioPlayerLevels>();
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
        else
        {
            
            gapl.DJPPPPlayThatShid();
            i++;
            if (i == points.Length)
            {
                i = 0; // Loop back to the start
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (transform.position.y < collision.transform.position.y)
            {
                //being able to move the player while still being in the world root.
                //Rigidbody2D rb = GetComponent<Rigidbody2D>();
                //Debug.Log(rb.velocity);
                //transform.parent = collision.transform.parent;
                //collision.rigidbody.velocity = new Vector2(speed * Time.deltaTime, 0f);
                
                collision.transform.SetParent(transform);
            }
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //Transform wrT = worldRoot.transform;
            //transform.parent = worldRoot.transform.parent;

            
            collision.transform.SetParent(null);
        } 
    }
}
