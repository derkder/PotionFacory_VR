using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using static SIPSorcery.Net.Mjpeg;

public class TransIdol : MonoBehaviour
{
    public float radius = 3f;

    [SerializeField]
    private InputActionAsset _actionAsset;
    private InputAction _buttonXPressed;

    private void Awake()
    {
        _buttonXPressed = _actionAsset.FindActionMap("XRI LeftHand").FindAction("XButton");
        _buttonXPressed.performed += OnXButtonPressed;
    }

    void Start()
    {

    }



    // Update is called once per frame
    private void OnXButtonPressed(InputAction.CallbackContext context)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                GameManager.Instance.Transport();
            }
        }
    }
}