using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DocentImage : MonoBehaviour
{
    //베이스 코드
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

                // *오디오파트* 오디오 클립을 재생하는 코드 추가
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
        if (imageName == "숭의목공예")
        {
            return Resources.Load<GameObject>("Prefab/숭의목공예");
        }
        else if (imageName == "영스퀘어")
        {
            return Resources.Load<GameObject>("Prefab/영스퀘어");
        }
        else
        {
            return null; // 이미지 이름에 해당하는 프리팹이 없는 경우 null을 반환합니다.
        }
    }
    // *오디오 파트*
    //인식한 이미지에 맞는 오디오 재생
    private AudioClip GetAudioClipForImage(string imageName)
    {
        if (imageName == "숭의목공예")
        {
            return Resources.Load<AudioClip>("Sounds/숭의목공예");
        }
        else if (imageName == "영스퀘어")
        {
            return Resources.Load<AudioClip>("Sounds/영스퀘어");
        }
        else
        {
            return null; // 이미지 이름에 해당하는 오디오 클립이 없는 경우 null을 반환합니다.
        }
    }

    //이벤트 종료
    public void OnBackBtnClickedEvent()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesEvent;
    }*/
    //베이스 코드[건들면 안됨]



    public GameObject videoPanel;
    public ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> instantiatedObjects = new Dictionary<string, GameObject>();
    
    // *오디오파트*
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

        // *오디오파트*
        timelineSlider.value = 0f;
        replayButton.onClick.AddListener(ReplayAudio);
    }

    // *오디오파트*
    void Update()
    {
        // 슬라이더 값에 따라 타임라인을 조정합니다.
        float timelineValue = timelineSlider.value;
        // 타임라인 조정에 필요한 작업 수행
        if(audioSource != null)
        {
            timelineSlider.value = audioSource.time;

            // 타임라인이 인식된 오디오의 최대치에 도달하면 슬라이더를 최대치로 조정합니다.
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

                // *오디오파트* 오디오 클립을 재생하는 코드 추가
                AudioClip audioClip = GetAudioClipForImage(imageName);
                if (audioClip != null)
                {
                    audioSource = obj.AddComponent<AudioSource>();
                    audioSource.clip = audioClip;
                    audioSource.Play();

                    // 타임라인 슬라이더의 최대값 설정
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
        if (imageName == "숭의목공예")
        {
            return Resources.Load<GameObject>("Prefab/숭의목공예");
        }
        else if (imageName == "영스퀘어")
        {
            return Resources.Load<GameObject>("Prefab/영스퀘어");
        }
        else
        {
            return null; // 이미지 이름에 해당하는 프리팹이 없는 경우 null을 반환합니다.
        }
    }
    // *오디오 파트*
    //인식한 이미지에 맞는 오디오 재생
    private AudioClip GetAudioClipForImage(string imageName)
    {
        if (imageName == "숭의목공예")
        {
            return Resources.Load<AudioClip>("Sounds/숭의목공예");
        }
        else if (imageName == "영스퀘어")
        {
            return Resources.Load<AudioClip>("Sounds/영스퀘어");
        }
        else
        {
            return null; // 이미지 이름에 해당하는 오디오 클립이 없는 경우 null을 반환합니다.
        }
    }
    public void ReplayAudio()
    {
        audioSource.Stop();
        audioSource.Play();
        timelineSlider.value = 0f;
    }

    //이벤트 종료
    public void OnBackBtnClickedEvent()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesEvent;
    }



    /*public GameObject videoPanel;
    public ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> instantiatedObjects = new Dictionary<string, GameObject>();

    // [오디오 파트]
    public AudioSource audioSource;
    public Button replayButton;
    private bool isPaused = true;
    public Slider timelineSlider;

    private float audioLength = 0f;
    // [오디오 파트 끝]

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        trackedImageManager.trackedImagesChanged += OnTrackedImagesEvent;

        // [오디오 파트]
        // 타임라인 슬라이더 초기값 설정
        timelineSlider.minValue = 0f;
        timelineSlider.maxValue = audioSource.clip.length;
        // [오디오 파트 끝]
    }

    void Start()
    {
        videoPanel.SetActive(false);

        // [오디오 파트]
        replayButton.onClick.AddListener(ReplayAudio);
        // [오디오 파트 끝]
    }

    // [오디오 파트]
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
    // [오디오 파트 끝]

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

                // [오디오 파트]
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
                // [오디오 파트 끝]
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
        if (imageName == "숭의목공예")
        {
            return Resources.Load<GameObject>("Prefab/숭의목공예");
        }
        else if (imageName == "영스퀘어")
        {
            return Resources.Load<GameObject>("Prefab/영스퀘어");
        }
        else
        {
            return null;
        }
    }

    // [오디오 파트]
    private AudioClip GetAudioClipForImage(string imageName)
    {
        if (imageName == "숭의목공예")
        {
            return Resources.Load<AudioClip>("Sounds/숭의목공예");
        }
        else if (imageName == "영스퀘어")
        {
            return Resources.Load<AudioClip>("Sounds/영스퀘어");
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
    // [오디오 파트 끝]

    public void OnBackBtnClickedEvent()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesEvent;
    }*/
}

