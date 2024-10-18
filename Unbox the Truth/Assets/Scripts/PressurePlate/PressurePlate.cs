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
    private Vector3 originalScale;
    private bool moveBack;
    private bool moveBackSafe;

    private bool isRotating;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        targetPosition = transform.TransformPoint(Vector3.down * 0.5f);
        interactable = interactableObject.GetComponent<IPressurePlateInteractable>();
        direction = (targetPosition - originalPosition).normalized;
        isRotating = false;

        WorldRotation worldRotation = FindObjectOfType<WorldRotation>();
        worldRotation.onWorldRotationChangedEvent.AddListener(OnWorldRotationChanged);
        //worldRotation.onWorldRotationFinishedEvent.AddListener(OnWorldRotationFinished);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (moveBack)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IPressurePlateTrigger trigger = collision.gameObject.GetComponent<IPressurePlateTrigger>();
        direction = (targetPosition - originalPosition).normalized;
        if (trigger != null && direction == Vector3.down)
        {
                
                //collision.transform.parent = transform;
                
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        IPressurePlateTrigger trigger = collision.gameObject.GetComponent<IPressurePlateTrigger>();
        if (trigger != null && direction == Vector3.down)
        {
            //transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
            float distance = Vector3.Distance(transform.position, targetPosition);
            if(!isRotating)
            {
                if(distance > 0.005f)
                {
                    transform.position += direction * Time.deltaTime * 0.5f;
                    collision.transform.position += direction * Time.deltaTime * 0.5f;
                }
                moveBack = false;

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
        if (trigger != null && direction == Vector3.down)
        {
                GetComponent<SpriteRenderer>().color = Color.white;
                //collision.transform.parent = null;
                moveBack = true;
        }
    }

    private void OnWorldRotationChanged(Vector3 pivot, Vector3 direction, float rotationAmount)   
    {
        Debug.Log("OnWorldRotationChanged");
        moveBackSafe = moveBack;
        //moveBack = false;
        //isRotating = true;

        
        // Create a temporary GameObject to use the Transform component
        GameObject tempObject = new GameObject("TempObject");
        // Set its position to the initial position (the Vector3 position)
        tempObject.transform.position = targetPosition;
        // Rotate around a specific point using RotateAround
        tempObject.transform.RotateAround(pivot, direction, rotationAmount);
        targetPosition = tempObject.transform.position;

        tempObject.transform.position = originalPosition;
        // Rotate around a specific point using RotateAround
        tempObject.transform.RotateAround(pivot, direction, rotationAmount);
        originalPosition = tempObject.transform.position;

    }

    private void OnWorldRotationFinished()
    {
        moveBack = moveBackSafe;
        isRotating = false;
        Debug.Log("OnWorldRotationChanged");
    }

}
