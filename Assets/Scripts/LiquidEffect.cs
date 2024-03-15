using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 如果再想搞得正确一点儿，_maxAmont这些需要根据物体的rotation做变换
 */
public class LiquidEffect : MonoBehaviour
{
    public float Radius = 5f;
    public bool isPouring;
    public float _curAmount;
    public Transform PotionRecharge;
    public Transform CookPot;
    public IngredientManager IngredientManager;
    
    public event Action HasPoured;


    // Start is called before the first frame update
    private Renderer _rend;
    private Material _material;

    [SerializeField]
    private float _minAmont;
    [SerializeField]
    private float _maxAmont;
    private float _step = 0.001f;


    void Start()
    {
        _rend = GetComponent<Renderer>();
        _material = _rend.material;
        _material.SetFloat("_FillAmount", _minAmont);
        _curAmount = _minAmont;
        IngredientManager = CookPot.GetComponent<IngredientManager>();
        HasPoured += IngredientManager.PotionNumUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        _material.SetFloat("_WorldCoordY", transform.position.y);
        _material.SetFloat("_FillAmount", _curAmount);
        if (Vector3.Distance(PotionRecharge.position, transform.position) < Radius)
        {
            //Debug.Log("asdasdasd");
            _maxAmont -= 0.4f;
            _minAmont -= 0.08f;
            _curAmount = _minAmont;
        }
    }

    private void FixedUpdate()
    {
        Vector3 forwardDirection = transform.up;
        float dotProduct = Vector3.Dot(forwardDirection, Vector3.up);

        // 判断夹角是否超过90°
        if (dotProduct < Mathf.Cos(50 * Mathf.Deg2Rad))
        {
            //下面的判断只会进入一次
            if(Mathf.Abs(_minAmont - _curAmount) <= _step - 0.0005)
            {
                //第一次开始倾倒，因为旋转，所以需要增加偏移
                //其实最好是偏移跟着rotation慢慢lerp到这里0.05f
                //试图修复这里一下子倾倒总会有一下子变满的错误
                Debug.Log("FirstBiggerThan50");
                _maxAmont += 0.4f;
                _minAmont += 0.08f;
                _curAmount = _minAmont + _step;
                if(Vector3.Distance(CookPot.position, this.transform.position) < Radius)
                {
                    HasPoured?.Invoke();
                }
            }
            isPouring = true;
            _curAmount = Mathf.Min(_curAmount + _step, _maxAmont);
            _material.SetFloat("_FillAmount", _curAmount);
            Debug.Log("物体的前方与y轴的夹角超过90度");
        }
        else
        {
            isPouring = false;
        }
    }
}
