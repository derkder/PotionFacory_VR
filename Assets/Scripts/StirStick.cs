using System.Collections;
using System.Collections.Generic;
using Ubiq.Spawning;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class StirStick : MonoBehaviour
{
    public GameObject potParticlePrefab;

    // 从Inspector中分配InputActionAsset
    [SerializeField]
    private GameObject _pot;
    [SerializeField] 
    private InputActionAsset _actionAsset;

    private InputAction _selectAction;
    private Vector3 _lastPosition;
    private bool _isGripped;
    private float _timeStart = 0;
    private float _accumulateTime = 0;
    
    private NetworkSpawnManager _spawnManager;

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

    private void Start()
    {
        _spawnManager = NetworkSpawnManager.Find(this);
    }

    private void Update()
    {
        if (_isGripped)
        {
            Vector3 currentPosition = transform.position;

            if (Vector3.Distance(currentPosition, _lastPosition) > 0.1f
                    && this.transform.rotation.x >= -50 && this.transform.rotation.x <= 50
                    && this.transform.rotation.z >= -50 && this.transform.rotation.z <= 50
                        && Vector3.Distance(this.transform.position, _pot.transform.position) < 1f) // 检查手柄是否有足够的移动
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
        //Debug.Log("isStiring");
        //_potParticle.SetActive(true);
        //这里Room不行，Peer可以}
        var go = _spawnManager.SpawnWithPeerScope(potParticlePrefab);
        var potParticle = go.GetComponent<PotParticle>();
        potParticle.transform.position = transform.position;

    }

    private void stopStiring()
    {
        if (Time.time - _timeStart > 1)
        {
            //Debug.Log("notStiring");
            //_potParticle.SetActive(false);
            
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
