using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//RequireComponent(typeof(ARRaycastManager))]
public class DocentImage : MonoBehaviour
{
    /*public GameObject videoPanel;
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
                obj.transform.localPosition = new Vector3(-0.2f, 0f, 0); // 원하는 위치로 수정
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
                trackedImage.transform.GetChild(0).localPosition = new Vector3(-0.2f, 0f, 0); // 원하는 위치로 수정
            }

            if(trackedImage.trackingState == TrackingState.Limited)
            {
                if (instantiatedObjects.ContainsKey(imageName))
                {
                    GameObject obj = instantiatedObjects[imageName];
                    obj.SetActive(false);
                    Debug.Log("이미지 파괴");
                }
            }
            else if (trackedImage.trackingState == TrackingState.Tracking)
            {
                if (instantiatedObjects.ContainsKey(imageName))
                {
                    GameObject obj = instantiatedObjects[imageName];
                    obj.SetActive(true); // 이미지가 Tracking 상태일 때 오브젝트를 활성화합니다.
                }
            }
        }

    }

    public void OnBackBtnClickedEvent()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesEvent;
    }*/

    /*public GameObject videoPanel;
    private ARTrackedImageManager trackedImageManager;
    private ARRaycastManager raycastManager;
    private Dictionary<string, GameObject> instantiatedObjects = new Dictionary<string, GameObject>();

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        trackedImageManager.trackedImagesChanged += OnTrackedImagesEvent;
        raycastManager = GetComponent<ARRaycastManager>();
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

            if (!instantiatedObjects.ContainsKey(imageName))
            {
                GameObject prefab = GetPrefabForImage(imageName);

                if (prefab != null)
                {
                    GameObject obj = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                    obj.transform.SetParent(trackedImage.transform);
                    //obj.transform.localPosition = Vector3.zero; // 도슨트 이미지의 위치를 이미지 트래킹 위치로 설정
                    instantiatedObjects.Add(imageName, obj);
                }
            }

            videoPanel.SetActive(true);
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
            string imageName = trackedImage.referenceImage.name;

            if (trackedImage.trackingState == TrackingState.Limited)
            {
                if (instantiatedObjects.ContainsKey(imageName))
                {
                    GameObject obj = instantiatedObjects[imageName];
                    obj.SetActive(false);
                    instantiatedObjects.Remove(imageName);
                }
            }
            else if (trackedImage.trackingState == TrackingState.Tracking)
            {
                if (!instantiatedObjects.ContainsKey(imageName))
                {
                    GameObject prefab = GetPrefabForImage(imageName);

                    if (prefab != null)
                    {
                        GameObject obj = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                        obj.transform.SetParent(trackedImage.transform);
                        //obj.transform.localPosition = Vector3.zero; // 도슨트 이미지의 위치를 이미지 트래킹 위치로 설정
                        instantiatedObjects.Add(imageName, obj);
                    }
                }
                else
                {
                    GameObject obj = instantiatedObjects[imageName];
                    obj.SetActive(true);
                }
            }
        }
    }

    private GameObject GetPrefabForImage(string imageName)
    {
        if (imageName == "Image1")
        {
            return Resources.Load<GameObject>("Image1");
        }
        else if (imageName == "Image2")
        {
            return Resources.Load<GameObject>("Image2");
        }
        else
        {
            return null; // 이미지 이름에 해당하는 프리팹이 없는 경우 null을 반환합니다.
        }
    }

    public void OnBackBtnClickedEvent()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesEvent;
    }*/

    public GameObject videoPanel;
    public ARTrackedImageManager trackedImageManager;
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
            InstantiateObjectForTrackedImage(trackedImage);
            videoPanel.SetActive(true);
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
            if (trackedImage.trackingState == TrackingState.Limited)
            {
                RemoveObjectForTrackedImage(trackedImage);
            }
            else if (trackedImage.trackingState == TrackingState.Tracking)
            {
                InstantiateObjectForTrackedImage(trackedImage);
            }
        }
    }

    void InstantiateObjectForTrackedImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (!instantiatedObjects.ContainsKey(imageName))
        {
            GameObject prefab = GetPrefabForImage(imageName);

            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                obj.transform.SetParent(trackedImage.transform);
                instantiatedObjects.Add(imageName, obj);
            }
        }
    }

    void RemoveObjectForTrackedImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (instantiatedObjects.ContainsKey(imageName))
        {
            GameObject obj = instantiatedObjects[imageName];
            Destroy(obj);
            instantiatedObjects.Remove(imageName);
        }
    }

    private GameObject GetPrefabForImage(string imageName)
    {
        if (imageName == "Image1")
        {
            return Resources.Load<GameObject>("Image1");
        }
        else if (imageName == "Image2")
        {
            return Resources.Load<GameObject>("Image2");
        }
        else
        {
            return null; // 이미지 이름에 해당하는 프리팹이 없는 경우 null을 반환합니다.
        }
    }

    public void OnBackBtnClickedEvent()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesEvent;
    }
}

