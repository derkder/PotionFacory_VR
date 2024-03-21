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
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(HandleGrab);
        grabInteractable.selectExited.AddListener(HandleRelease);
    }

    private void HandleGrab(SelectEnterEventArgs args)
    {
        particleEffect.Play();
        Debug.Log("zhuzhule");
    }

    private void HandleRelease(SelectExitEventArgs args)
    {
        particleEffect.Stop();
        Debug.Log("fangxiale");
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(HandleGrab);
        grabInteractable.selectExited.RemoveListener(HandleRelease);
    }
}
