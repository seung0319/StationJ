using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using System.Data;
using UnityEngine.UI;
using Unity.Collections;
using System.IO;
using UnityEngine.SceneManagement;

public class PhotoCapture : MonoBehaviour
{
    [Header("버튼 이미지")]
    [SerializeField] Image shotImage;
    [SerializeField] Sprite videoStopShot;

    [Header("버튼")]
    [SerializeField] GameObject videoStartBtn;
    [SerializeField] GameObject videoStopBtn;
    [SerializeField] GameObject returnBtn;
    [SerializeField] string sceneName;

    [Header("카메라 영역")]
    [SerializeField] GameObject shotUI;

    Camera cam;

    private void Start()
    {
        videoStartBtn.SetActive(true);
        videoStopBtn.SetActive(false);
        returnBtn.SetActive(true);
        shotUI.SetActive(true);

    }

    public void OnShotBtn()
    {
        StartCoroutine(ScreenShot());
        //" ResultPhoto " 씬으로 이동한다.

        SceneManager.LoadScene("ResultPhoto");
    }

    IEnumerator ScreenShot()
    {
        returnBtn.SetActive(false);
        shotUI.SetActive(false);
        yield return new WaitForEndOfFrame();

        if (CameraMode.isPhoto)
        {



        }
    }
}
