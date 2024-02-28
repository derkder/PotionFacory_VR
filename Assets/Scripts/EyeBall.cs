using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EyeBall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _particleEffect;
    [SerializeField]
    private GameObject _model;
    [SerializeField]
    private ActionBasedController _rightController;

    void Start()
    {
        if (_particleEffect)
        {
            _particleEffect.SetActive(false);
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
            if (_rightController)
            {
                if(_rightController.SendHapticImpulse(0.5f, 1))
                {
                    print("success");
                }
            }
            _model.SetActive(false);
            _particleEffect.SetActive(true);
        }
    }

}
