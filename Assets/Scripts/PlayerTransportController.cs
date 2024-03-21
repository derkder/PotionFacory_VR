using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerTransportController : MonoBehaviour
{

    [SerializeField]
    private Transform _roomSpawnPoint;
    [SerializeField]
    private Transform _forestSpawnPoint;

    private void Start()
    {
        GameManager.Instance.TransportToRoom += () => transform.position = _roomSpawnPoint.transform.position;
        GameManager.Instance.TransportToForest += () => transform.position = _forestSpawnPoint.transform.position;
    }


    private void OnDestroy()
    {
        //_buttonXPressed.performed -= OnXButtonPressed;
        //_buttonYPressed.performed -= OnYButtonPressed;
    }
}
