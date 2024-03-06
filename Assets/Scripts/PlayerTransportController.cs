using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerTransportController : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset _actionAsset;
    private InputAction _buttonXPressed;
    private InputAction _buttonYPressed;

    [SerializeField]
    private GameObject _roomSpawnPoint;
    [SerializeField]
    private GameObject _forestSpawnPoint;

    private void Awake()
    {
        _buttonXPressed = _actionAsset.FindActionMap("XRI LeftHand").FindAction("XButton");
        _buttonYPressed = _actionAsset.FindActionMap("XRI LeftHand").FindAction("YButton");

        _buttonXPressed.performed += OnXButtonPressed;
        _buttonYPressed.performed += OnYButtonPressed;
    }

    private void OnXButtonPressed(InputAction.CallbackContext context)
    {
        transform.position = _roomSpawnPoint.transform.position;
    }

    private void OnYButtonPressed(InputAction.CallbackContext context)
    {
        // Y按钮被按下时要执行的代码
        transform.position = _forestSpawnPoint.transform.position;
    }

    private void OnDestroy()
    {
        // 清理，避免内存泄漏
        _buttonXPressed.performed -= OnXButtonPressed;
        _buttonYPressed.performed -= OnYButtonPressed;
    }
}
