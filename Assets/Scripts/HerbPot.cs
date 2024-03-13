using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Spawning;
using static SIPSorcery.Net.Mjpeg;

//只要传送到房间事件发生之后，生成对应数量的草药
public class HerbPot : MonoBehaviour
{
    public GameObject CollectPot;
    public List<int> counts; // 存储对应草药数量的列表
    public List<GameObject> HerbList;
    public Transform SpawnPoint;

    private CollectPot _collectPotScript;
    private NetworkSpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.TransportToRoom += GenerateHerbs;
        _collectPotScript = CollectPot.GetComponent<CollectPot>();
        _spawnManager = NetworkSpawnManager.Find(this);
        // 确保数量列表的大小与标签列表相同，并初始化为0
        if (counts.Count != HerbList.Count)
        {
            counts = new List<int>(new int[HerbList.Count]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)) 
        {
            GenerateHerbs();
        }
    }

    private void GenerateHerbs()
    {
        Debug.Log("GenerateHerbs()");
        counts = _collectPotScript.GetHerbsCount();
        for (int i = 0; i < counts.Count; i++)
        {
            GameObject prefab = HerbList[i];
            int quantity = counts[i];
            Debug.Log($"Herb{i}quantity{quantity}");

            for (int j = 0; j < quantity; j++)
            {
                // 实例化GameObject
                var go = _spawnManager.SpawnWithPeerScope(prefab);
                //var potParticle = go.GetComponent<PotParticle>();
                //GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);
                go.transform.position = SpawnPoint.position;
                // 设置Rigidbody组件的属性
                Rigidbody rb = go.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.useGravity = true;   
                }
            }
        }

    }
}
