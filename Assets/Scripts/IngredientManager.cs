using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static SIPSorcery.Net.Mjpeg;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

//herb数量不需要同步，药水也不需要，两边的药水有旋转就会给这玩意儿加加
//需要同步的: 有没有被搅动， 干脆别同步了这里，到时候做的人拿到药水，生成一个两边有的就可以了
public class IngredientManager : MonoBehaviour
{
    public List<string> PotionName = new List<string>();
    public List<string> FragName = new List<string>();
    public List<int> PotionCount = new List<int>();
    public List<int> FragCount = new List<int>();
    public List<bool> PotionGenerated = new List<bool>(3);
    public float Radius = 2.7f;

    [SerializeField]
    private ActionBasedController _rightController;
    public GameObject StirStick;
    private StirStick ss;
    private float stirTime;


    void Start()
    {
        PotionName.Add("PotionR");
        PotionName.Add("PotionG");
        PotionName.Add("PotionB");
        FragName.Add("fragA");
        FragName.Add("fragB");
        FragName.Add("fragC");
        ss = StirStick.GetComponent<StirStick>();
        ss.OnPotionMade += GeneratePotion;
        PotionCount = new List<int>(new int[PotionName.Count]);
        FragCount = new List<int>(new int[FragName.Count]);
    }

    // Update is called once per frame
    void Update()
    {
        if (ss.IsStiring)
        {
            stirTime += Time.deltaTime;
        }
        if(stirTime > 1.0f)
        {
            stirTime = 0;
            if (_rightController)
            {
                if (_rightController.SendHapticImpulse(0.5f, 1))
                {
                    print("GeneratePotion");
                }
            }
        }
    }



    public void PotionNumUpdate()
    {
        // 使用Physics.OverlapSphere获取指定半径内的所有碰撞体
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);

        foreach (var hitCollider in hitColliders)
        {
            for (int i = 0; i < PotionName.Count; i++)
            {
                // 检查碰撞体的标签是否与列表中的某个标签相匹配
                if (hitCollider.CompareTag(PotionName[i]))
                {
                    // 如果匹配，相应标签的数量加一
                    PotionCount[i]++;
                    break; // 匹配成功后跳出循环
                }
            }
        }
    }

    public void GeneratePotion()
    {
        // 使用Physics.OverlapSphere获取指定半径内的所有碰撞体
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);

        foreach (var hitCollider in hitColliders)
        {
            for (int i = 0; i < PotionName.Count; i++)
            {
                // 检查碰撞体的标签是否与列表中的某个标签相匹配
                if (hitCollider.CompareTag(FragName[i]))
                {
                    // 如果匹配，相应标签的数量加一
                    FragCount[i]++;
                    break; // 匹配成功后跳出循环
                }
            }
        }

        if (FragCount[0] >=1 && PotionCount[0] >= 1)
        {
            PotionGenerated[0] = true;
            
        }
        
    }
}
