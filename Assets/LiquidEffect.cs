using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer rend;
    private Material material;
    private float coordY;

    private void Awake()
    {
        coordY = transform.position.y;
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        material = rend.material;
        material.SetFloat("_WorldCoordY", coordY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
