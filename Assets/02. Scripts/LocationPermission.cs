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
        // MapScreen �� ���� �� ��ġ��û
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
        // ������ �� �׼�
        locationUpdater.SetActive(true);
        playerMarker.SetActive(true);
    }
    void LocationInfoNotAllow()
    {
        // �ź� �� �ƹ��͵� ����
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
            // OpenSettings ���� UI ����
            AndroidRuntimePermissions.OpenSettings();
        }
    }
}
