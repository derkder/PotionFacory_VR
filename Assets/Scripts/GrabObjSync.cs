using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//能抓取的物体挂载就会实现位置同步
//这里同步被我写成async了，但是改起来实在太麻烦了就先这么滴吧
public class GrabObjSync : MonoBehaviour
{
    XRGrabInteractable interactable;
    NetworkContext context;
    //Transform parent;

    public int token;
    public bool isOwner;

    //GameObject一开始是否开启isKinematic
    private bool _isKinematic;

    void Start()
    {
        //parent = transform.parent;
        interactable = GetComponent<XRGrabInteractable>();
        interactable.firstSelectEntered.AddListener(OnPickedUp);
        interactable.lastSelectExited.AddListener(OnDropped);
        context = NetworkScene.Register(this);
        token = Random.Range(1, 10000);
        isOwner = true;
    }

    //同步destroy所有端的这个物体
    public void DestroySync()
    {
        TakeOwnership();
        Message m = new Message();
        m.position = this.transform.position;
        m.token = token;
        m.getDestroy = true;
        context.SendJson(m);
        Debug.Log("grab sync destroy");
        //isOwner貌似就不接受消息了
        Destroy(gameObject);
    }

    void OnPickedUp(SelectEnterEventArgs ev)
    {
        _isKinematic = GetComponent<Rigidbody>().isKinematic;
        Debug.Log("Picked up");
        TakeOwnership();
    }

    void OnDropped(SelectExitEventArgs ev)
    {
        Debug.Log("Dropped");
        //transform.parent = parent;
        GetComponent<Rigidbody>().isKinematic = _isKinematic;

    }


    private struct Message
    {
        public Vector3 position;
        public int token;
        public bool getDestroy;
    }

    void TakeOwnership()
    {
        token++;
        isOwner = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOwner)
        {
            Message m = new Message();
            m.position = this.transform.position;
            m.token = token;
            context.SendJson(m);
        }
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage m)
    {
        Debug.Log("ProcessMessage");
        var message = m.FromJson<Message>();
        transform.position = message.position;
        if (message.getDestroy)
        {
            Destroy(gameObject);
        }
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