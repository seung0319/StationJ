using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���� ���� ���� Ŭ����
/// </summary>
public class GoOpenSettings : MonoBehaviour
{
    //���� ���� �Լ�
    public void OpenSetting()
    {
        //���� ���� �ڵ�
        AndroidRuntimePermissions.OpenSettings();
    }
}
