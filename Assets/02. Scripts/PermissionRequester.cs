using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PermissionRequester : MonoBehaviour
{
    public void CamaraUseAllow(string NextScene)
    {
        // ī�޶�/����Ʈ, ī�޶�, ����� ��û
        RequestPermission(NextScene);
    }

    public void CamaraUseAllow2(string NextScene)
    {
        // AR�׺���̼�, ī�޶� ��û
        RequestPermission2(NextScene);
    }

    public void CamaraUseAllow3(string NextScene)
    {
        // ī�޶�/����Ʈ, ��ġ ��û
        RequestPermission3(NextScene);
    }

    public async void RequestPermission(string NextScene)
    {
        AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.CAMERA");
        if (result == AndroidRuntimePermissions.Permission.Denied)
        {
            AndroidRuntimePermissions.OpenSettings();
        }
        AndroidRuntimePermissions.Permission result2 = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.WRITE_EXTERNAL_STORAGE");
        if (result2 == AndroidRuntimePermissions.Permission.Denied)
        {
            AndroidRuntimePermissions.OpenSettings();
        }
        if (result == AndroidRuntimePermissions.Permission.Granted && result2 == AndroidRuntimePermissions.Permission.Granted)
        {
            SceneManager.LoadScene(NextScene);
        }
    }
    public async void RequestPermission3(string NextScene)
    {
        AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.CAMERA");
        if (result == AndroidRuntimePermissions.Permission.Denied)
        {
            AndroidRuntimePermissions.OpenSettings();
        }
        if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            SceneManager.LoadScene(NextScene);
        }
    }

    public void LocationUseAllow(string NextScene)
    {
        RequestPermission2(NextScene);
    }

    public async void RequestPermission2(string NextScene)
    {
        AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.ACCESS_FINE_LOCATION");
        if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            SceneManager.LoadScene(NextScene);
        }
        else
        {
            AndroidRuntimePermissions.OpenSettings();
        }
    }
}
