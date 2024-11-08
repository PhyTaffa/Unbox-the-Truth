using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRotResetter : MonoBehaviour
{
    // Start is called before the first frame update

    WorldRotation  worldRotation;
    void Start()
    {
        GameObject worldRotationGO = GameObject.FindWithTag("WorldRoot");
        worldRotation = worldRotationGO.GetComponent<WorldRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject) //&& direction == Vector3.down
        {
                worldRotation.SetCanRotate(true);
                //collision.transform.parent = transform;
                
        }
    }
}
