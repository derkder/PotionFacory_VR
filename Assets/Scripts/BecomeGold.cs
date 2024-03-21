using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomeGold : MonoBehaviour
{
    private Renderer _renderer;
    private Color _currentColor;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        
        Color originTint = new Color(0.5f, 0.5f, 0.5f, 1f);
        _currentColor = originTint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ("Magic" == collision.gameObject.tag)
        {

            // lerp to gold
            Color yellowTint = new Color(
                Mathf.Clamp(_currentColor.r + 0.2f, 0, 1),
                Mathf.Clamp(_currentColor.g + 0.2f, 0, 1), 
                Mathf.Clamp(_currentColor.b - 0.2f, 0, 1), 
                _currentColor.a
            );

            _renderer.material.color = yellowTint;
            Destroy(collision.gameObject);
            _currentColor = _renderer.material.color;
        }
    }
}
