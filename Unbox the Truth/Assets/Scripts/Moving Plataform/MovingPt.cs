using UnityEngine;
public class MovingPt : MonoBehaviour
{
    public float speed;
    public int startingPoint; 
    public Transform[] points;
    private int i;
    void Start()

    {
        transform.position = points[startingPoint].position;
        i = startingPoint; 
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) > 0.2f)

        {
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
        else
        {
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
            collision.transform.SetParent(transform);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        } 
    }
}
