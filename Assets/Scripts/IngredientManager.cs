using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//herb数量不需要同步
//需要同步的: 有没有被搅动，药水数量，生成了什么药水
public class IngredientManager : MonoBehaviour
{
    public List<int> potionCount = new List<int>(3);
    public List<int> fragCount = new List<int>(3);

    [SerializeField]
    private ActionBasedController _rightController;
    public GameObject StirStick;
    private StirStick ss;
    private float stirTime;
    private bool generatePotion;


    void Start()
    {
        ss = StirStick.GetComponent<StirStick>();
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
}
