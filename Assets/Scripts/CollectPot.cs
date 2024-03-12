using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;

public class CollectPot : MonoBehaviour
{
    //这里不用传消息吧，两边的所有属性本来就是同步的
    public float radius = 2.7f; // 检测的半径
    public List<string> tags; // 存储草药标签的列表
    public List<int> counts; // 存储对应草药数量的列表

    void Start()
    {
        tags.Add("HerbA");
        tags.Add("HerbB");
        tags.Add("HerbC");

        // 确保数量列表的大小与标签列表相同，并初始化为0
        if (counts.Count != tags.Count)
        {
            counts = new List<int>(new int[tags.Count]);
        }
    }

    private struct Message
    {
        public List<int> herbNums;
        public int token;
    }

    // Update is called once per frame
    void Update()
    {
        // 将所有数量重置为0
        for (int i = 0; i < counts.Count; i++)
        {
            counts[i] = 0;
        }

        // 使用Physics.OverlapSphere获取指定半径内的所有碰撞体
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hitCollider in hitColliders)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                // 检查碰撞体的标签是否与列表中的某个标签相匹配
                if (hitCollider.CompareTag(tags[i]))
                {
                    // 如果匹配，相应标签的数量加一
                    counts[i]++;
                    break; // 匹配成功后跳出循环
                }
            }
        }

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    for (int i = 0; i < tags.Count; i++)
        //    {
        //        Debug.Log($"There are {counts[i]} of herb '{tags[i]}'.");
        //    }
        //}
        
    }
}
