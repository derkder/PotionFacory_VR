using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Dropper : MonoBehaviour
{
    //这里要换成被抓了而不是单纯的gripped
    public XRGrabInteractable interactable;
    [SerializeField]
    private InputActionAsset _actionAsset;
    private InputAction _selectAction;
    private InputAction _triggerAction;
    private bool _isGripped;
    private bool _isTriggered;
    private bool _hasTrggered;
    private bool _hasPotion;

    // Start is called before the first frame update
    void Start()
    {
        //_selectAction = _actionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Select");
        _triggerAction = _actionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Activate");

        interactable = GetComponent<XRGrabInteractable>();
        interactable.firstSelectEntered.AddListener(_ => _isGripped = true);
        interactable.lastSelectExited.AddListener(_ => _isGripped = false);

        _triggerAction.performed += _ => _isTriggered = true;
        _triggerAction.canceled += _ => _isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isGripped && _isTriggered && !_hasTrggered)
        {
            _hasTrggered = true;
        }
        else if(_isGripped && !_isTriggered && _hasTrggered)
        {
            _hasTrggered = false;
            Debug.Log("xishui");
            _hasPotion = true;
        }
        else if(_isGripped && _isTriggered && _hasPotion)
        {
            _hasPotion = false;
            _hasTrggered = false;
            Debug.Log("fangshui");
        }
    }

    void OnPickedUp(SelectEnterEventArgs ev)
    {

    }
}
