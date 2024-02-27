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
        RequestPermission(NextScene);
    }

    public async void RequestPermission(string NextScene)
    {
        AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.CAMERA");
        if (result == AndroidRuntimePermissions.Permission.Denied)
        {
            AndroidRuntimePermissions.OpenSettings();
            return;
        }
        AndroidRuntimePermissions.Permission result2 = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.WRITE_EXTERNAL_STORAGE");
        if (result2 == AndroidRuntimePermissions.Permission.Denied)
        {
            AndroidRuntimePermissions.OpenSettings();
            return;
        }
        SceneManager.LoadScene(NextScene);
    }
}
