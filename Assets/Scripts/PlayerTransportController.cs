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


    private void OnDestroy()
    {
        // ÇåÀí£¬±ÜÃâÄÚ´æĞ¹Â©
        //_buttonXPressed.performed -= OnXButtonPressed;
        //_buttonYPressed.performed -= OnYButtonPressed;
    }
}
