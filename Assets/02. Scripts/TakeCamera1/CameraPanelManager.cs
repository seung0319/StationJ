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
    // �ʵ� ����
    public GameObject ChoosePanel; // ����, ���� �Կ� ���� �г�
    public GameObject ResultPanel; // �Կ� ��� Ȯ�� �г�
    public GameObject VideoPanel; // ���� �Կ� �г�
    public GameObject SaveCompletePnael; // ���� �Ϸ� �г�

    // ChoosePanel ��ư��
    public GameObject PhotoButton; // ���� �Կ� ��ư
    public GameObject PhotoPickButton; // ���������� ���� ���� ��ư
    public GameObject VideoButton; // ���� �Կ� ��ư
    public GameObject GalleryButton; // ������ ��ư
    public GameObject choosepanelBackButton; // ChoosePanel �ڷΰ��� ��ư

    // VideoPanel ��ư��
    public GameObject VideoCaptureButton; // ���� �Կ� ���� ��ư
    public GameObject VideoStopButton; // ���� �Կ� ���� ��ư
    public GameObject VideoGalleryutton; // ���� ������ ��ư
    public GameObject VideopanelBackButton; // VideoPanel �ڷΰ��� ��ư

    // ResultPanel ��ư��
    public GameObject SaveButton; // ���� ��ư
    public GameObject ShareButton; // ���� ��ư
    public GameObject resultpanelBackButton; // ResultPanel �ڷΰ��� ��ư

    // ĸó�� �̹����� ���� �� ���� ũ��
    public int CaptureWidth = 1080;
    public int CaptureHeight = 1600;

    // ���� �̹����� ������ Texture2D ��ü �� ����� ������ Image ��ü
    private Texture2D capturedTexture;
    public Image ResultImage;
    public GameObject SizePanel;

    void Start()
    {
        // �ʱ�ȭ ����. ChoosePanel �� �ش� ��ư�鸸 Ȱ��ȭ
        SetChooseMode();
        SaveCompletePnael.SetActive(false);

        // VideoPanel �ڷΰ��� ��ư Ŭ�� �� ChooseMode�� ����
        VideopanelBackButton.GetComponent<Button>().onClick.AddListener(SetChooseMode);
    }

    // ChoosePanel Ȱ��ȭ
    public void SetChooseMode()
    {
        ChoosePanel.SetActive(true);
        ResultPanel.SetActive(false);
        VideoPanel.SetActive(false);
    }

    // VideoPanel Ȱ��ȭ
    public void SetVideoMode()
    {
        ChoosePanel.SetActive(false);
        ResultPanel.SetActive(false);
        VideoPanel.SetActive(true);
        VideoCaptureButton.SetActive(true);
        VideoStopButton.SetActive(false);
    }

    // ResultPanel Ȱ��ȭ
    public void SetResultMode()
    {
        ChoosePanel.SetActive(false);
        ResultPanel.SetActive(true);
        VideoPanel.SetActive(false);
    }

    // ��ũ���� ���
    public void TakeScreenshot()
    {
        StartCoroutine(CaptureScreenshot());
        SetResultMode();
    }

    // ��ũ���� ĸó �ڷ�ƾ
    private IEnumerator CaptureScreenshot()
    {
        // ĸó �� Result Image�� �Բ� ������ ������ �ذ��ϱ� ���� ���� ����
        ResultImage.color = new Color(0, 0, 0, 0);

        // ������ ���
        yield return new WaitForEndOfFrame();

        // SizePanel ũ�� ��������
        RectTransform SizePanelRectTransform = SizePanel.GetComponent<RectTransform>();
        Vector2 panelSize = SizePanelRectTransform.sizeDelta;

        // ĸó ���� ����
        Rect captureRect = new Rect(0, 553, panelSize.x, panelSize.y);

        // Texture2D�� ĸó
        capturedTexture = new Texture2D(Mathf.RoundToInt(captureRect.width), Mathf.RoundToInt(captureRect.height), TextureFormat.RGB24, false);
        capturedTexture.ReadPixels(captureRect, 0, 0);
        capturedTexture.Apply();

        // �̹��� ǥ��
        ResultImage.sprite = Sprite.Create(capturedTexture, new Rect(0, 0, capturedTexture.width, capturedTexture.height), Vector2.zero);
        ResultPanel.SetActive(true);
        SizePanel.SetActive(false);
        ResultImage.color = new Color(1, 1, 1, 1);
    }

    // ��ũ���� ����
    public void SaveScreenshot()
    {
        if (capturedTexture != null)
        {
            // �̹����� PNG ���Ϸ� ����
            byte[] bytes = capturedTexture.EncodeToPNG();
            string fileName = "screenshot.png";
            string folderPath = Path.Combine(Application.persistentDataPath, "DCIM/Sta");
            string filePath = Path.Combine(folderPath, fileName);

            // ���� ����
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // ������/������ ��ũ���� ����
            string name = string.Format("{0}_Capture{1}_{2}.png", Application.productName, "{0}", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
            NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(bytes, Application.productName + " Captures", name);
            Debug.Log("Permission result: " + permission);
            Debug.Log("Screenshot saved at: " + filePath);
        }

        // ���� �Ϸ� �г� Ȱ��ȭ �� 3�� �ڿ� ����
        SaveCompletePnael.SetActive(true);
        Invoke("HideSaveCompletePanel", 3f);
    }

    // ���� �Ϸ� �г� �����
    public void HideSaveCompletePanel()
    {
        SaveCompletePnael.SetActive(false);
    }

    // �ڷΰ��� ��ư Ŭ�� �� ChooseMode�� ��ȯ
    public void OnBackButtonClicked()
    {
        SetChooseMode();
    }

    // Ư�� ������ �̵��ϴ� �ڷΰ��� ��ư ó��
    public void OnBackToSceneButtonClicked()
    {
        SceneManager.LoadScene("YourSceneName");
    }

    // ���������� �̹��� ����
    public void PickImageFromGallery()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        // Android���� ������ �� ����
        using (AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) 
        {
            using (AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity")) 
            {
                using (AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent")) 
                {
                    intent.Call<AndroidJavaObject>("setAction", intent.GetStatic<string>("ACTION_VIEW"));
                    intent.Call<AndroidJavaObject>("setType", "image/*"); // ��� �̹��� ǥ��

                    currentActivity.Call("startActivity", intent);
                }

                }
#endif
    }
}

