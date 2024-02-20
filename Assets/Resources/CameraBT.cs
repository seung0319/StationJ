using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.Android;

public class CameraBT : MonoBehaviour
{
    public Button screenshotButton;
    public Image screenshotImage;
    public Button saveButton;
    public Button exisButton;
    public GameObject ShowPhotopanel;
    public GameObject Capturepanel;
    private Texture2D capturedTexture;
    public int captureWidth = 1080; // ĸó�� �̹��� ���� ũ��
    public int captureHeight = 1080; // ĸó�� �̹��� ���� ũ��

    private void Start()
    {
        // ���� ��û
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);

        }

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }

        screenshotButton.onClick.AddListener(TakeScreenshot);
        //saveButton.onClick.AddListener(SaveScreenshot);
        //exisButton.onClick.AddListener(HidePanel);
        ShowPhotopanel.SetActive(false);
    }

    public void TakeScreenshot()
    {
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        // ������ ���
        yield return new WaitForEndOfFrame();

        RectTransform CapturepanelRectTransform = Capturepanel.GetComponent<RectTransform>();
        Vector3 CapturepanelPosition = CapturepanelRectTransform.position;
        Vector2 panelSize = CapturepanelRectTransform.sizeDelta;

        Rect captureRect = new Rect(CapturepanelPosition.x, CapturepanelPosition.y, panelSize.x, panelSize.y);

        capturedTexture = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        capturedTexture.ReadPixels(captureRect, 0, 0);
        capturedTexture.Apply();

        // �̹��� ǥ��
        screenshotImage.sprite = Sprite.Create(capturedTexture, new Rect(0, 0, capturedTexture.width, capturedTexture.height), Vector2.zero);
        ShowPhotopanel.SetActive(true);
        Capturepanel.SetActive(false);
    }

    int screenshotCount;
    public void SaveScreenshot()
    {
        if (capturedTexture != null)
        {
            // ���� �ؽ�ó�� �̹��� ���Ϸ� ����
            byte[] bytes = capturedTexture.EncodeToPNG();
            string fileName = "screenshot.png";
            string folderPath = Path.Combine(Application.persistentDataPath, "DCIM/Sta");
            string filePath = Path.Combine(folderPath, fileName);

            // ������ ������ ����
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Save the screenshot to Gallery/Photos
            string name = string.Format("{0}_Capture{1}_{2}.png", Application.productName, "{0}", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
            Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(capturedTexture, Application.productName + " Captures", name));
        }
    }

    public void HidePanel()
    {
        ShowPhotopanel.SetActive(false);
    }
}
