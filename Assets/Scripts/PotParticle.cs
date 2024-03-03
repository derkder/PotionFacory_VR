using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Spawning;
using Ubiq.Messaging;

public class PotParticle : MonoBehaviour, INetworkSpawnable
{
    public NetworkId NetworkId { get; set; }
    NetworkContext context;
    void Start()
    {
        context = NetworkScene.Register(this);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
    }
}
