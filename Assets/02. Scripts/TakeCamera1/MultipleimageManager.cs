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

        Debug.Log("인식됨");
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
                    ///인식한 이미지의 바로 앞의 위치에 생성되도록 만들어놓았다.
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

        //Resources 라는 파일 속에 P2 라는 폴더가 또 있는데, 그 안의 프리팹의 이름과 
        // 인식해야할 이미지의 이름이 같은 것끼리 매치되어 인식합니다.

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
                return null; // 이미지 이름에 해당하는 프리팹이 없는 경우 null을 반환합니다.
            }
        }


    public void OnBackBtnClickedEvent()
    {
        imageManager.trackedImagesChanged -= OnImageTrackedEvent;
    }

    // * 주의
    // ReferenceImageLibrary (사진 넣는 곳) 에 Name 와 리소시스 폴더
    // 안에 있는 오브젝트 이름이 같아야 함
    /// Keep Texture at Runtime 체크 (중요)
    /// Specify Size 체크 후
    /// Physical Size 입력
    /// 0.01 당 실제 1cm 정도의 비율


}

    


