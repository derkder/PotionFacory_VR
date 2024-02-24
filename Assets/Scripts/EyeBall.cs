using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EyeBall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject particleEffect;
    [SerializeField]
    private GameObject model;
    [SerializeField]
    private ActionBasedController rightController;

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
            if (rightController)
            {
                if(rightController.SendHapticImpulse(0.5f, 1))
                {
                    print("success");
                }
            }
            model.SetActive(false);
            particleEffect.SetActive(true);
        }
    }

}
