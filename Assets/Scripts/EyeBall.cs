using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//两边的眼球是同步发生爆炸的！所以这里不需要搞同步
//但是实机跑的时候就没有，我吐了
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
        //等待一会儿之后销毁眼球特效
        yield return new WaitForSeconds(1f);
        _particleEffect.SetActive(false);
    }
}