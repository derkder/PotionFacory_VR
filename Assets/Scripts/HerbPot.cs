using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Spawning;
using static SIPSorcery.Net.Mjpeg;

public class HerbPot : MonoBehaviour
{
    public GameObject CollectPot;
    public List<int> counts; 
    public List<GameObject> HerbList;
    public Transform SpawnPoint;

    private CollectPot _collectPotScript;
    private NetworkSpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.TransportToRoom += TransformHerbs;
        _collectPotScript = CollectPot.GetComponent<CollectPot>();
        _spawnManager = NetworkSpawnManager.Find(this);
        //if (counts.Count != HerbList.Count)
        //{
        //    counts = new List<int>(new int[HerbList.Count]);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)) 
        {
            TransformHerbs();
        }
    }

    private void TransformHerbs()
    {
        Debug.Log("TransformHerbs()");
        counts = _collectPotScript.GetHerbsCount();
        //for (int i = 0; i < counts.Count; i++)
        //{
        //    GameObject prefab = HerbList[i];
        //    int quantity = counts[i];
        //    Debug.Log($"Herb{i}quantity{quantity}");

        //    for (int j = 0; j < quantity; j++)
        //    {
        //        var go = _spawnManager.SpawnWithPeerScope(prefab);
        //        //var potParticle = go.GetComponent<PotParticle>();
        //        //GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);
        //        go.transform.position = SpawnPoint.position;
        //        Rigidbody rb = go.GetComponent<Rigidbody>();
        //        if (rb != null)
        //        {
        //            rb.isKinematic = false;
        //            rb.useGravity = true;   
        //        }
        //    }
        //}

    }
}
