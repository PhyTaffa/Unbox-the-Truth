using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumLine : MonoBehaviour
{

    LineRenderer lineRenderer;
    HingeJoint2D hingeJoint2D;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        hingeJoint2D = GetComponent<HingeJoint2D>();
        //Vector3 anchorPos = new Vector3(hingeJoint2D.connectedAnchor.y, hingeJoint2D.connectedAnchor.y, 0);
        

    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, hingeJoint2D.connectedAnchor);
        lineRenderer.SetPosition(1, transform.position);
    }
}
