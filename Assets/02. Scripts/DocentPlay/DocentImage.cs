using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DocentImage : MonoBehaviour
{
    //���̽� �ڵ�
    /*public GameObject videoPanel;
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
                obj.transform.localPosition = new Vector3(-0.2f, -0.3f, -0.2f);
                obj.transform.localRotation = Quaternion.Euler(-90f, 180f, 0f);
                instantiatedObjects.Add(imageName, obj);

                // *�������Ʈ* ����� Ŭ���� ����ϴ� �ڵ� �߰�
                AudioClip audioClip = GetAudioClipForImage(imageName);
                if (audioClip != null)
                {
                    AudioSource audioSource = obj.AddComponent<AudioSource>();
                    audioSource.clip = audioClip;
                    audioSource.Play();
                }
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
        if (imageName == "���Ǹ����")
        {
            return Resources.Load<GameObject>("Prefab/���Ǹ����");
        }
        else if (imageName == "��������")
        {
            return Resources.Load<GameObject>("Prefab/��������");
        }
        else
        {
            return null; // �̹��� �̸��� �ش��ϴ� �������� ���� ��� null�� ��ȯ�մϴ�.
        }
    }
    // *����� ��Ʈ*
    //�ν��� �̹����� �´� ����� ���
    private AudioClip GetAudioClipForImage(string imageName)
    {
        if (imageName == "���Ǹ����")
        {
            return Resources.Load<AudioClip>("Sounds/���Ǹ����");
        }
        else if (imageName == "��������")
        {
            return Resources.Load<AudioClip>("Sounds/��������");
        }
        else
        {
            return null; // �̹��� �̸��� �ش��ϴ� ����� Ŭ���� ���� ��� null�� ��ȯ�մϴ�.
        }
    }

    //�̺�Ʈ ����
    public void OnBackBtnClickedEvent()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesEvent;
    }*/
    //���̽� �ڵ�[�ǵ�� �ȵ�]



    public GameObject videoPanel;
    public ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> instantiatedObjects = new Dictionary<string, GameObject>();
    
    // *�������Ʈ*
    public Slider timelineSlider;
    public Button replayButton;

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        trackedImageManager.trackedImagesChanged += OnTrackedImagesEvent;
    }

    void Start()
    {
        videoPanel.SetActive(false);

        // *�������Ʈ*
        timelineSlider.value = 0f;
        replayButton.onClick.AddListener(ReplayAudio);
    }

    // *�������Ʈ*
    void Update()
    {
        // �����̴� ���� ���� Ÿ�Ӷ����� �����մϴ�.
        float timelineValue = timelineSlider.value;
        // Ÿ�Ӷ��� ������ �ʿ��� �۾� ����
        if(audioSource != null)
        {
            timelineSlider.value = audioSource.time;

            // Ÿ�Ӷ����� �νĵ� ������� �ִ�ġ�� �����ϸ� �����̴��� �ִ�ġ�� �����մϴ�.
            if (timelineSlider.value >= timelineSlider.maxValue)
            {
                timelineSlider.value = timelineSlider.maxValue;
            }
        }
            timelineSlider.value = audioSource.time;
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
    AudioSource audioSource;
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
                obj.transform.localRotation = Quaternion.Euler(-90f, 180f, 0f);
                instantiatedObjects.Add(imageName, obj);

                // *�������Ʈ* ����� Ŭ���� ����ϴ� �ڵ� �߰�
                AudioClip audioClip = GetAudioClipForImage(imageName);
                if (audioClip != null)
                {
                    audioSource = obj.AddComponent<AudioSource>();
                    audioSource.clip = audioClip;
                    audioSource.Play();

                    // Ÿ�Ӷ��� �����̴��� �ִ밪 ����
                    timelineSlider.maxValue = audioClip.length;
                }
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
        if (imageName == "���Ǹ����")
        {
            return Resources.Load<GameObject>("Prefab/���Ǹ����");
        }
        else if (imageName == "��������")
        {
            return Resources.Load<GameObject>("Prefab/��������");
        }
        else
        {
            return null; // �̹��� �̸��� �ش��ϴ� �������� ���� ��� null�� ��ȯ�մϴ�.
        }
    }
    // *����� ��Ʈ*
    //�ν��� �̹����� �´� ����� ���
    private AudioClip GetAudioClipForImage(string imageName)
    {
        if (imageName == "���Ǹ����")
        {
            return Resources.Load<AudioClip>("Sounds/���Ǹ����");
        }
        else if (imageName == "��������")
        {
            return Resources.Load<AudioClip>("Sounds/��������");
        }
        else
        {
            return null; // �̹��� �̸��� �ش��ϴ� ����� Ŭ���� ���� ��� null�� ��ȯ�մϴ�.
        }
    }
    public void ReplayAudio()
    {
        audioSource.Stop();
        audioSource.Play();
        timelineSlider.value = 0f;
    }

    //�̺�Ʈ ����
    public void OnBackBtnClickedEvent()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesEvent;
    }



    /*public GameObject videoPanel;
    public ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> instantiatedObjects = new Dictionary<string, GameObject>();

    // [����� ��Ʈ]
    public AudioSource audioSource;
    public Button replayButton;
    private bool isPaused = true;
    public Slider timelineSlider;

    private float audioLength = 0f;
    // [����� ��Ʈ ��]

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        trackedImageManager.trackedImagesChanged += OnTrackedImagesEvent;

        // [����� ��Ʈ]
        // Ÿ�Ӷ��� �����̴� �ʱⰪ ����
        timelineSlider.minValue = 0f;
        timelineSlider.maxValue = audioSource.clip.length;
        // [����� ��Ʈ ��]
    }

    void Start()
    {
        videoPanel.SetActive(false);

        // [����� ��Ʈ]
        replayButton.onClick.AddListener(ReplayAudio);
        // [����� ��Ʈ ��]
    }

    // [����� ��Ʈ]
    void Update()
    {
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        isPaused = !isPaused;
        //        if (isPaused)
        //        {
        //            audioSource.Pause();
        //        }
        //        else
        //        {
        //            audioSource.Play();
        //        }
        //    }
        //}

        if (!isPaused)
        {
            timelineSlider.value = audioSource.time;
        }
    }
    // [����� ��Ʈ ��]

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
                videoPanel.SetActive(false);
            }
            else if (trackedImage.trackingState == TrackingState.Tracking)
            {
                InstantiateObjectForTrackedImage(trackedImage);
                videoPanel.SetActive(true);
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
                obj.transform.localPosition = new Vector3(-0.2f, -0.3f, -0.2f);
                obj.transform.localRotation = Quaternion.Euler(-90f, 180f, 0f);
                instantiatedObjects.Add(imageName, obj);

                // [����� ��Ʈ]
                AudioClip audioClip = GetAudioClipForImage(imageName);
                if (audioClip != null)
                {
                    audioSource.clip = audioClip;

                    timelineSlider.value = 0f;
                    //timelineSlider.minValue = 0f;
                    timelineSlider.maxValue = audioClip.length;
                    audioLength = audioClip.length;

                    audioSource.Play();
                }
                // [����� ��Ʈ ��]
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
        if (imageName == "���Ǹ����")
        {
            return Resources.Load<GameObject>("Prefab/���Ǹ����");
        }
        else if (imageName == "��������")
        {
            return Resources.Load<GameObject>("Prefab/��������");
        }
        else
        {
            return null;
        }
    }

    // [����� ��Ʈ]
    private AudioClip GetAudioClipForImage(string imageName)
    {
        if (imageName == "���Ǹ����")
        {
            return Resources.Load<AudioClip>("Sounds/���Ǹ����");
        }
        else if (imageName == "��������")
        {
            return Resources.Load<AudioClip>("Sounds/��������");
        }
        else
        {
            return null;
        }
    }

    public void ReplayAudio()
    {
        audioSource.Stop();
        audioSource.Play();
        isPaused = false;
        timelineSlider.value = 0f;
    }
    // [����� ��Ʈ ��]

    public void OnBackBtnClickedEvent()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesEvent;
    }*/
}

