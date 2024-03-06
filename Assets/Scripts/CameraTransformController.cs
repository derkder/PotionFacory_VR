using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//暴力控制这个癫子摄像机
public class CameraTransformController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position.y);
        if(transform.position.y < 0.2f)
        {
            Debug.Log("Camera Offset Descending" + transform.position.y);
            Vector3 newPosition = transform.position;
            newPosition.y += 0.8f;
            transform.position = newPosition;
        }
    }
}
