using System.Collections;
using System.Collections.Generic;
using Ubiq.Spawning;
using Ubiq.Messaging;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BasicHerb : MonoBehaviour, INetworkSpawnable
{
    XRGrabInteractable interactable;
    public NetworkId NetworkId { get; set; }
    NetworkContext context;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        interactable.lastSelectExited.AddListener(OnDropped);
    }

    void OnDropped(SelectExitEventArgs ev)
    {
        Debug.Log("Dropped");
        //transform.parent = parent;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
    }
}
