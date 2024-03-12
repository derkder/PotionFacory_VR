using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerTransportController : MonoBehaviour
{

    [SerializeField]
    private GameObject _roomSpawnPoint;
    [SerializeField]
    private GameObject _forestSpawnPoint;

    private void Start()
    {
        GameManager.Instance.TransportToRoom += () => transform.position = _roomSpawnPoint.transform.position;
        GameManager.Instance.TransportToForest += () => transform.position = _forestSpawnPoint.transform.position;
    }

    private void OnXButtonPressed()
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
        //_buttonXPressed.performed -= OnXButtonPressed;
        //_buttonYPressed.performed -= OnYButtonPressed;
    }
}
