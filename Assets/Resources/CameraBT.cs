using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.Android;

public class CameraBT : MonoBehaviour
{

    /*public Button screenshotButton;

    private void Start()
    {
        screenshotButton.onClick.AddListener(TakeScreenshot);

        // ���� ��û
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
    }

    public void TakeScreenshot()
    {
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        yield return new WaitForEndOfFrame();

        // ���� ȭ���� ĸó�Ͽ� �̹��� ���Ϸ� ����
        string fileName = "screenshot.png";
        string folderPath = Path.Combine(Application.persistentDataPath, "DCIM/StationJ/Screenshots");

        // ������ ������ ����
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath, fileName);
        ScreenCapture.CaptureScreenshot(filePath);

        Debug.Log("Screenshot captured at: " + filePath);
    }*/

    /*public Button screenshotButton;

    private void Start()
    {
        // ���� ��û
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) || !Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }

        screenshotButton.onClick.AddListener(TakeScreenshot);
    }

    public void TakeScreenshot()
    {
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        // ������ ���
        yield return new WaitForEndOfFrame();

        // ���� ȭ���� ĸó�Ͽ� �̹��� ���Ϸ� ����
        string fileName = "screenshot.png";
        string folderPath;

        // �ȵ���̵忡���� �ܺ� ����� ��ΰ� ����Ǿ����ϴ�.
        if (Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass environment = new AndroidJavaClass("android.os.Environment"))
            {
                using (AndroidJavaObject externalStorageDirectory = environment.CallStatic<AndroidJavaObject>("getExternalStorageDirectory"))
                {
                    string externalStoragePath = externalStorageDirectory.Call<string>("getAbsolutePath");
                    folderPath = Path.Combine(externalStoragePath, "DCIM/StationJ/Screenshots");
                }
            }
        }
        else
        {
            folderPath = Path.Combine(Application.persistentDataPath, "DCIM/StationJ/Screenshots");
        }

        // ������ ������ ����
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath, fileName);

        // ��ũ���� ĸó
        ScreenCapture.CaptureScreenshot(filePath);

        // ��ũ������ �Ϸ�� ������ ���
        yield return new WaitUntil(() => File.Exists(filePath));

        Debug.Log("Screenshot captured at: " + filePath);

        // �ȵ���̵忡���� �̵�� ��ĵ�� ���� �������� ������ �ݿ�
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass mediaScanner = new AndroidJavaClass("android.media.MediaScannerConnection");
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            mediaScanner.CallStatic("scanFile", new object[] { currentActivity, new string[] { filePath }, null, null });
        }
    }*/

    public Button screenshotButton;
    public Image screenshotImage;
    public Button saveButton;
    public Button exisButton;
    public GameObject panel;
    public GameObject Capturepanel;
    private Texture2D capturedTexture;
    public int captureWidth = 1080; // ĸó�� �̹��� ���� ũ��
    public int captureHeight = 1080; // ĸó�� �̹��� ���� ũ��

    private void Start()
    {
        // ���� ��û (������ �κ�)
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);

        }

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }

        screenshotButton.onClick.AddListener(TakeScreenshot);
        saveButton.onClick.AddListener(SaveScreenshot);
        exisButton.onClick.AddListener(HidePanel);
        panel.SetActive(false);
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
        panel.SetActive(true);
        Capturepanel.SetActive(false);
    }

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
        panel.SetActive(false);
    }
}
