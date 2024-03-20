using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnHerbFragUpdate : MonoBehaviour
{
    public GameObject CookPot;
    private IngredientManager _ingredientManager;

    void Start()
    {
        _ingredientManager = CookPot.GetComponent<IngredientManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < _ingredientManager.FragName.Count; i++)
        {
            // 检查碰撞体的标签是否与列表中的某个标签相匹配
            if (_ingredientManager.FragName[i] == collision.gameObject.tag)
            {
                // 如果匹配，相应标签的数量加一
                _ingredientManager.FragCount[i]++;
                collision.gameObject.GetComponent<GrabObjSync>().DestroySync();
                break; // 匹配成功后跳出循环
            }
        }
    }
}
