using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Spawning;

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
        if (_collectPotScript != null)
        {
            counts =  _collectPotScript.GetHerbsCount();
        }
        for (int i = 0; i < counts.Count; i++)
        {
            GameObject prefab = HerbList[i];
            int quantity = counts[i];

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
