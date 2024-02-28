using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NativeGalleryNamespace;


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

    YieldInstruction delay;



    void Start()
    {
        //���ѿ�û �ڵ� �־��ּ���.
        //�ʱ� ���� ����. ChoosePanel�� �� ���� ��ư�鸸 Ȱ��ȭ
        SetChooseMode();
        SaveCompletePnael.SetActive(false);

        VideopanelBackButton.GetComponent<Button>().onClick.AddListener(SetChooseMode);
        delay = new WaitForSeconds(0.5f);
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
        
    }

    private IEnumerator CaptureScreenshot()
    {
        choosepanelBackButton.SetActive(false);
        yield return delay;
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
        yield return delay;
        choosepanelBackButton.SetActive(true);
        SetResultMode();
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

    public void screenCapture()
    {
        StartCoroutine(SaveScreenshotCoroutine());
    }
    IEnumerator SaveScreenshotCoroutine()
    {
        resultpanelBackButton.SetActive(false);
        yield return delay;
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
            string name = string.Format("{0}_Capture{1}_{2}.png", Application.productName,"{0}", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
            NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(bytes, Application.productName + " Captures", name);
            Debug.Log("Permission result: " + permission);
            Debug.Log("Screenshot saved at: " + filePath);
        }

        SaveCompletePnael.SetActive(true);
        yield return delay;
        resultpanelBackButton.SetActive(true);
        yield return new WaitForSeconds(3f); // Invoke ��� �ڷ�ƾ�� WaitForSeconds�� ����մϴ�.
        HideSaveCompletePanel();

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

    public void OpenGalleryApp()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) 
        {
            using (AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity")) 
            {
                using (AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent")) 
                {
                    intent.Call<AndroidJavaObject>("setAction", intent.GetStatic<string>("ACTION_VIEW"));
                    intent.Call<AndroidJavaObject>("setType", "image/*");

                    currentActivity.Call("startActivity", intent);
                }
            }
        }
#endif
    }
}
