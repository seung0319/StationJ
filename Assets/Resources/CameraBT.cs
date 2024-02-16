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
        string folderPath = Path.Combine(Application.persistentDataPath, "DCIM/Sta");
        string filePath = Path.Combine(folderPath, fileName);

        // ������ ������ ����
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // ��ũ���� ĸó
        ScreenCapture.CaptureScreenshot(filePath);

        // ������ �κ�: ��ũ������ �Ϸ�� ������ ���
        yield return new WaitUntil(() => File.Exists(filePath));

        Debug.Log("Screenshot captured at: " + filePath);

        // ������ �κ�: �ȵ���̵忡���� �̵�� ��ĵ�� ���� �������� ������ �ݿ�
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass mediaScanner = new AndroidJavaClass("android.media.MediaScannerConnection");
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            // ������ �κ�: filePath�� ����
            mediaScanner.CallStatic("scanFile", new object[] { currentActivity, new string[] { filePath }, null, null });
        }
    }
    //��������������������������������������������������������������������������������������������
}
