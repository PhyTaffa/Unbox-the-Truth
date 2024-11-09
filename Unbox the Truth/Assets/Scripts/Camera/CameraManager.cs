using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private float minFOV = 80.0f;
    [SerializeField] private float maxFOV = 120.0f;
    private float currFOV = 80.0f;
    
    [SerializeField] private float FOVChangeRate = 1f;
    [SerializeField] private float cameraDepth = -10.0f;
    private GameObject playerGO;
    private Transform playerTransform;
    
    
    
    [SerializeField] private float currFov = 80f;  // The value that increases (starting at 80)
    [SerializeField] private float maxValue = 120f;     // The maximum value (120)
    [SerializeField] private float increaseSpeed = 0.2f;  // How fast the value increases
    [SerializeField] private float resetSpeed = 0.2f;     // Speed at which the value resets

    private float startValue;  // The initial value (80)



    // [SerializeField] private float chaseSpeed = 4.0f;
    // [SerializeField] private float chaseDurationSeconds = 2f;

    public void Start()
    {
        startValue = currFov;  
        currFOV = minFOV;
        
        
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerGO.GetComponent<Transform>();
        
        
        //transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, cameraDepth);
    }
    
    void Update () 
    {

        //transform.position = Vector3.Lerp(transform.position, playerTransform.transform.position, chaseSpeed * Time.deltaTime / chaseDurationSeconds);
        
        //to be reworkd with states
        // if (Input.GetKey(KeyCode.B))
        // {
        //     StartCoroutine(PanCameraOut());
        //
        // } 
        // else if(Input.GetKeyUp(KeyCode.B))
        // {
        //     StartCoroutine(PanCameraIn());
        //
        // }

        // if (Input.GetKey(KeyCode.B))
        // {
        //     FOVChange();
        // }
        
        // If the key is pressed, increase the value
        // if (Input.GetKey(KeyCode.B))  // If the "B" key is pressed
        // {
        //     // Increase the value, but clamp it to the maxValue
        //     currFov = Mathf.Min(currFov + increaseSpeed, maxValue);
        // }
        // else if (currFov != startValue)  // If the key is not pressed and the value isn't at the start
        // {
        //     // Smoothly reset the value back to the start value
        //     currFov = Mathf.MoveTowards(currFov, startValue, resetSpeed);
        // }
        //
        // Camera.main.fieldOfView = currFov;
    }

    private IEnumerator PanCameraOut()
    {
        //StopCoroutine(nameof(PanCameraIn));
        while (currFOV < maxFOV)
        { 
            currFOV += FOVChangeRate / 4F;
            
            Camera.main.fieldOfView = currFOV;

            yield return null;
        }

        currFOV = maxFOV;
    }
    
    private IEnumerator PanCameraIn()
    {
        StopCoroutine(nameof(PanCameraOut));
        while (currFOV > minFOV)
        {
            currFOV -= FOVChangeRate;
           
            Camera.main.fieldOfView = currFOV;
            yield return null;
        }
        currFOV = minFOV;
    }

    private void FOVChange()
    {
        if (currFOV < maxFOV)
        {
            currFOV += FOVChangeRate;
        }
        else
        {
            currFOV -= maxFOV;
        }
    }
}
