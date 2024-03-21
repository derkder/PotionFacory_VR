using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class IngredientManager : MonoBehaviour
{
    public List<string> PotionName = new List<string>();
    public List<string> FragName = new List<string>();
    public List<int> PotionCount = new List<int>();
    public List<int> FragCount = new List<int>();
    public List<bool> PotionGenerated = new List<bool>();
    private float Radius = 1.5f;
    public bool HasGened = false;

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
        if (stirTime > 1.0f)
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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);

        foreach (var hitCollider in hitColliders)
        {
            for (int i = 0; i < PotionName.Count; i++)
            {
                if (hitCollider.CompareTag(PotionName[i]))
                {
                    PotionCount[i]++;
                    break;
                }
            }
        }
    }

  
    public void GeneratePotion()
    {
        if (HasGened) return;
        Debug.Log("GeneratePotion()");


        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);

        //foreach (var hitCollider in hitColliders)
        //{
        //    for (int i = 0; i < FragName.Count; i++)
        //    {
        //        if (hitCollider.CompareTag(FragName[i]))
        //        {
        //            FragCount[i]++;
        //            Debug.Log($"i{i} : FragName[i]");
        //            Destroy(hitCollider.gameObject);
        //            break; 
        //        }
        //    }
        //}

        HasGened = true;

        if (FragCount[0] >= 1 && PotionCount[0] >= 1)
        {
            PotionGenerated[0] = true;
            FragCount = FragCount.Select(i => 0).ToList();
            PotionCount = PotionCount.Select(i => 0).ToList();
        }
        else if (FragCount[1] >= 1 && PotionCount[1] >= 1)
        {
            PotionGenerated[1] = true;
            FragCount = FragCount.Select(i => 0).ToList();
            PotionCount = PotionCount.Select(i => 0).ToList();
        }
        else if (FragCount[2] >= 1 && PotionCount[2] >= 1)
        {
            PotionGenerated[2] = true;
            FragCount = FragCount.Select(i => 0).ToList();
            PotionCount = PotionCount.Select(i => 0).ToList();
        }
        else
        {
            HasGened = false;
        }

    }
}