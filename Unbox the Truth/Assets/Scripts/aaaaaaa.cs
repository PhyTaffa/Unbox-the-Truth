using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaaaaaa : MonoBehaviour
{
    // Start is called before the first frame update
    private float move = 0.01f;
    private float originalY;
    void Start()
    {
        originalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * move);

        if (transform.position.y > originalY + 5f)
        {
            transform.position = new Vector2(transform.position.x, originalY);
        }
        
    }
}
