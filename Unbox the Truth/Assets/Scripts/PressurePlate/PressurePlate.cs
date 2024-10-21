using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject interactableObject;
    private IPressurePlateInteractable interactable;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private Vector3 direction;
    private Vector3 updatedDirection;
    private bool reachedTarget;
    private bool moveBack;
    private bool moveBackSafe;
    private bool isRotating;


    
    [SerializeField] private Vector3 spherePosition = Vector3.zero; // Position where the sphere will be drawn


    void Start()
    {
        originalPosition = transform.position;
        targetPosition = transform.TransformPoint(Vector3.down * 0.5f);
        interactable = interactableObject.GetComponent<IPressurePlateInteractable>();
        direction = (targetPosition - originalPosition).normalized;
        isRotating = false;

        WorldRotation worldRotation = FindObjectOfType<WorldRotation>();
        worldRotation.onWorldRotationChangedEvent.AddListener(OnWorldRotationChanged);
        worldRotation.onWorldRotationFinishedEvent.AddListener(OnWorldRotationFinished);

        reachedTarget = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (moveBack && !isRotating)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, Time.deltaTime);

            float curDistance = Vector3.Distance(transform.position, originalPosition);
            float origDist = Vector3.Distance(originalPosition, targetPosition);
            if( curDistance < origDist)
            {
                reachedTarget = false;
            }
            if (curDistance < 0.001f)
            {
                moveBack = false;
            }

        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IPressurePlateTrigger trigger = collision.gameObject.GetComponent<IPressurePlateTrigger>();
        direction = (targetPosition - originalPosition).normalized;
        updatedDirection = direction;
        if (trigger != null) //&& direction == Vector3.down
        {
                
                //collision.transform.parent = transform;
                
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        IPressurePlateTrigger trigger = collision.gameObject.GetComponent<IPressurePlateTrigger>();
        if (trigger != null) // && updatedDirection == Vector3.down
        {
            //transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
            updatedDirection = (targetPosition - originalPosition).normalized;
            float distance = Vector3.Distance(transform.position, targetPosition);
            if(!isRotating && !reachedTarget)
            {
                if(distance > 0.005f)
                {
                    transform.position += updatedDirection * Time.deltaTime * 0.5f;
                    collision.transform.position += updatedDirection * Time.deltaTime * 0.5f;
                } 
                else if(distance < 0.005f)
                {
                    reachedTarget = true;
                }
                

                if(distance < 0.01f)
                {
                    GetComponent<SpriteRenderer>().color = Color.green;
                    interactable.interact();
                }
            }
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IPressurePlateTrigger trigger = collision.gameObject.GetComponent<IPressurePlateTrigger>();
        if (trigger != null)
        {
                GetComponent<SpriteRenderer>().color = Color.white;
                //collision.transform.parent = null;
                reachedTarget = false;
                moveBack = true;
                interactable.unInteract();
        }
    }

    private void OnWorldRotationChanged(Vector3 pivot, Vector3 direction, float rotationAmount)   
    {
        Debug.Log("OnWorldRotationChanged");
        moveBackSafe = moveBack;
        //moveBack = false;
        isRotating = true;
        Debug.Log(isRotating);

        
        // Create a temporary GameObject to use the Transform component
        GameObject tempObject = new GameObject("TempObject");

        tempObject.transform.position = targetPosition;

        tempObject.transform.RotateAround(pivot, direction, rotationAmount);
        targetPosition = tempObject.transform.position;

        tempObject.transform.position = originalPosition;
        tempObject.transform.RotateAround(pivot, direction, rotationAmount);
        originalPosition = tempObject.transform.position;

    }

    private void OnWorldRotationFinished()
    {
        moveBack = moveBackSafe;
        isRotating = false;
        Debug.Log("OnWorldRotationFinished");
    }



    [SerializeField] private float sphereRadius = 0.1f;               
    [SerializeField] private Color sphereColor = Color.red;        
    private void OnDrawGizmos()
    {
        Gizmos.color = sphereColor;
        Gizmos.DrawSphere(targetPosition, sphereRadius);
    }

}
