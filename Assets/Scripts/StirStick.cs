using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StirStick : MonoBehaviour
{
    // 从Inspector中分配InputActionAsset
    [SerializeField] 
    private InputActionAsset actionAsset;
    [SerializeField]
    private GameObject potParticle;

    private InputAction selectAction;
    private Vector3 lastPosition;
    private bool isGripped;
    private float timeStart = 0;
    private float accumulateTime = 0;

    private void Awake()
    {
        selectAction = actionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Select");
        if (selectAction == null)
        {
            Debug.LogError("Select action not found.");
            return;
        }

        selectAction.performed += _ => isGripped = true;
        selectAction.canceled += _ => isGripped = false;
    }

    private void Update()
    {
        if (isGripped)
        {
            Vector3 currentPosition = transform.position;

            if (Vector3.Distance(currentPosition, lastPosition) > 0.1f
                    && this.transform.rotation.x >= -50 && this.transform.rotation.x <= 50
                    && this.transform.rotation.z >= -50 && this.transform.rotation.z <= 50
                        && Vector3.Distance(this.transform.position, potParticle.transform.position) < 2) // 检查手柄是否有足够的移动
            {
                isStiring();
            }
            else
            {
                stopStiring();
            }
            // 更新最后的位置
            lastPosition = currentPosition;
        }
        else
        {
            stopStiring();
        }
    }

    private void isStiring()
    {
        timeStart = Time.time;
        Debug.Log("isStiring");
        potParticle.SetActive(true);
    }

    private void stopStiring()
    {
        if (Time.time - timeStart > 1)
        {
            Debug.Log("notStiring");
            potParticle.SetActive(false);
        }  
    }

    private void OnEnable()
    {
        selectAction.Enable();
    }

    private void OnDisable()
    {
        selectAction.Disable();
    }
}
