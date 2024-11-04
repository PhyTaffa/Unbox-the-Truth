
using UnityEngine;

namespace Player
{
    public class CameraManager : MonoBehaviour
    {

        [SerializeField]private float minFOV = 67.0f;
        [SerializeField] private float maxFOV = 120.0f;
        
        void Update () 
        {
            //transform.position = new Vector3 (player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows 
            //should be an easing
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
