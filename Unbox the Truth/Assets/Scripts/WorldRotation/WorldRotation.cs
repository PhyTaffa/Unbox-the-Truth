using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnWorldRotationChanged : UnityEvent<Vector3, Vector3, float> { }
[System.Serializable]
public class OnWorldRotationFinished: UnityEvent { }


public class WorldRotation : MonoBehaviour
{

    private GameObject player;
    private Movement playerMoveComponent;
    private Transform world;
    private bool canRotate = true;
    private bool isRotating = false;
    public float rotationDuration;
    private float rotationAmount = 0f;
    private float currentRotationTime = 0f;
    private float targetRotation = 90f;
    private Vector3 playerPosition;
    private Vector3 rotationDirection;
    private Vector3 rotationCorrectionDirection;
    private Vector2 velocitySafe;

    public OnWorldRotationChanged onWorldRotationChangedEvent;
    public OnWorldRotationFinished onWorldRotationFinishedEvent;

    //Cheats
    private Cheats cheats;
    
    void Start()
    {
        //Get the player and world objects by tag
        player = GameObject.FindWithTag("Player");
        world = GameObject.FindWithTag("WorldRoot").transform;

        if (player == null)
        {
            Debug.LogError("Player not found");
        }else{
            playerMoveComponent = player.GetComponent<Movement>();
        }
        
        if (world == null)
        {
            Debug.LogError("WorldRoot not found");
        }
        
        cheats = player.GetComponent<Cheats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cheats.canSpamRotation)
        {
            canRotate = true;
        }
        // Check for input to rotate the world and set the rotation direction
        if (Input.GetKeyDown(KeyCode.Q) && !isRotating)
        {

            if(canRotate && !playerMoveComponent.isCarryingObject)
            {
               //if setting the parent is heavy we can perform an early check.
                player.transform.SetParent(null);
                
                rotationDirection = Vector3.forward;
                rotationCorrectionDirection = Vector3.back;
                if(!playerMoveComponent.IsGrounded()){
                    canRotate = false;
                }
                canRotate = false;
                StartRotation();
            }
            
        } 
        else if (Input.GetKeyDown(KeyCode.E) && !isRotating)
        {

            if(canRotate && !playerMoveComponent.isCarryingObject)
            {
                player.transform.SetParent(null);
            
                rotationDirection = Vector3.back;
                rotationCorrectionDirection = Vector3.forward;
                if(!playerMoveComponent.IsGrounded()){
                    canRotate = false;
                }
                canRotate = false;
                StartRotation();
            } 
        }
    }

    private void StartRotation()
    {
        // Safe velocity to reapply after rotation
        //Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        //velocitySafe = playerRb.velocity;


        PausePhysicsSimulation();  // Pause the physics simulation while rotating
        
        isRotating = true;
        currentRotationTime = 0f;  // Reset the timer for this rotation
        rotationAmount = 0f;       // Reset the current rotation amount


        // Get the player's position (as the pivot)
        playerPosition = player.transform.position;
        playerPosition = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);

        onWorldRotationChangedEvent.Invoke(playerPosition, rotationDirection, targetRotation);
    }

     void FixedUpdate()
    {
        if (isRotating)
        {
            PerformRotation(playerPosition);
        }
    }

    private void PerformRotation(Vector3 rotatePosition)
    {
        // Increment the timer
        currentRotationTime += Time.fixedDeltaTime;

        // Calculate how much rotation should happen this frame
        float stepRotation = (targetRotation / rotationDuration) * Time.fixedDeltaTime;

        // Accumulate the rotation
        rotationAmount += stepRotation;

        // Rotate the world around the player
        world.RotateAround(rotatePosition, rotationDirection, stepRotation);

        // work on correction!!
        // Stop rotating after 1 second or when rotation reaches 90 degrees
        if (Mathf.Abs(rotationAmount) >= targetRotation)
        {
            isRotating = false;

            // Snap the final rotation in case of small discrepancies due to floating point precision
            float correction = targetRotation - rotationAmount;
            if (Mathf.Abs(correction) > 0)
            {
                world.RotateAround(playerPosition, -rotationCorrectionDirection, correction);
            }
            onWorldRotationFinishedEvent.Invoke();
            ResumePhysicsSimulation();  // Resume the physics simulation after rotation
        }
    }

    private void PausePhysicsSimulation()
    {
        Physics2D.simulationMode = SimulationMode2D.Script;;  // Stops all physics calculations globally
    }

    private void ResumePhysicsSimulation()
    {
        Physics2D.simulationMode = SimulationMode2D.FixedUpdate;  // Resume physics calculations globally
    }

    public void SetCanRotate(bool value)
    {
        canRotate = value;
    }
}
