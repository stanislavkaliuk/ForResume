using System;
using Core;
using Managers;
using UnityEngine;

public class TestManager : ManagerBase, IAwake, IDestroy, IUpdate
{
    public void Awake()
    {
        EventsController.AddListener(EventsType.OnApplicationStart, OnStart);
        EventsController.AddListener<bool>(EventsType.OnApplicationFocus, OnFocus);
    }

    public void Destroy()
    {
        EventsController.RemoveListener(EventsType.OnApplicationStart, OnStart);
        EventsController.RemoveListener<bool>(EventsType.OnApplicationFocus, OnFocus);
    }

    private void OnFocus(bool focus)
    {
        Debug.Log("[TEST] OnFocus = "+ focus);
    }

    private void OnStart()
    {
        Debug.Log("[TEST] OnStart");
    }


    public void OnUpdate()
    {
        Debug.Log("[TEST] Update");
    }
}
