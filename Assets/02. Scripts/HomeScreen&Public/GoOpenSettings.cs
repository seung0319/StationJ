using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 설정 들어갈때 쓰는 클래스
/// </summary>
public class GoOpenSettings : MonoBehaviour
{
    //설정 들어가는 함수
    public void OpenSetting()
    {
        //설정 들어가는 코드
        AndroidRuntimePermissions.OpenSettings();
    }
}
