using System.Collections;
using System.Collections.Generic;
using Ubiq.Spawning;
using Ubiq.Messaging;
using UnityEngine;

public class MagicPotion : MonoBehaviour, INetworkSpawnable
{
    public NetworkId NetworkId { get; set; }
    NetworkContext context;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
    }

}
