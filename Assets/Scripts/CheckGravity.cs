using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class checkGravity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
         
        if(rb != null) {
            rb.useGravity=true;
            rb.isKinematic=false;
        }
    }
    // void  Awake() {
    //     Rigidbody rb = GetComponent<Rigidbody>();
         
    //     if(rb != null) {
    //         rb.useGravity=true;
    //         rb.isKinematic=false;
    //     }
    // }
}
