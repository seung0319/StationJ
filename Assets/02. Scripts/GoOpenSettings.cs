using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoOpenSettings : MonoBehaviour
{
    public void OpenSetting()
    {
        AndroidRuntimePermissions.OpenSettings();
    }
}
