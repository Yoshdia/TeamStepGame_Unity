using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insideCasolMoving : MonoBehaviour
{

    Vector3 firstPos;
    // Start is called before the first frame update
    void Start()
    {
        firstPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = (Input.mousePosition);
        mouse.z = 2.0f;
        Vector3 nowMousePos = Camera.main.ScreenToWorldPoint(mouse) ;
        Debug.Log(""+nowMousePos);
        
        Vector3 sub = nowMousePos- firstPos;
        //sub.z = 0;
        Vector3 nor= Vector3.Normalize(sub);
        nor.x *= 0.1f;
        nor.y *= 0.1f;
        transform.position = firstPos + nor;
    }
}
