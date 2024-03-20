using System;
using System.Collections;
using System.Collections.Generic;
using Ubiq.Spawning;
using UnityEngine;
using Ubiq.Messaging;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

//告诉锅和粒子，锅有没有被搅动
//只有搅拌的人的锅可以会有药水
//但是药水生出来之后两个人都可以喝
public class StirStick : MonoBehaviour
{
    public bool IsStiring;
    public GameObject PotParticle;
    public bool isCoroutineRunning; // 用于追踪协程是否正在运行
    public event Action OnPotionMade;

    public int token;
    public bool isOwner;

    // 从Inspector中分配InputActionAsset
    [SerializeField]
    private GameObject _pot;
    [SerializeField] 
    private InputActionAsset _actionAsset;
    private InputAction _selectAction;
    private Vector3 _lastPosition;
    private bool _isGripped;
    private float _timeStart = 0;
    //private float _accumulateTime = 0;
    NetworkContext context;
    private NetworkSpawnManager _spawnManager;
    

    XRGrabInteractable interactable;
    private float _accumulateTime = 0;

    private void Awake()
    {
        //_selectAction = _actionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Select");
        //if (_selectAction == null)
        //{
        //    Debug.LogError("Select action not found.");
        //    return;
        //}

        //_selectAction.performed += _ => _isGripped = true;
        //_selectAction.canceled += _ => _isGripped = false;

        interactable = GetComponent<XRGrabInteractable>();
        interactable.firstSelectEntered.AddListener(_ => _isGripped = true);
        interactable.lastSelectExited.AddListener(_ => _isGripped = false);
    }

    private void Start()
    {
        //parent = transform.parent;
        interactable = GetComponent<XRGrabInteractable>();
        interactable.firstSelectEntered.AddListener(OnPickedUp);
        interactable.lastSelectExited.AddListener(OnDropped);
        context = NetworkScene.Register(this);
        token = UnityEngine.Random.Range(1, 10000);
        isOwner = true;
    }

    private void Update()
    {
        if (IsStiring)
        {
            PotParticle.SetActive(true);
        }
        else
        {
            PotParticle.SetActive(false);
        }


        if (_isGripped)
        {
            Vector3 currentPosition = transform.position;

            if (Vector3.Distance(currentPosition, _lastPosition) > 0.1f
                    && this.transform.rotation.x >= -50 && this.transform.rotation.x <= 50
                    && this.transform.rotation.z >= -50 && this.transform.rotation.z <= 50
                        && Vector3.Distance(this.transform.position, _pot.transform.position) < 3f) // 检查手柄是否有足够的移动
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

        if (isOwner)
        {
            Message m = new Message();
            m.IsStiring = IsStiring;
            m.token = token;
            context.SendJson(m);
        }
    }

    private void isStiring()
    {
        //这个isCoroutineRunning确保了之前的一直扣hp现象不会发生！
        if (!IsStiring && !isCoroutineRunning)
        {
            StartCoroutine(SetBoolFalseAfterTime(0.7f)); // 启动协程，保持布尔值为真1秒
        }
    }

    IEnumerator SetBoolFalseAfterTime(float time)
    {
        IsStiring = true;
        isCoroutineRunning = true; // 标记协程开始运行
        yield return new WaitForSeconds(time); // 等待指定的时间
        IsStiring = false; // 将布尔值设置为假
        isCoroutineRunning = false; // 标记协程结束运行
        _accumulateTime += 0.2f;
        if(_accumulateTime >= 1f)
        {
            _accumulateTime = 0;
            OnPotionMade?.Invoke();
        }
    }

    private void stopStiring()
    {
        if (IsStiring && !isCoroutineRunning)
        {
            IsStiring = false;
        }
    }

    void TakeOwnership()
    {
        token++;
        isOwner = true;
    }

    void OnPickedUp(SelectEnterEventArgs ev)
    {
        Debug.Log("Picked up");
        TakeOwnership();
    }

    void OnDropped(SelectExitEventArgs ev)
    {
        Debug.Log("Dropped");
    }

    private struct Message
    {
        public Vector3 position;
        public int token;
        public bool IsStiring;
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage m)
    {
        var message = m.FromJson<Message>();
        IsStiring = message.IsStiring;
        if (message.token > token)
        {
            isOwner = false;
            token = message.token;
            Debug.Log("WandToken" + token);
            GetComponent<Rigidbody>().isKinematic = true;
        }
        Debug.Log(gameObject.name + " Updated");
    }
}
