using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ball : MonoBehaviour
{
    XRGrabInteractable interactable;
    NetworkContext context;
    Transform parent;

    // The token decides who has priority when two messages conflict. The higher
    // one wins.
    // 每帧所有client的球都是同一个token
    public int token;
    // 每帧只有一个client的球是true
    // Does this instance of the Component control the transforms for everyone?
    public bool isOwner;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        interactable = GetComponent<XRGrabInteractable>();
        interactable.firstSelectEntered.AddListener(OnPickedUp);
        interactable.lastSelectExited.AddListener(OnDropped);
        context = NetworkScene.Register(this);
        token = Random.Range(1, 10000);
        isOwner = true; // Start by both exchanging the random tokens to see who wins...
    }

    void OnPickedUp(SelectEnterEventArgs ev)
    {
        Debug.Log("Picked up");
        //所有的client都会有这个球，但是球的位置只由捡起来的人决定：
        //被捡起来了，把isOwner变量设置为真，update的时候看到
        //isOwner是真了就会生成新的message，包括token&位置，作为json广播给所有client的球
        //球的ProcessMessage会处理ProcessMessage得到球现在应该在那个位置
        TakeOwnership();
    }

    void OnDropped(SelectExitEventArgs ev)
    {
        Debug.Log("Dropped");
        transform.parent = parent;
        //运动由引擎控制
        GetComponent<Rigidbody>().isKinematic = false;

    }

    private struct Message
    {
        public Vector3 position;
        public int token;
    }

    // When a Component Instance takes Ownership, that Peer decides the position
    // for everyone, either through the VR Controller or through its local Physics
    // Engine
    void TakeOwnership()
    {
        token++;
        isOwner = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOwner)
        {
            Message m = new Message();
            m.position = this.transform.localPosition;
            m.token = token;
            context.SendJson(m);
        }
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage m)
    {
        var message = m.FromJson<Message>();
        //想试一下这行注释掉了本人还能不能看到移动，但是断连了所以回家
        //试了可以
        transform.localPosition = message.position;
        if(message.token > token)
        {
            isOwner = false;
            token = message.token;
            // 脚本控制移动过
            GetComponent<Rigidbody>().isKinematic = true;
        }
        Debug.Log(gameObject.name + " Updated");
    }
}

// 好了，本天才已经全部理解了
