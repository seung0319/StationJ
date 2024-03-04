using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultipleimageManager : MonoBehaviour
{
    ARTrackedImageManager imageManager;
    private Dictionary<string, GameObject> instantiatedObjects = new Dictionary<string, GameObject>();


    void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
        imageManager.trackedImagesChanged += OnImageTrackedEvent;
        
    }

    void OnImageTrackedEvent(ARTrackedImagesChangedEventArgs arg)
    {
        foreach (ARTrackedImage trackedImage in arg.added)
        {
            InstantiateObjectForTrackedImage(trackedImage);

        }

        foreach (ARTrackedImage trackedImage in arg.updated)
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

        Debug.Log("�νĵ�");
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
                    obj.transform.localPosition = new Vector3(-0.2f, -0.3f, -0.2f);
                    obj.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                    ///�ν��� �̹����� �ٷ� ���� ��ġ�� �����ǵ��� �������Ҵ�.
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

        private  GameObject GetPrefabForImage(string imageName)
        {

        //Resources ��� ���� �ӿ� P2 ��� ������ �� �ִµ�, �� ���� �������� �̸��� 
        // �ν��ؾ��� �̹����� �̸��� ���� �ͳ��� ��ġ�Ǿ� �ν��մϴ�.

            if (imageName == "P1 1")
            {
                return Resources.Load<GameObject>("Prefab2/P1 1");
            }
            else if (imageName == "P2")
            {
                return Resources.Load<GameObject>("Prefab2/P2");
            }
            else if (imageName == "P3 1")
            {
                return Resources.Load<GameObject>("Prefab2/P3 1");
                
            }
            else if (imageName == "P4")
            {
                return Resources.Load<GameObject>("Prefab2/P4");
            }
            else if (imageName == "P5")
            {
                return Resources.Load<GameObject>("Prefab2/P5");
            }
            else
            {
                return null; // �̹��� �̸��� �ش��ϴ� �������� ���� ��� null�� ��ȯ�մϴ�.
            }
        }


    public void OnBackBtnClickedEvent()
    {
        imageManager.trackedImagesChanged -= OnImageTrackedEvent;
    }

    // * ����
    // ReferenceImageLibrary (���� �ִ� ��) �� Name �� ���ҽý� ����
    // �ȿ� �ִ� ������Ʈ �̸��� ���ƾ� ��
    /// Keep Texture at Runtime üũ (�߿�)
    /// Specify Size üũ ��
    /// Physical Size �Է�
    /// 0.01 �� ���� 1cm ������ ����


}

    


