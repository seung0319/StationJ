using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class photozoneImgaedetection : MonoBehaviour
{
    ///Image Library �� �� �̹����� �´� 3D ������Ʈ�� Resources ��������
    ///�ҷ��ͼ� �����ϴ� ��
    ///

    ARTrackedImageManager imageManager;

    void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
        imageManager.trackedImagesChanged += OnImageTrackedEvent;
    }

    void OnImageTrackedEvent(ARTrackedImagesChangedEventArgs arg)
    {
        foreach(ARTrackedImage trackedImage in arg.added)
        {
            string imagename = trackedImage.referenceImage.name;
            Debug.Log(imagename);
            GameObject prefab = Resources.Load<GameObject>(imagename);



            if(prefab != null)
            {
                GameObject obj = Instantiate(prefab,trackedImage.transform.position, trackedImage.transform.transform.rotation);
                obj.transform.SetParent(trackedImage.transform); 
             
            }

        }


        foreach(ARTrackedImage trackedImage in arg.updated)
        {
            if(trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;

                trackedImage.transform.GetChild(0).gameObject.SetActive(true);

            }
        }



        foreach(ARTrackedImage trackedImage in arg.removed)
        {

            if(trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnImageTrackedEvent;
    }



}
