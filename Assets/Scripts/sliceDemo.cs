using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.XR.Interaction.Toolkit;
using Org.BouncyCastle.Crypto.Engines;
using Ubiq.Spawning;
using Ubiq.Messaging;

public class SliceDemo : MonoBehaviour
{
    //切割速度以及切割点
    Vector3 oldPoint;
    Vector3 velocity;
    
    NetworkContext context;
    private NetworkSpawnManager spawnManager;//管理网络生成的对象
    public Transform spawnPoint;//生成prefab的位置和旋转
    public GameObject[] herb_frag_prefab;
    public GameObject original_herb;
    public static int count=4;
    


    bool isFinished;
   

    void Start()
    {
        spawnManager = NetworkSpawnManager.Find(this);
        //manager.OnSpawned.AddListener(Manager_OnSpawned);
        isFinished=false;
    }

    void Update()
    {   
        //获取切割速度和切割点
        if(transform.position != oldPoint){
            velocity = (transform.position - oldPoint) / Time.deltaTime;
            oldPoint = transform.position;
        }

        
    }
    private Material GetCutMaterial(GameObject objectToCut)
    {
        Renderer renderer = objectToCut.GetComponent<Renderer>();
        return renderer ? renderer.material : null;
    }
    // 计算并返回网格的中心点
    public static Vector3 CalculateMeshCentroid(Mesh mesh)
    {
        // 获取网格的所有顶点
        Vector3[] vertices = mesh.vertices;

        // 用于计算所有顶点坐标和的变量
        Vector3 sumOfVertices = Vector3.zero;

        // 遍历所有顶点，累加它们的坐标
        foreach (Vector3 vertex in vertices)
        {
            sumOfVertices += vertex;
        }

        // 计算顶点坐标的平均值，得到中心点
        Vector3 centroid = sumOfVertices / vertices.Length;

        return centroid;
    }
    private void OnTriggerEnter(Collider other) {
        Material CMaterial = GetCutMaterial(other.gameObject);
        Rigidbody origin_rigidBody=other.gameObject.GetComponent<Rigidbody>();
        if(origin_rigidBody!=null){
            origin_rigidBody.isKinematic=true;
            origin_rigidBody.useGravity=false;
        }

        SlicedHull slicedHull = other.gameObject.Slice(transform.position, new Vector3(velocity.y, velocity.x, velocity.z));
       
        if(slicedHull!=null){
            //isFinished=true;
            
            Debug.Log("切割"+count);
            Debug.Log(count);
            if(count==0){
            Debug.Log("不许切了");
            count--;
            original_herb.SetActive(false);

            //var go = spawnManager.SpawnWithPeerScope(herb_frag_prefab);
            
            // 检查触发器碰撞的游戏对象名称
            switch (other.gameObject.name)
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
            // go.transform.position = spawnPoint.position;
            // go.transform.rotation=spawnPoint.rotation;
            
            }
            if(count>0){
            count--;
            //新生成的碎片是复制出的旋转缩小版的original herb
            GameObject fragment = Instantiate(other.gameObject);
            Debug.Log("copy~"+count);
            fragment.name = "fragment"+count;
            //但是虽然是复制的，不许它有子节点，不许它grabable，不许是碰撞体
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
            Collider fragment_collider=fragment.GetComponent<Collider>();
             if (fragment_collider != null)
            {
                Destroy(fragment_collider);
            }
            //获取rigidbody为碎片添加一下爆炸效果
            //爆炸发生的位置在fragment的位置附近随机偏移
            Rigidbody fragment_rigidBody=fragment.GetComponent<Rigidbody>();
            float randomExplosionForce = Random.Range(1f, 6f);
            Vector3 explosionOffset = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            Vector3 explosionPosition = fragment.transform.position + explosionOffset;
            //Vector3 explosionPosition = CalculateMeshCentroid(other.gameObject.GetComponent<MeshFilter>().mesh);
            fragment_rigidBody.AddExplosionForce(randomExplosionForce, explosionPosition, 10);
            
            //将碎片的大小设置为原来的一半
            fragment.transform.localScale = other.transform.localScale * Random.Range(0.2f, 0.5f);
            //随机旋转一下
            fragment.transform.eulerAngles = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
            other.transform.eulerAngles = -other.transform.eulerAngles;
            //将碎片的父节点设为原来的herb
            fragment.transform.parent = other.transform;
            //original herb也位置发生一些变化吧
            other.transform.localScale *= 0.8f;
            other.transform.position +=new Vector3(Random.Range(-0.01f, 0.03f), Random.Range(-0.01f, 0.02f), Random.Range(-0.01f, 0.01f));
            other.transform.eulerAngles = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
            spawnPoint=other.transform;
            original_herb=other.gameObject;
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

