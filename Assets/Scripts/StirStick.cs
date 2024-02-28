using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StirStick : MonoBehaviour
{
    // 从Inspector中分配InputActionAsset
    [SerializeField] 
    private InputActionAsset _actionAsset;
    [SerializeField]
    private GameObject _potParticle;

    private InputAction _selectAction;
    private Vector3 _lastPosition;
    private bool _isGripped;
    private float _timeStart = 0;
    private float _accumulateTime = 0;

    private void Awake()
    {
        _selectAction = _actionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Select");
        if (_selectAction == null)
        {
            Debug.LogError("Select action not found.");
            return;
        }

        _selectAction.performed += _ => _isGripped = true;
        _selectAction.canceled += _ => _isGripped = false;
    }

    private void Update()
    {
        if (_isGripped)
        {
            Vector3 currentPosition = transform.position;

            if (Vector3.Distance(currentPosition, _lastPosition) > 0.1f
                    && this.transform.rotation.x >= -50 && this.transform.rotation.x <= 50
                    && this.transform.rotation.z >= -50 && this.transform.rotation.z <= 50
                        && Vector3.Distance(this.transform.position, _potParticle.transform.position) < 2) // 检查手柄是否有足够的移动
            {
                isStiring();
            }
            else
            {
                stopStiring();
            }
            // 更新最后的位置
            _lastPosition = currentPosition;
        }
        else
        {
            stopStiring();
        }
    }

    private void isStiring()
    {
        _timeStart = Time.time;
        Debug.Log("isStiring");
        _potParticle.SetActive(true);
    }

    private void stopStiring()
    {
        if (Time.time - _timeStart > 1)
        {
            Debug.Log("notStiring");
            _potParticle.SetActive(false);
        }  
    }

    private void OnEnable()
    {
        _selectAction.Enable();
    }

    private void OnDisable()
    {
        _selectAction.Disable();
    }
}
