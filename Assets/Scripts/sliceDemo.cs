using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;


public class sliceDemo : MonoBehaviour
{
    public Material CMaterial;
    Vector3 oldPoint;
    Vector3 velocity;
    
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position!=oldPoint){
            velocity=(transform.position-oldPoint)/Time.deltaTime;
            oldPoint=transform.position;
        }
        
    }
    private void  OnTriggerEnter(Collider other) {
        //SlicedHull slicedHull=other.gameObject.Slice(transform.position,transform.up);
        SlicedHull slicedHull=other.gameObject.Slice(transform.position,new Vector3(velocity.y,velocity.x,velocity.z));
        if(slicedHull!=null){
            Debug.Log("切割！");
            //切面以material作为材质
            GameObject lower=slicedHull.CreateLowerHull(other.gameObject,CMaterial);
            GameObject upper=slicedHull.CreateUpperHull(other.gameObject,CMaterial);
            //赋予刚体属性
            lower.AddComponent<Rigidbody>();
            lower.AddComponent<MeshCollider>().convex=true;
            lower.GetComponent<Rigidbody>().AddExplosionForce(500,other.gameObject.transform.position,20);
            upper.AddComponent<Rigidbody>();
            upper.AddComponent<MeshCollider>().convex=true;
            upper.GetComponent<Rigidbody>().AddExplosionForce(500,other.gameObject.transform.position,20);
            Destroy(other.gameObject);

        }
        
        
    }
}
