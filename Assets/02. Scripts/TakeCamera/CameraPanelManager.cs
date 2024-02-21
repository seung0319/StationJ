using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
<<<<<<< Updated upstream
=======


>>>>>>> Stashed changes

//Photo, Video 기능 과 버튼 클릭을 통한 Panel 이동과 씬 이동에 관한 코드
// 3개의 패널을 오고 가며 기능 구현

public class CameraPanelManager : MonoBehaviour
{
    public GameObject ChoosePanel;
    public GameObject ResultPanel;
    public GameObject VideoPanel;
    public GameObject SaveCompletePnael;

    //ChoosePanel Buttons
    public GameObject PhotoButton;
    public GameObject PhotoPickButton;
    public GameObject VideoButton;
    public GameObject GalleryButton;
    public GameObject choosepanelBackButton;

    //VideoPanel Buttons
    public GameObject VideoCaptureButton;
    public GameObject VideoStopButton;
    public GameObject VideoGalleryutton;
    public GameObject VideopanelBackButton;

    //ReultPanel Buttons
    public GameObject SaveButton;
    public GameObject ShareButton;
    public GameObject resultpanelBackButton;

    public int CaptureWidth = 1080; //캡쳐할 이미지 가로 크기
    public int CaptureHeight = 1600; //캡쳐할 이미지 세로 크기
    
    private Texture2D capturedTexture;
    public Image ResultImage;
    public GameObject SizePanel;
    

    void Start()
    {
        //권한요청 코드 넣어주세요.


        //초기 상태 설정. ChoosePanel과 그 안의 버튼들만 활성화
        SetChooseMode();
        SaveCompletePnael.SetActive(false);
<<<<<<< Updated upstream
=======

        VideopanelBackButton.GetComponent<Button>().onClick.AddListener(SetChooseMode);



>>>>>>> Stashed changes
    }

    public void SetChooseMode()
    {
        ChoosePanel.SetActive(true);
        ResultPanel.SetActive(false);
        VideoPanel.SetActive(false);
    }

    public void SetVideoMode()
    {
        ChoosePanel.SetActive(false);
        ResultPanel.SetActive(false);
        VideoPanel.SetActive(true);
        VideoCaptureButton.SetActive(true);
        VideoStopButton.SetActive(false);
    }

    public void SetResultMode()
    {
        ChoosePanel.SetActive(false);
        ResultPanel.SetActive(true);
        VideoPanel.SetActive(false);
    }
    public void TakeScreenshot()
    {
        StartCoroutine(CaptureScreenshot());
        SetResultMode();
    }

    private IEnumerator CaptureScreenshot()
    {
        ResultImage.color = new Color(0, 0, 0, 0);
        //스크린샷을 찍을 때 결과물이 Result Image 가 같이 찍혀나와서
        // 이부분을 수정하고자 사진찍히는 순간에는 이 Result Image의 투명도를
        // 0 으로 해놨다가 다 찍히고 난 순간부터 1 로 바꿔주는 식으로 코드를
        //작성했다. 정석은 아닌 것 같지만 우선 그렇게 해결하였다.

        // 프레임 대기
        yield return new WaitForEndOfFrame();



        RectTransform SizePanelRectTransform = SizePanel.GetComponent<RectTransform>();
        Vector2 panelSize = SizePanelRectTransform.sizeDelta;

        Rect captureRect = new Rect(0, 553, panelSize.x, panelSize.y);

        capturedTexture = new Texture2D(Mathf.RoundToInt(captureRect.width), Mathf.RoundToInt(captureRect.height), TextureFormat.RGB24, false);
        capturedTexture.ReadPixels(captureRect, 0, 0);
        capturedTexture.Apply();

        // 이미지 표시
        ResultImage.sprite = Sprite.Create(capturedTexture, new Rect(0, 0, capturedTexture.width, capturedTexture.height), Vector2.zero);
        ResultPanel.SetActive(true);
        SizePanel.SetActive(false);

        ResultImage.color = new Color(1, 1, 1, 1);



    }






    public void OnVideoButtonClicked()
    {
        //동영상 촬영 모드로 전환
        SetVideoMode();
    }

    public void OnVideoCaptureButtonClicked()
        
    {
        //동영상 촬영 시작 코드 ...
        // 촬영 시작 시 촬영 버튼 비활성화, 중지 버튼 활성화

    }

    public void OnVideoStopButtonClicked()
    {
        //동영상 촬영 중지 코드
        //촬영 중지 시 촬영 버튼 활성화, 중지 버튼 비활성화

        // 촬영이 끝난 후 결과 모드로 전환
        SetVideoMode();
    }
    public void SaveScreenshot()
    {
        if (capturedTexture != null)
        {
            // 현재 텍스처를 이미지 파일로 저장
            byte[] bytes = capturedTexture.EncodeToPNG();
            string fileName = "screenshot.png";
            string folderPath = Path.Combine(Application.persistentDataPath, "DCIM/Sta");
            string filePath = Path.Combine(folderPath, fileName);

            // 폴더가 없으면 생성
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Save the screenshot to Gallery/Photos
            string name = string.Format("{0}_Capture{1}_{2}.png", Application.productName, "{0}", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
            // Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(capturedTexture, Application.productName + " Captures", name));
            Debug.Log("Screenshot saved at: " + filePath);
        }

        SaveCompletePnael.SetActive(true);
        Invoke("HideSaveCompletePanel", 3f);
    }
    
    public void HideSaveCompletePanel()
    {
        SaveCompletePnael.SetActive(false);
    }

    public void OnBackButtonClicked()
    {
        //뒤로가기 버튼을 누르면 ChooseMode로 전환.
        SetChooseMode();
    }

    public void OnBackToSceneButtonClicked()
    {
        //뒤로가기 버튼을 누르면 특정 씬으로 이동
        SceneManager.LoadScene("YourSceneName");
    }
    
}
