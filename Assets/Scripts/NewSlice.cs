using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;
using Ubiq.Spawning;
using Ubiq.Messaging;

public class NewSlice : MonoBehaviour
{
    //切割速度以及切割点
    Vector3 oldPoint;
    Vector3 velocity;

    NetworkContext context;
    private NetworkSpawnManager spawnManager;//管理网络生成的对象
    public Transform spawnPoint;//生成prefab的位置和旋转
    public GameObject[] herbs;
    public GameObject[] herb_frag_prefab;
    public Dictionary<string, int> herbCounts = new Dictionary<string, int>(); // 用于跟踪每种草药的计数

    public GameObject original_herb;
    bool isFinished;

    void Start()
    {
        spawnManager = NetworkSpawnManager.Find(this);
        isFinished = false;

        // 初始化每种草药的计数为 4
        foreach (GameObject herb in herbs)
        {
            herbCounts[herb.name] = 2;
        }
    }

    void Update()
    {
        //获取切割速度和切割点
        if (transform.position != oldPoint)
        {
            velocity = (transform.position - oldPoint) / Time.deltaTime;
            oldPoint = transform.position;
        }
    }

    private Material GetCutMaterial(GameObject objectToCut)
    {
        Renderer renderer = objectToCut.GetComponent<Renderer>();
        return renderer ? renderer.material : null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Material CMaterial = GetCutMaterial(other.gameObject);
        SlicedHull slicedHull = other.gameObject.Slice(transform.position, new Vector3(velocity.y, velocity.x, velocity.z));

        if (slicedHull != null)
        {
            string herbName = other.gameObject.name;
            // && herbCounts[herbName] > 0
            if (herbCounts.ContainsKey(herbName))
            {
                // Debug.Log("切割 " + herbName + ", 剩余数量: " + herbCounts[herbName]);
                // herbCounts[herbName]--;

                if (herbCounts[herbName] == 0)
                {
                    Debug.Log("不许再切了 " + herbName);
                    switch (herbName)
                     {
                case "1":
                    var go1 = spawnManager.SpawnWithPeerScope(herb_frag_prefab[0]);
                    go1.transform.position = spawnPoint.position;
                    go1.transform.rotation=spawnPoint.rotation;
                    Debug.Log("生成了1的prefab");
                    
                    break;
                case "2":
                    var go2 = spawnManager.SpawnWithPeerScope(herb_frag_prefab[1]);
                    go2.transform.position = spawnPoint.position;
                    go2.transform.rotation=spawnPoint.rotation;
                    Debug.Log("生成了2的prefab");
                    break;
                case "3":
                    var go3 = spawnManager.SpawnWithPeerScope(herb_frag_prefab[2]);
                    go3.transform.position = spawnPoint.position;
                    go3.transform.rotation=spawnPoint.rotation;
                    Debug.Log("生成了3的prefab");
                    break;
                default:
                    Debug.Log("No matching case found.");
                    break;
            };
                    Debug.Log("generate");

                    original_herb.SetActive(false);
                }

                // 剩余数量大于 0 时继续生成碎片
                if (herbCounts[herbName] > 0)
                {   
                    Debug.Log("切割 " + herbName + ", 剩余数量: " + herbCounts[herbName]);
                
                    herbCounts[herbName]--; // 独立的计数减少
                    // 新生成的碎片是复制出的旋转缩小版的 original_herb
                    GameObject fragment = Instantiate(other.gameObject);
                    Debug.Log("copy~" + herbName);
                    fragment.name = "fragment" + herbName;
                    // 但是虽然是复制的，不许它有子节点，不许它 grabable，不许是碰撞体
                    foreach (Transform child in fragment.transform)
                    {
                        Destroy(child.gameObject);
                    }
                    XRGrabInteractable fragment_grabInteractable = fragment.GetComponent<XRGrabInteractable>();
                    if (fragment_grabInteractable != null)
                    {
                        Destroy(fragment_grabInteractable);
                    }
                    MeshRenderer fragment_Renderer = fragment.GetComponent<MeshRenderer>();
                    if (fragment_Renderer != null)
                    {
                        fragment_Renderer.material = CMaterial;
                    }
                    Collider fragment_collider = fragment.GetComponent<Collider>();
                    if (fragment_collider != null)
                    {
                        Destroy(fragment_collider);
                    }
                    // 获取 rigidbody 为碎片添加爆炸效果
                    // 爆炸发生的位置在 fragment 的位置附近随机偏移
                    Rigidbody fragment_rigidBody = fragment.GetComponent<Rigidbody>();
                    float randomExplosionForce = Random.Range(1f, 6f);
                    Vector3 explosionOffset = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
                    Vector3 explosionPosition = fragment.transform.position + explosionOffset;
                    fragment_rigidBody.AddExplosionForce(randomExplosionForce, explosionPosition, 10);

                    // 将碎片的大小设置为原来的一半
                    fragment.transform.localScale = other.transform.localScale * Random.Range(0.2f, 0.5f);
                    // 随机旋转一下
                    fragment.transform.eulerAngles = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
                    other.transform.eulerAngles = -other.transform.eulerAngles;
                    // 将碎片的父节点设为原来的 herb
                    fragment.transform.parent = other.transform;
                    // original_herb 也位置发生一些变化
                    other.transform.localScale *= 0.8f;
                    other.transform.position += new Vector3(Random.Range(-0.01f, 0.03f), Random.Range(-0.01f, 0.02f), Random.Range(-0.01f, 0.01f));
                    other.transform.eulerAngles = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
                    spawnPoint = other.transform;
                    original_herb = other.gameObject;
                }
            }
            else
            {
                Debug.Log("无法切割 " + herbName + "，数量不足或者未知草药");
            }
        }
    }

    public void SpawnWithRoomScope(int prefabindex)
    {
        if (spawnManager)
        {
            spawnManager.SpawnWithRoomScope(spawnManager.catalogue.prefabs[prefabindex]);
        }
    }
}
