using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocationPermission : MonoBehaviour
{
    public GameObject markerPanel;
    public GameObject routeFindPanel;
    public GameObject categoryPanel;
    public GameObject infoPanel;
    public GameObject routeManager;
    public GameObject locationUpdater;
    public GameObject playerMarker;

    public Text debugger;
    public Text debugger2;

    // Start is called before the first frame update
    void Start()
    {
        // MapScreen 씬 시작 시 위치요청
        RequestPermission();
    }

    public async void RequestPermission()
    {
        AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.ACCESS_FINE_LOCATION");
        if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            LocationInfoAllow();
        }
        else
        {
            LocationInfoNotAllow();
        }
    }
    void LocationInfoAllow()
    {
        // 허용됐을 시 액션
        locationUpdater.SetActive(true);
        playerMarker.SetActive(true);
    }
    void LocationInfoNotAllow()
    {
        // 거부 시 아무것도 안함
    }

    public void RouteButtonClick()
    {
        RequestPermission2();
    }
    public async void RequestPermission2()
    {
        AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.ACCESS_FINE_LOCATION");
        if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            markerPanel.SetActive(false);
            categoryPanel.SetActive(false);
            infoPanel.SetActive(false);
            routeManager.SetActive(true);
            routeFindPanel.SetActive(true);
        }
        else
        {
            // OpenSettings 전에 UI 띄우기
            AndroidRuntimePermissions.OpenSettings();
        }
    }
}
