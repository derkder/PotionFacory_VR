using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using UnityEngine.XR.Interaction.Toolkit;

//同步烧瓶的位置和液体的量
public class PotionSync : MonoBehaviour
{
    XRGrabInteractable interactable;
    NetworkContext context;
    public int token;
    public bool isOwner;
    LiquidEffect le;


    private struct Message
    {
        public Vector3 position;
        public bool hasPooled;
        public float curAmount;
        public int token;
    }

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        interactable.firstSelectEntered.AddListener(OnPickedUp);
        interactable.lastSelectExited.AddListener(OnDropped);

        context = NetworkScene.Register(this);
        token = Random.Range(1, 10000);
        isOwner = true;
        le = GetComponent<LiquidEffect>();
    }
    void OnPickedUp(SelectEnterEventArgs ev)
    {
        Debug.Log("Picked up");
        TakeOwnership();
    }

    void OnDropped(SelectExitEventArgs ev)
    {
        Debug.Log("Dropped");
        //运动由引擎控制
        GetComponent<Rigidbody>().isKinematic = false;

    }

    void TakeOwnership()
    {
        token++;
        isOwner = true;
    }

    void Update()
    {
        if (isOwner)
        {
            Message m = new Message();
            m.position = this.transform.position;
            m.token = token;
            m.curAmount = le._curAmount;
            context.SendJson(m);
        }
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage m)
    {
        var message = m.FromJson<Message>();
        transform.position = message.position;
        le._curAmount = message.curAmount;
        if (message.token > token)
        {
            isOwner = false;
            token = message.token;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        //Debug.Log(gameObject.name + " Updated");
    }
}
