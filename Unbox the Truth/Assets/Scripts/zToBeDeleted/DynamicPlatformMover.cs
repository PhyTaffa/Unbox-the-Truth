using UnityEngine;
using System.Collections;

public class DynamicPlatformMover : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    public Transform target; // The target position
    public float moveSpeed = 1f; // Speed of the platform movement
    private bool isMoving = false; // Flag to check if the platform is currently moving

    void Update()
    {
        // Control the target position with arrow keys
        MoveTarget();

        // Check for spacebar press to start moving the platform
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            isMoving = true; // Set moving state
            StartCoroutine(MoveToTarget());
        }
    }

    private void MoveTarget()
    {
        // Example movement: adjust target position with arrow keys
        if (Input.GetKey(KeyCode.UpArrow))
        {
            target.position += Vector3.up * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            target.position += Vector3.down * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            target.position += Vector3.left * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            target.position += Vector3.right * Time.deltaTime * speed;
        }
    }

    private IEnumerator MoveToTarget()
    {
        while (isMoving)
        {
            // Move the platform towards the target position
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            // Check if the platform has reached the target
            if (transform.position == target.position)
            {
                isMoving = false; // Stop moving if the target is reached
            }

            yield return null; // Wait until the next frame
        }
    }
}