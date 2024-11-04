
using UnityEngine;

namespace Player
{
    public class CameraManager : MonoBehaviour
    {

        [SerializeField]private float minFOV = 67.0f;
        [SerializeField] private float maxFOV = 120.0f;
        private GameObject playerGO;
        private Transform playerTransform;

        public void Start()
        {
            playerGO = GameObject.FindGameObjectWithTag("Player");
            playerTransform = playerGO.transform;
            
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -10);
        }
        
        void Update () 
        {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -10);
            //transform.position = new Vector3 (player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows 
            //should be an easing
            if (Input.GetKey(KeyCode.B))
            {
                //smooth with a loop, for now
                Camera.main.fieldOfView = maxFOV;
            }
            else
            {
                Camera.main.fieldOfView = minFOV;
            }
        }
    }
}
