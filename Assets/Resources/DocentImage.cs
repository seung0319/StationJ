using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class DocentImage : MonoBehaviour
{
    /*public GameObject videoPanel;
    void OnImageTrackedEvent(ARTrackedImagesChangedEventArgs arg)
    {
        foreach (ARTrackedImage trackedImage in arg.added)
        {
            string imagename = trackedImage.referenceImage.name;
            GameObject prefab = Resources.Load<GameObject>(imagename);

            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                obj.transform.SetParent(trackedImage.transform);

                // 패널 오브젝트를 찾아서 활성화시킵니다.
                videoPanel.SetActive(true);
            }
        }
    }*/

    public GameObject videoPanel;

    void Start()
    {
        videoPanel.SetActive(false);
        ARTrackedImageManager trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
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
}
