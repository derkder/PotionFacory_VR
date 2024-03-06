using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Spawning;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using System;
public class PickHerb : MonoBehaviour
{
    // Start is called before the first frame update
    
    public ParticleSystem particleEffect; // 在Unity编辑器中指定粒子系统

    [SerializeField] 
    private XRGrabInteractable grabInteractable; // XRGrabInteractable组件的引用
    private NetworkSpawnManager _spawnManager;
    
    void Awake()
    {
        _spawnManager = NetworkSpawnManager.Find(this);
        // 获取XRGrabInteractable组件
        grabInteractable = GetComponent<XRGrabInteractable>();
        // 订阅抓取和释放的事件
         // 获取 XRGrabInteractable 组件
        grabInteractable = GetComponent<XRGrabInteractable>();
        // 订阅抓取和释放的新事件
        grabInteractable.selectEntered.AddListener(HandleGrab);
        grabInteractable.selectExited.AddListener(HandleRelease);
    }

    private void HandleGrab(SelectEnterEventArgs args)
    {
        // 激活粒子效果
        particleEffect.Play();
        Debug.Log("zhuzhule");
    }

    private void HandleRelease(SelectExitEventArgs args)
    {
        // 停止粒子效果
        particleEffect.Stop();
        Debug.Log("fangxiale");
    }

    void OnDestroy()
    {
        // 取消订阅事件
        grabInteractable.selectEntered.RemoveListener(HandleGrab);
        grabInteractable.selectExited.RemoveListener(HandleRelease);
    }
}
