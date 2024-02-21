using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class DocentImage : MonoBehaviour
{
    public GameObject videoPanel;
    private ARTrackedImageManager trackedImageManager;
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
            Debug.Log(imageName);
            GameObject prefab = Resources.Load<GameObject>(imageName);

            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                obj.transform.SetParent(trackedImage.transform);
            }

            videoPanel.SetActive(true);
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
            string imageName = trackedImage.referenceImage.name;
            Debug.Log(imageName);

            if (trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
                trackedImage.transform.GetChild(0).gameObject.SetActive(true);

                void OnTrackedImagesEvent(ARTrackedImagesChangedEventArgs args)
                {
                    foreach (ARTrackedImage trackedImage in args.added)
                    {
                        string imageName = trackedImage.referenceImage.name;
                        Debug.Log(imageName);
                        GameObject prefab = Resources.Load<GameObject>(imageName);

                        if (prefab != null)
                        {
                            GameObject obj = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                            obj.transform.SetParent(trackedImage.transform);

                            // 3D ������Ʈ�� ��ġ�� �����ϴ� �ڵ� �߰�
                            obj.transform.localPosition = new Vector3(0, 0, 0); // ���ϴ� ��ġ�� ����
                        }

                        videoPanel.SetActive(true);
                    }

                    foreach (ARTrackedImage trackedImage in args.updated)
                    {
                        string imageName = trackedImage.referenceImage.name;
                        Debug.Log(imageName);

                        if (trackedImage.transform.childCount > 0)
                        {
                            trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                            trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
                            trackedImage.transform.GetChild(0).gameObject.SetActive(true);

                            // 3D ������Ʈ�� ��ġ�� �����ϴ� �ڵ� �߰�
                            trackedImage.transform.GetChild(0).localPosition = new Vector3(0, 0, 0); // ���ϴ� ��ġ�� ����
                        }
                    }

                    foreach (ARTrackedImage trackedImage in args.removed)
                    {
                        string imageName = trackedImage.referenceImage.name;
                        Debug.Log(imageName);

                        // ������ ����� �̹����� ���� ó���� �����մϴ�.
                        if (instantiatedObjects.ContainsKey(imageName))
                        {
                            Destroy(instantiatedObjects[imageName]);
                            instantiatedObjects.Remove(imageName);
                        }
                    }
                }

            }
        }

        foreach (ARTrackedImage trackedImage in args.removed)
        {
            string imageName = trackedImage.referenceImage.name;
            Debug.Log(imageName);

            // ������ ����� �̹����� ���� ó���� �����մϴ�.
            if (instantiatedObjects.ContainsKey(imageName))
            {
                Destroy(instantiatedObjects[imageName]);
                instantiatedObjects.Remove(imageName);
            }
        }
    }

    public void OnBackBtnClickedEvent()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesEvent;
    }
}

