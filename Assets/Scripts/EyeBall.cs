using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EyeBall : MonoBehaviour
{
    public bool HasExploded;

    // Start is called before the first frame update
    [SerializeField]
    private GameObject _particleEffect;
    [SerializeField]
    private GameObject _model;
    [SerializeField]
    private GameObject _fire;
    [SerializeField]
    private ActionBasedController _rightController;


    void Start()
    {
        _particleEffect.SetActive(false);
        _fire.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        HasExploded = true;
        if (other.transform.CompareTag("wand"))
        {
            if (_rightController)
            {
                AudioManager.Instance.PlaySFX(SfxType.Expolode);
                if (_rightController.SendHapticImpulse(0.5f, 1))
                {
                    print("success");
                }
            }
            _model.SetActive(false);
            _particleEffect.SetActive(true);
            _fire.SetActive(true);
            StartCoroutine(WaitAndDestroy());
        }
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(1f);
        _particleEffect.SetActive(false);
    }
}