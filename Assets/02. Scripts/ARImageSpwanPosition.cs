using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARImageSpwanPosition : MonoBehaviour
{
    /*public GameObject spawnedObject; // 인식된 이미지에 생성될 3D 오브젝트

    private Dictionary<ARTrackedImage, GameObject> spawnedObjects = new Dictionary<ARTrackedImage, GameObject>();

    private void Awake()
    {
        // ARTrackedImageManager의 이벤트에 함수 등록
        ARTrackedImageManager trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            // 이미지가 인식되었을 때 호출되는 콜백 함수

            // 인식된 이미지에 대한 3D 오브젝트 생성
            GameObject spawned = Instantiate(spawnedObject, trackedImage.transform.position, trackedImage.transform.rotation);
            spawnedObjects.Add(trackedImage, spawned);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            // 이미지의 상태가 업데이트되었을 때 호출되는 콜백 함수

            // 인식된 이미지의 위치와 회전 정보 가져오기
            Vector3 position = trackedImage.transform.position;
            Quaternion rotation = trackedImage.transform.rotation;

            // 위치 조정
            // 원하는 위치로 수정하세요
            position += new Vector3(-2f, -1f, 0);

            // 회전 조정
            // 원하는 회전값으로 수정하세요
            rotation *= Quaternion.Euler(0, 10, 0);

            // 수정된 위치와 회전 정보 적용
            spawnedObjects[trackedImage].transform.position = position;
            spawnedObjects[trackedImage].transform.rotation = rotation;
        }
    }*/

    public GameObject objectToSpawn; // 인식된 이미지에 생성될 3D 오브젝트
    public ARTrackedImage targetImage; // 인식된 이미지와 연결할 타겟 이미지

    private GameObject spawnedObject; // 생성된 3D 오브젝트

    private void Awake()
    {
        // ARTrackedImageManager의 이벤트에 함수 등록
        ARTrackedImageManager trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            // 이미지가 인식되었을 때 호출되는 콜백 함수

            // 인식된 이미지가 타겟 이미지와 일치하는 경우
            if (trackedImage.referenceImage.name == targetImage.referenceImage.name)
            {
                // 인식된 이미지에 대한 3D 오브젝트 생성
                spawnedObject = Instantiate(objectToSpawn, trackedImage.transform.position, trackedImage.transform.rotation);
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            // 이미지의 상태가 업데이트되었을 때 호출되는 콜백 함수

            // 인식된 이미지가 타겟 이미지와 일치하고, 3D 오브젝트가 생성된 경우
            if (trackedImage.referenceImage.name == targetImage.referenceImage.name && spawnedObject != null)
            {
                // 인식된 이미지의 위치와 회전 정보 가져오기
                Vector3 position = trackedImage.transform.position;
                Quaternion rotation = trackedImage.transform.rotation;

                // 위치 조정
                // 원하는 위치로 수정하세요
                position += new Vector3(-2f, -1f, 0);

                // 회전 조정
                // 원하는 회전값으로 수정하세요
                rotation *= Quaternion.Euler(0, 10, 0);

                // 수정된 위치와 회전 정보 적용
                spawnedObject.transform.position = position;
                spawnedObject.transform.rotation = rotation;
            }
        }
    }
}