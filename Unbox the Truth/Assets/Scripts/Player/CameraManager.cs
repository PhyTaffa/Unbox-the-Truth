using System.Collections;
using UnityEngine;

namespace Player
{
    public class CameraManager : MonoBehaviour
    {

        [SerializeField] private float minFOV = 80.0f;
        [SerializeField] private float maxFOV = 120.0f;
        private float currFOV = 80.0f;
        [SerializeField] private float FOVChangeRate = 1f;
        [SerializeField] private float cameraDepth = -10.0f;
        private GameObject playerGO;
        private Transform playerTransform;
        
        //buffer zone
        private GameObject cameraBZ;
        private Transform cameraBZTransform;

        public void Start()
        {
            currFOV = minFOV;
            
            
            playerGO = GameObject.FindGameObjectWithTag("Player");
            playerTransform = playerGO.transform;
            
            
            
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, cameraDepth);
            
            //Camera Buffer zone
            
            //fetches the buffer zone component
            cameraBZTransform = transform.GetChild(0);

            if (cameraBZTransform != null)
            {
                Debug.Log("Child GameObject found: " + cameraBZTransform.name);
            }
            else
            {
                Debug.LogError("Child GameObject not found.");
            }
            
        }
        
        void Update () 
        {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, cameraDepth);
            MoveBufferZone(cameraBZTransform);
            //transform.position = new Vector3 (player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows 
            
            //uses the same input down. and decides which 
            if (Input.GetKeyDown(KeyCode.B))
            {
                StartCoroutine(PanCameraOut());
                
                //Camera.main.fieldOfView = maxFOV;
            }
            
            if(Input.GetKeyUp(KeyCode.B))
            {
                StartCoroutine(PanCameraIn());
                
                //Camera.main.fieldOfView = minFOV;
            }
        }

        private IEnumerator PanCameraOut()
        {
            while (currFOV < maxFOV)
            { 
                currFOV += FOVChangeRatePerFrame;
                
                Camera.main.fieldOfView = currFOV;

                yield return null;
                //yield return new WaitForSeconds(5); // Wait until the next frame
            }

            currFOV = maxFOV;
        }
        
        private IEnumerator PanCameraIn()
        {
            while (currFOV > minFOV)
            {
                currFOV += -FOVChangeRatePerFrame;
               
                Camera.main.fieldOfView = currFOV;
                yield return null; // Wait until the next frame
            }
            currFOV = minFOV;
        }
        
        private void MoveBufferZone(Transform bufferZone)
        {
            bufferZone.position = new Vector3(transform.position.x, transform.position.y, -0.1f);
        }
    }
}
