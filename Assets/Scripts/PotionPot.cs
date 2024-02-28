using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPot : MonoBehaviour
{
    [SerializeField]
    private GameObject potion_1;
    private LiquidEffect _liquidEffect;

    // Start is called before the first frame update
    private Renderer _rend;
    private Material _material;
    private float _coordY;

    [SerializeField]
    private float _minAmont;
    [SerializeField]
    private float _maxAmont;
    private float _curAmount;
    private float _step = 0.001f;

    private void Awake()
    {
        _curAmount = _maxAmont;
        if (potion_1)
        {
            _liquidEffect = potion_1.GetComponent<LiquidEffect>();
        }
    }

    void Start()
    {
        _rend = GetComponent<Renderer>();
        _material = _rend.material;
        _material.SetFloat("_FillAmount", _maxAmont);
    }

    // Update is called once per frame
    void Update()
    {
        _coordY = transform.position.y;
        _material.SetFloat("_WorldCoordY", _coordY);
    }

    private void FixedUpdate()
    {
        if (_liquidEffect && _liquidEffect.isPouring)
        {
            _curAmount = Mathf.Max(_curAmount - _step, _minAmont);
            _material.SetFloat("_FillAmount", _curAmount);
            Debug.Log("物体正在加满");
        }
    }
}
