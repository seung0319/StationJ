using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NativeGalleryNamespace;


//Photo, Video 기능 과 버튼 클릭을 통한 Panel 이동과 씬 이동에 관한 코드
// 3개의 패널을 오고 가며 기능 구현

public class CameraPanelManager : MonoBehaviour
{
    // 필드 정의
    public GameObject ChoosePanel; // 사진, 비디오 촬영 선택 패널
    public GameObject ResultPanel; // 촬영 결과 확인 패널
    public GameObject VideoPanel; // 비디오 촬영 패널
    public GameObject SaveCompletePnael; // 저장 완료 패널

    // ChoosePanel 버튼들
    public GameObject PhotoButton; // 사진 촬영 버튼
    public GameObject PhotoPickButton; // 갤러리에서 사진 선택 버튼
    public GameObject VideoButton; // 비디오 촬영 버튼
    public GameObject GalleryButton; // 갤러리 버튼
    public GameObject choosepanelBackButton; // ChoosePanel 뒤로가기 버튼

    // VideoPanel 버튼들
    public GameObject VideoCaptureButton; // 비디오 촬영 시작 버튼
    public GameObject VideoStopButton; // 비디오 촬영 중지 버튼
    public GameObject VideoGalleryutton; // 비디오 갤러리 버튼
    public GameObject VideopanelBackButton; // VideoPanel 뒤로가기 버튼

    // ResultPanel 버튼들
    public GameObject SaveButton; // 저장 버튼
    public GameObject ShareButton; // 공유 버튼
    public GameObject resultpanelBackButton; // ResultPanel 뒤로가기 버튼

    // 캡처할 이미지의 가로 및 세로 크기
    public int CaptureWidth = 1080;
    public int CaptureHeight = 1600;

    // 찍은 이미지를 저장할 Texture2D 객체 및 결과를 보여줄 Image 객체
    private Texture2D capturedTexture;
    public Image ResultImage;
    public GameObject SizePanel;

    void Start()
    {
        // 초기화 설정. ChoosePanel 및 해당 버튼들만 활성화
        SetChooseMode();
        SaveCompletePnael.SetActive(false);

        // VideoPanel 뒤로가기 버튼 클릭 시 ChooseMode로 설정
        VideopanelBackButton.GetComponent<Button>().onClick.AddListener(SetChooseMode);
    }

    // ChoosePanel 활성화
    public void SetChooseMode()
    {
        ChoosePanel.SetActive(true);
        ResultPanel.SetActive(false);
        VideoPanel.SetActive(false);
    }

    // VideoPanel 활성화
    public void SetVideoMode()
    {
        ChoosePanel.SetActive(false);
        ResultPanel.SetActive(false);
        VideoPanel.SetActive(true);
        VideoCaptureButton.SetActive(true);
        VideoStopButton.SetActive(false);
    }

    // ResultPanel 활성화
    public void SetResultMode()
    {
        ChoosePanel.SetActive(false);
        ResultPanel.SetActive(true);
        VideoPanel.SetActive(false);
    }

    // 스크린샷 찍기
    public void TakeScreenshot()
    {
        StartCoroutine(CaptureScreenshot());
        SetResultMode();
    }

    // 스크린샷 캡처 코루틴
    private IEnumerator CaptureScreenshot()
    {
        // 캡처 시 Result Image가 함께 찍히는 문제를 해결하기 위해 투명도 조정
        ResultImage.color = new Color(0, 0, 0, 0);

        // 프레임 대기
        yield return new WaitForEndOfFrame();

        // SizePanel 크기 가져오기
        RectTransform SizePanelRectTransform = SizePanel.GetComponent<RectTransform>();
        Vector2 panelSize = SizePanelRectTransform.sizeDelta;

        // 캡처 영역 설정
        Rect captureRect = new Rect(0, 553, panelSize.x, panelSize.y);

        // Texture2D로 캡처
        capturedTexture = new Texture2D(Mathf.RoundToInt(captureRect.width), Mathf.RoundToInt(captureRect.height), TextureFormat.RGB24, false);
        capturedTexture.ReadPixels(captureRect, 0, 0);
        capturedTexture.Apply();

        // 이미지 표시
        ResultImage.sprite = Sprite.Create(capturedTexture, new Rect(0, 0, capturedTexture.width, capturedTexture.height), Vector2.zero);
        ResultPanel.SetActive(true);
        SizePanel.SetActive(false);
        ResultImage.color = new Color(1, 1, 1, 1);
    }

    // 스크린샷 저장
    public void SaveScreenshot()
    {
        if (capturedTexture != null)
        {
            // 이미지를 PNG 파일로 저장
            byte[] bytes = capturedTexture.EncodeToPNG();
            string fileName = "screenshot.png";
            string folderPath = Path.Combine(Application.persistentDataPath, "DCIM/Sta");
            string filePath = Path.Combine(folderPath, fileName);

            // 폴더 생성
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // 갤러리/사진에 스크린샷 저장
            string name = string.Format("{0}_Capture{1}_{2}.png", Application.productName, "{0}", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
            NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(bytes, Application.productName + " Captures", name);
            Debug.Log("Permission result: " + permission);
            Debug.Log("Screenshot saved at: " + filePath);
        }

        // 저장 완료 패널 활성화 후 3초 뒤에 숨김
        SaveCompletePnael.SetActive(true);
        Invoke("HideSaveCompletePanel", 3f);
    }

    // 저장 완료 패널 숨기기
    public void HideSaveCompletePanel()
    {
        SaveCompletePnael.SetActive(false);
    }

    // 뒤로가기 버튼 클릭 시 ChooseMode로 전환
    public void OnBackButtonClicked()
    {
        SetChooseMode();
    }

    // 특정 씬으로 이동하는 뒤로가기 버튼 처리
    public void OnBackToSceneButtonClicked()
    {
        SceneManager.LoadScene("YourSceneName");
    }

    // 갤러리에서 이미지 선택
    public void PickImageFromGallery()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        // Android에서 갤러리 앱 열기
        using (AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) 
        {
            using (AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity")) 
            {
                using (AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent")) 
                {
                    intent.Call<AndroidJavaObject>("setAction", intent.GetStatic<string>("ACTION_VIEW"));
                    intent.Call<AndroidJavaObject>("setType", "image/*"); // 모든 이미지 표시

                    currentActivity.Call("startActivity", intent);
                }

                }
#endif
    }
}

