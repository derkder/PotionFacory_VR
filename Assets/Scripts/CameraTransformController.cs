using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//±©Á¦¿ØÖÆÕâ¸öñ²×ÓÉãÏñ»ú
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
        if(Mathf.Abs(transform.position.y - 0.7f) < 0.1f)
        {
            Debug.Log("Camera Offset Descending" + transform.position.y);
            Vector3 newPosition = transform.position;
            newPosition.y = 0.7f;
            transform.position = newPosition;
        }
    }

    
}
