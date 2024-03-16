using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//herb数量不需要同步，药水也不需要，两边的药水有旋转就会给这玩意儿加加
//需要同步的: 有没有被搅动， 干脆别同步了这里，到时候做的人拿到药水，生成一个两边有的就可以了
public class IngredientManager : MonoBehaviour
{
    public List<string> PotionName = new List<string>();
    public List<string> FragName = new List<string>();
    public List<int> PotionCount = new List<int>();
    public List<int> FragCount = new List<int>();
    public List<bool> PotionGenerated = new List<bool>();
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
        PotionGenerated = new List<bool>(new bool[FragName.Count]);
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
                    //这里后续改成同步destroy
                    Destroy(hitCollider.gameObject);
                    break; // 匹配成功后跳出循环
                }
            }
        }

        if (FragCount[0] >=1 && PotionCount[0] >= 1)
        {
            PotionGenerated[0] = true;
            // 假设list是你的整型列表
            FragCount = FragCount.Select(i => 0).ToList();
            PotionCount = PotionCount.Select(i => 0).ToList();
        }
        else if(FragCount[1] >= 1 && PotionCount[1] >= 1)
        {
            PotionGenerated[1] = true;
            // 假设list是你的整型列表
            FragCount = FragCount.Select(i => 0).ToList();
            PotionCount = PotionCount.Select(i => 0).ToList();
        }
        else if(FragCount[2] >= 1 && PotionCount[2] >= 1)
        {
            PotionGenerated[2] = true;
            // 假设list是你的整型列表
            FragCount = FragCount.Select(i => 0).ToList();
            PotionCount = PotionCount.Select(i => 0).ToList();
        }
        
    }
}
