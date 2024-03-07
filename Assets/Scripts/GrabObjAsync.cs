using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabObjAsync : MonoBehaviour
{
    XRGrabInteractable interactable;
    NetworkContext context;
    //Transform parent;

    public int token;
    public bool isOwner;

    //GameObjectһ��ʼ�Ƿ���isKinematic
    private bool _isKinematic;

    void Start()
    {
        //parent = transform.parent;
        interactable = GetComponent<XRGrabInteractable>();
        interactable.firstSelectEntered.AddListener(OnPickedUp);
        interactable.lastSelectExited.AddListener(OnDropped);
        context = NetworkScene.Register(this);
        token = Random.Range(1, 10000);
        _isKinematic = GetComponent<Rigidbody>().isKinematic;
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
        //transform.parent = parent;
        GetComponent<Rigidbody>().isKinematic = _isKinematic;

    }

    private struct Message
    {
        public Vector3 position;
        public int token;
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
        var message = m.FromJson<Message>();
        transform.position = message.position;
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