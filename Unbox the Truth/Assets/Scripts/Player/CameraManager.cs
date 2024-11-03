
using UnityEngine;

namespace Player
{
    public class CameraManager : MonoBehaviour
    {

        private float minFOV = 67.0f;
        private float maxFOV = 90.0f;
        
        void Update () 
        {
            //transform.position = new Vector3 (player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows 
            
            if (Input.GetKey(KeyCode.B))
            {
                Camera.main.fieldOfView = maxFOV;
            }
            else
            {
                Camera.main.fieldOfView = minFOV;
            }
        }
    }
}
