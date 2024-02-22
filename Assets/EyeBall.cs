using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject particleEffect;
    [SerializeField]
    private GameObject model;

    void Start()
    {
        if (particleEffect)
        {
            particleEffect.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("wand"))
        {
            model.SetActive(false);
            particleEffect.SetActive(true);
        }
    }

}
