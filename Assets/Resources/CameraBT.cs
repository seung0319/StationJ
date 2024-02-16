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

        // 권한 요청
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

        // 현재 화면을 캡처하여 이미지 파일로 저장
        string fileName = "screenshot.png";
        string folderPath = Path.Combine(Application.persistentDataPath, "DCIM/StationJ/Screenshots");

        // 폴더가 없으면 생성
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
        // 권한 요청
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
        // 프레임 대기
        yield return new WaitForEndOfFrame();

        // 현재 화면을 캡처하여 이미지 파일로 저장
        string fileName = "screenshot.png";
        string folderPath;

        // 안드로이드에서는 외부 저장소 경로가 변경되었습니다.
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

        // 폴더가 없으면 생성
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath, fileName);

        // 스크린샷 캡처
        ScreenCapture.CaptureScreenshot(filePath);

        // 스크린샷이 완료될 때까지 대기
        yield return new WaitUntil(() => File.Exists(filePath));

        Debug.Log("Screenshot captured at: " + filePath);

        // 안드로이드에서는 미디어 스캔을 통해 갤러리에 사진을 반영
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
        // 권한 요청 (수정된 부분)
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
        // 프레임 대기
        yield return new WaitForEndOfFrame();

        // 현재 화면을 캡처하여 이미지 파일로 저장
        string fileName = "screenshot.png";
        string folderPath = Path.Combine(Application.persistentDataPath, "DCIM/Sta");
        string filePath = Path.Combine(folderPath, fileName);

        // 폴더가 없으면 생성
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // 스크린샷 캡처
        ScreenCapture.CaptureScreenshot(filePath);

        // 수정된 부분: 스크린샷이 완료될 때까지 대기
        yield return new WaitUntil(() => File.Exists(filePath));

        Debug.Log("Screenshot captured at: " + filePath);

        // 수정된 부분: 안드로이드에서는 미디어 스캔을 통해 갤러리에 사진을 반영
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass mediaScanner = new AndroidJavaClass("android.media.MediaScannerConnection");
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            // 수정된 부분: filePath를 전달
            mediaScanner.CallStatic("scanFile", new object[] { currentActivity, new string[] { filePath }, null, null });
        }
    }
    //제발제발제발제발제발제발제발제발제발제발제발제발제발제발제발제발제발제발제발제발제발제발제발
}
