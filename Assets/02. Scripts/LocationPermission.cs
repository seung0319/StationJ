using System;
using System.Collections;
using System.Collections.Generic;
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
                //��� O
                //���� ����ġ ����
            }
            else
            {
                //��� X
                //�������� ��ġ ����
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
                //��� O
                routeFindPanel.SetActive(true);
            }
            else
            {
                //��� X
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

    string[] permissions = { Permission.ExternalStorageWrite, Permission.ExternalStorageRead, Permission.Camera };

    public void CamaraUseAllow(string NextScene)
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) ||
            !Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead) ||
            !Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermissions(permissions);

            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) ||
            !Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead) ||
            !Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                //��� X
                return;
            }
            else
            {
                //��� O
                SceneManager.LoadScene(NextScene);
            }
        }
        else
        {
            SceneManager.LoadScene(NextScene);
        }
    }

    //void CamaraUse2(string NextScene)
    //{
    //    if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
    //    {
    //        Permission.RequestUserPermission(Permission.Camera);
    //        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
    //        {
    //            return;
    //        }
    //        else
    //        {
    //            SceneManager.LoadScene(NextScene);
    //        }
    //    }
    //    else
    //    {
    //        SceneManager.LoadScene(NextScene);
    //    }
    //}
}
