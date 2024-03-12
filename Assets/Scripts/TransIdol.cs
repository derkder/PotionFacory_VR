using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using static SIPSorcery.Net.Mjpeg;

public class TransIdol : MonoBehaviour
{
    public float radius = 3f; // 检测的半径

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
        // 使用Physics.OverlapSphere获取指定半径内的所有碰撞体
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hitCollider in hitColliders)
        {
            // 检查碰撞体的标签是否与列表中的某个标签相匹配
            if (hitCollider.CompareTag("Player"))
            {
                GameManager.Instance.Transport();
            }
        }
    }
}
