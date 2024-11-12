using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextReposition : MonoBehaviour
{
    [SerializeField] private GameObject levelDoor;
    private Transform levelDoorTransform;

    private Camera camera;
    private Vector3 camPos;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        camPos = camera.transform.position;
        
        levelDoorTransform = levelDoor.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(596, 356, levelDoorTransform.position.z);
        //transform.position = new Vector3(levelDoorTransform.position.x, levelDoorTransform.position.y, levelDoorTransform.position.z);
        //transform.position = camPos;
    }
}
