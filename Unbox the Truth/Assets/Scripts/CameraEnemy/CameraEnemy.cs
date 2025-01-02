using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraEnemy : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.5f;
    [SerializeField] private float rotationAngle = 45f;
    [SerializeField] private float fovAngle = 60f; // Field of view angle
    [SerializeField] private float viewDistance = 20f; // Distance the enemy can see
    private Transform playerTransform; 
    private Movement playerMovement;
    private float startAngle;

    private Light2D spotLight;
    public OnPlayerDied onPlayerDiedEvent;

    private float RotationCorrection;


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        startAngle = transform.rotation.eulerAngles.z;
        spotLight = GetComponentInChildren<Light2D>();
        spotLight.pointLightInnerRadius = 0;
        spotLight.pointLightOuterRadius = viewDistance;
        spotLight.pointLightOuterAngle = fovAngle+1;
        spotLight.pointLightInnerAngle = fovAngle;

        WorldRotation worldRotation = FindObjectOfType<WorldRotation>();
        worldRotation.onWorldRotationChangedEvent.AddListener(OnWorldRotationChanged);

        RotationCorrection = 0;
    }

        void Update()
    {
        
        // Rotate the camera back and forth
        float angle = Mathf.Sin(Time.time * rotationSpeed) * rotationAngle;
        transform.rotation = Quaternion.Euler(0, 0, startAngle + angle - RotationCorrection);

        // Check if the player is within the field of view
        if(!playerMovement.GetIsHiding()){
            DetectPlayer();
        }
        
    }

    private void DetectPlayer()
    {
    
        // Get the direction to the player
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

        // Get the camera's forward direction (use 'right' in 2D for local forward axis)
        Vector3 cameraForward = transform.right;

        // Calculate the dot product
        float dotProduct = Vector3.Dot(cameraForward, directionToPlayer);

        // Calculate the threshold dot product based on the FOV angle
        float threshold = Mathf.Cos(fovAngle * 0.5f * Mathf.Deg2Rad);

        // Check if the player is within the FOV
        if (dotProduct > threshold)
        {
            // Player is within the field of view

            if(CheckVisibility()){
                onPlayerDiedEvent.Invoke();
            }
            

        }
    }

    private bool CheckVisibility()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude+1;

        Debug.DrawRay(transform.position, directionToPlayer.normalized*viewDistance, Color.yellow); 

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer.normalized, viewDistance);
        if(hit.collider != null){
            if(hit.collider.CompareTag("Player"))
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
        return false;
        
    }

       private void OnWorldRotationChanged(Vector3 pivot, Vector3 direction, float angle)
    {
        if(direction == Vector3.forward)
        {
            RotationCorrection += -90f;
        }
        else if(direction == Vector3.back)
        {
            RotationCorrection += 90f;
        }
    }
}
