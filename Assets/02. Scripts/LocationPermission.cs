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
    public GameObject infoPanel;

    public Text debugger;
    public Text debugger2;

    // Start is called before the first frame update
    void Start()
    {
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

    public async void RequestPermission2()
    {
        AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.ACCESS_FINE_LOCATION");
        if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            routeFindPanel.SetActive(true);
        }
        else
        {
            AndroidRuntimePermissions.OpenSettings();
            SceneManager.LoadScene("HomeScreen");
        }
    }

    void LocationInfoAllow()
    {
        debugger.text = "허용";
    }
    void LocationInfoNotAllow()
    {
        debugger.text = "허용안함";
    }
}
