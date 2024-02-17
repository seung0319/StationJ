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
    [Header("��ư �̹���")]
    [SerializeField] Image shotImage;
    [SerializeField] Sprite videoStopShot;

    [Header("��ư")]
    [SerializeField] GameObject videoStartBtn;
    [SerializeField] GameObject videoStopBtn;
    [SerializeField] GameObject returnBtn;
    [SerializeField] string sceneName;

    [Header("ī�޶� ����")]
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
        //" ResultPhoto " ������ �̵��Ѵ�.

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
