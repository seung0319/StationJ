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

//Photo, Video ��� �� ��ư Ŭ���� ���� Panel �̵��� �� �̵��� ���� �ڵ�
// 3���� �г��� ���� ���� ��� ����

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

    public int CaptureWidth = 1080; //ĸ���� �̹��� ���� ũ��
    public int CaptureHeight = 1600; //ĸ���� �̹��� ���� ũ��
    
    private Texture2D capturedTexture;
    public Image ResultImage;
    public GameObject SizePanel;
    

    void Start()
    {
        //���ѿ�û �ڵ� �־��ּ���.


        //�ʱ� ���� ����. ChoosePanel�� �� ���� ��ư�鸸 Ȱ��ȭ
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
        //��ũ������ ���� �� ������� Result Image �� ���� �������ͼ�
        // �̺κ��� �����ϰ��� ���������� �������� �� Result Image�� ������
        // 0 ���� �س��ٰ� �� ������ �� �������� 1 �� �ٲ��ִ� ������ �ڵ带
        //�ۼ��ߴ�. ������ �ƴ� �� ������ �켱 �׷��� �ذ��Ͽ���.

        // ������ ���
        yield return new WaitForEndOfFrame();



        RectTransform SizePanelRectTransform = SizePanel.GetComponent<RectTransform>();
        Vector2 panelSize = SizePanelRectTransform.sizeDelta;

        Rect captureRect = new Rect(0, 553, panelSize.x, panelSize.y);

        capturedTexture = new Texture2D(Mathf.RoundToInt(captureRect.width), Mathf.RoundToInt(captureRect.height), TextureFormat.RGB24, false);
        capturedTexture.ReadPixels(captureRect, 0, 0);
        capturedTexture.Apply();

        // �̹��� ǥ��
        ResultImage.sprite = Sprite.Create(capturedTexture, new Rect(0, 0, capturedTexture.width, capturedTexture.height), Vector2.zero);
        ResultPanel.SetActive(true);
        SizePanel.SetActive(false);

        ResultImage.color = new Color(1, 1, 1, 1);



    }






    public void OnVideoButtonClicked()
    {
        //������ �Կ� ���� ��ȯ
        SetVideoMode();
    }

    public void OnVideoCaptureButtonClicked()
        
    {
        //������ �Կ� ���� �ڵ� ...
        // �Կ� ���� �� �Կ� ��ư ��Ȱ��ȭ, ���� ��ư Ȱ��ȭ

    }

    public void OnVideoStopButtonClicked()
    {
        //������ �Կ� ���� �ڵ�
        //�Կ� ���� �� �Կ� ��ư Ȱ��ȭ, ���� ��ư ��Ȱ��ȭ

        // �Կ��� ���� �� ��� ���� ��ȯ
        SetVideoMode();
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
        //�ڷΰ��� ��ư�� ������ ChooseMode�� ��ȯ.
        SetChooseMode();
    }

    public void OnBackToSceneButtonClicked()
    {
        //�ڷΰ��� ��ư�� ������ Ư�� ������ �̵�
        SceneManager.LoadScene("YourSceneName");
    }
    
}
