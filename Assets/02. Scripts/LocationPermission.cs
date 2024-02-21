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
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            markerPanel.SetActive(false);
            Permission.RequestUserPermission(Permission.FineLocation);
            if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                //허용 O
                //현재 내위치 시작
            }
            else
            {
                //허용 X
                //제물포역 위치 시작
            }
            markerPanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //debugger2.text = locationOK.ToString();
    }

    public void LocationInfoAllow()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                //허용 O
                routeFindPanel.SetActive(true);
            }
            else
            {
                //허용 X
                markerPanel.SetActive(true);
                infoPanel.SetActive(true);
            }
        }
        else
        {
            routeFindPanel.SetActive(true);
        }
    }

    public void PotoZonLocationInfoAllow()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                SceneManager.LoadScene("MapScreen");
            }
        }
        else
        {
            SceneManager.LoadScene("MapScreen");
        }
    }

    

    public void CamaraUseAllow(string NextScene)
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) ||
            !Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead) ||
            !Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            StartCoroutine("PermissionAllow",NextScene);
        }
        else
        {
            SceneManager.LoadScene(NextScene);
        }
    }

    IEnumerator PermissionAllow(string NextScene)
    {
        string[] permissions = { Permission.ExternalStorageWrite, Permission.ExternalStorageRead };
        Permission.RequestUserPermissions(permissions);

        float timer = 0f;
        float timeout = 5f; // 타임아웃 시간 설정
        while ((!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) ||
            !Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead)) && timer < timeout)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) ||
            !Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            yield break;
        }

        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            SceneManager.LoadScene(NextScene);
            yield break;
        }

        Permission.RequestUserPermission(Permission.Camera);

        timer = 0f;
        timeout = 5f;
        while (!Permission.HasUserAuthorizedPermission(Permission.Camera) && timer < timeout)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            SceneManager.LoadScene(NextScene);
        }
    }
}
