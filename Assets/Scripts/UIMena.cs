using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIMenu : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 3;
    public GameObject menu;

    [SerializeField]
    private InputActionAsset _actionAsset;
    private InputAction _selectAction;
    private Vector3 relativePosition;

    void Start()
    {
        _selectAction = _actionAsset.FindActionMap("XRI RightHand").FindAction("AButton");
        if (_selectAction == null)
        {
            Debug.LogError("Action not found.");
            return;
        }

        _selectAction.performed += _ => menu.SetActive(!menu.activeSelf);
        relativePosition = new Vector3(0, -0.5f, spawnDistance); // Adjusted relative position
    }

    void Update()
    {
        if (menu.activeSelf)
        {
            // Position the menu at a specific distance in front of the player
            Vector3 desiredPosition = head.position + head.forward.normalized * spawnDistance;
            // Adjust the height relative to the head's position to prevent it from appearing too high
            desiredPosition.y = head.position.y + relativePosition.y;

            menu.transform.position = desiredPosition;

            // Make the menu face the player
            menu.transform.LookAt(head);
            // This last line is to flip the menu to face the correct way if it's backwards
            menu.transform.forward = -menu.transform.forward;
        }
    }
}
