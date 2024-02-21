using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARImageSpwanPosition : MonoBehaviour
{
    /*public GameObject spawnedObject; // �νĵ� �̹����� ������ 3D ������Ʈ

    private Dictionary<ARTrackedImage, GameObject> spawnedObjects = new Dictionary<ARTrackedImage, GameObject>();

    private void Awake()
    {
        // ARTrackedImageManager�� �̺�Ʈ�� �Լ� ���
        ARTrackedImageManager trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            // �̹����� �νĵǾ��� �� ȣ��Ǵ� �ݹ� �Լ�

            // �νĵ� �̹����� ���� 3D ������Ʈ ����
            GameObject spawned = Instantiate(spawnedObject, trackedImage.transform.position, trackedImage.transform.rotation);
            spawnedObjects.Add(trackedImage, spawned);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            // �̹����� ���°� ������Ʈ�Ǿ��� �� ȣ��Ǵ� �ݹ� �Լ�

            // �νĵ� �̹����� ��ġ�� ȸ�� ���� ��������
            Vector3 position = trackedImage.transform.position;
            Quaternion rotation = trackedImage.transform.rotation;

            // ��ġ ����
            // ���ϴ� ��ġ�� �����ϼ���
            position += new Vector3(-2f, -1f, 0);

            // ȸ�� ����
            // ���ϴ� ȸ�������� �����ϼ���
            rotation *= Quaternion.Euler(0, 10, 0);

            // ������ ��ġ�� ȸ�� ���� ����
            spawnedObjects[trackedImage].transform.position = position;
            spawnedObjects[trackedImage].transform.rotation = rotation;
        }
    }*/

    public GameObject objectToSpawn; // �νĵ� �̹����� ������ 3D ������Ʈ
    public ARTrackedImage targetImage; // �νĵ� �̹����� ������ Ÿ�� �̹���

    private GameObject spawnedObject; // ������ 3D ������Ʈ

    private void Awake()
    {
        // ARTrackedImageManager�� �̺�Ʈ�� �Լ� ���
        ARTrackedImageManager trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            // �̹����� �νĵǾ��� �� ȣ��Ǵ� �ݹ� �Լ�

            // �νĵ� �̹����� Ÿ�� �̹����� ��ġ�ϴ� ���
            if (trackedImage.referenceImage.name == targetImage.referenceImage.name)
            {
                // �νĵ� �̹����� ���� 3D ������Ʈ ����
                spawnedObject = Instantiate(objectToSpawn, trackedImage.transform.position, trackedImage.transform.rotation);
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            // �̹����� ���°� ������Ʈ�Ǿ��� �� ȣ��Ǵ� �ݹ� �Լ�

            // �νĵ� �̹����� Ÿ�� �̹����� ��ġ�ϰ�, 3D ������Ʈ�� ������ ���
            if (trackedImage.referenceImage.name == targetImage.referenceImage.name && spawnedObject != null)
            {
                // �νĵ� �̹����� ��ġ�� ȸ�� ���� ��������
                Vector3 position = trackedImage.transform.position;
                Quaternion rotation = trackedImage.transform.rotation;

                // ��ġ ����
                // ���ϴ� ��ġ�� �����ϼ���
                position += new Vector3(-2f, -1f, 0);

                // ȸ�� ����
                // ���ϴ� ȸ�������� �����ϼ���
                rotation *= Quaternion.Euler(0, 10, 0);

                // ������ ��ġ�� ȸ�� ���� ����
                spawnedObject.transform.position = position;
                spawnedObject.transform.rotation = rotation;
            }
        }
    }
}