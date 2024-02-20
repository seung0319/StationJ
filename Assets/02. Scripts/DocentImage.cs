using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class DocentImage : MonoBehaviour
{
    public GameObject videoPanel;
    public GameObject my3DObject;
    private ARTrackedImageManager trackedImageManager;
    private AndroidJavaObject cameraActivity;

    void Start()
    {
        videoPanel.SetActive(false);
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            string imageName = trackedImage.referenceImage.name;
            Debug.Log(imageName);

            videoPanel.SetActive(true);
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
            string imageName = trackedImage.referenceImage.name;
            Debug.Log(imageName);

            // 이미지의 위치와 회전을 업데이트합니다.
            GameObject imagePanel = trackedImage.transform.GetChild(0).gameObject;
            imagePanel.transform.position = trackedImage.transform.position;
            imagePanel.transform.rotation = trackedImage.transform.rotation;
        }

        foreach (ARTrackedImage trackedImage in args.removed)
        {
            string imageName = trackedImage.referenceImage.name;
            Debug.Log(imageName);

            // 추적이 종료된 이미지에 대한 처리를 수행합니다.
            Destroy(trackedImage.transform.GetChild(0).gameObject);
        }
    }

    public void OnBackBtnClickedEvent()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
}

