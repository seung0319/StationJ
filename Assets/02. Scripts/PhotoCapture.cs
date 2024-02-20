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
            //ĸ�ĵ� ȭ���� Texture2D �� �����Ѵ�
            Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
            Rect captureRect = new Rect(0, 0, Screen.width, Screen.height);
            tex.ReadPixels(captureRect, 0, 0);
            tex.Apply();

            //ĸ�ĵ� ȭ���� PNG ������ byte �迭�� ��ȯ�Ѵ�.
            byte[] bytes = tex.EncodeToPNG();
            Destroy(tex);

            //byte �迭�� PNG ���Ϸ� �����Ѵ�.
            string fileName = "ImageName.png";
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            File.WriteAllBytes(filePath, bytes);
        }
    }

    public void OnVideoStartBtn()
    {
        if(CameraMode.isVideo)
        {


        }




    }




}
