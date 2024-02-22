using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PermissionRequester : MonoBehaviour
{
    string[] permissions = new[] { "android.permission.CAMERA", "android.permission.WRITE_EXTERNAL_STORAGE" };

    public void CamaraUseAllow(string NextScene)
    {
        RequestPermission(NextScene);
    }

    public async void RequestPermission(string NextScene)
    {
        AndroidRuntimePermissions.Permission[] result = await AndroidRuntimePermissions.RequestPermissionsAsync(permissions);
        if (result.Contains(AndroidRuntimePermissions.Permission.Denied))
        {
            AndroidRuntimePermissions.OpenSettings();
            return;
        }
        SceneManager.LoadScene(NextScene);
    }
}
