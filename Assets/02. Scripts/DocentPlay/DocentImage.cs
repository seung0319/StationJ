using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//[RequireComponent(typeof(ARRaycastManager))]
public class DocentImage : MonoBehaviour
{
    public GameObject videoPanel;
    private ARTrackedImageManager trackedImageManager;
    ARRaycastManager raycastManager;
    private Dictionary<string, GameObject> instantiatedObjects = new Dictionary<string, GameObject>();
     
    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        trackedImageManager.trackedImagesChanged += OnTrackedImagesEvent;
    }

    void Start()
    {
        videoPanel.SetActive(false);
    }

    void OnTrackedImagesEvent(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            string imageName = trackedImage.referenceImage.name;
            GameObject prefab = Resources.Load<GameObject>(imageName);

            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                obj.transform.SetParent(trackedImage.transform);
                obj.transform.localPosition = new Vector3(-0.2f, 0f, 0); // ���ϴ� ��ġ�� ����
                instantiatedObjects.Add(imageName, obj);

            }

            videoPanel.SetActive(true);
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
           
            Debug.Log(trackedImage.trackingState);
            string imageName = trackedImage.referenceImage.name;
            Debug.Log(imageName);

            if (trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
                trackedImage.transform.GetChild(0).gameObject.SetActive(true);
                trackedImage.transform.GetChild(0).localPosition = new Vector3(-0.2f, 0f, 0); // ���ϴ� ��ġ�� ����
            }

            if(trackedImage.trackingState == TrackingState.Limited)
            {
                if (instantiatedObjects.ContainsKey(imageName))
                {
                    GameObject obj = instantiatedObjects[imageName];
                    obj.SetActive(false);
                    Debug.Log("�̹��� �ı�");
                }
            }
            else if (trackedImage.trackingState == TrackingState.Tracking)
            {
                if (instantiatedObjects.ContainsKey(imageName))
                {
                    GameObject obj = instantiatedObjects[imageName];
                    obj.SetActive(true); // �̹����� Tracking ������ �� ������Ʈ�� Ȱ��ȭ�մϴ�.
                }
            }
        }

    }

    public void OnBackBtnClickedEvent()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesEvent;
    }
}

