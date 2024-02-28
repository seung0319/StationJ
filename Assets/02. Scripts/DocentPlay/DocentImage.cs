using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
/// <summary>
/// ar카메라를 이용해 이미지를 촬영하여 도슨트 이미지를 인식하고 
/// 타임라인 및 버튼이 있는 도슨트 플레이 패널이 켜지는 동시에
/// 이미지의 이름에 맞는 오디오 클립을 재생하고 재생시간에 맞는 타임라인이 작동하며
/// 이름에 맞는 3d캐릭터가 등장해 움직여 설명한다.
/// 
/// 오디오가 끝나면 애니메이션이 정지하고 리플레이 버튼으로 오디오를 다시 재생할 수 있으며
/// 이때 애니메이션은 리셋되며 오디오가 플레이 중임에도 리플레이 버튼을 누르면
/// 오디오 및 캐릭터가 처음부터 다시 시작된다.
/// 
/// 이미지 트래킹의 범위에 따라 패널의 온오프가 설정되고 3d오브젝트 또한 등장 및 사라진다.
/// </summary>
public class DocentImage : MonoBehaviour
{

    // *이미지 인식 파트*
    public GameObject videoPanel;
    public ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> instantiatedObjects = new Dictionary<string, GameObject>();

    // *오디오파트*
    public Slider timelineSlider;
    public Button replayButton;
    AudioSource audioSource;

    // *애니메이션 파트*
    public Animator animator;
    [SerializeField] ARSession aRSession;

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        trackedImageManager.trackedImagesChanged += OnTrackedImagesEvent;
        videoPanel.SetActive(false);
    }

    void Start()
    {
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
        if (audioSource != null)
        {
            timelineSlider.value = audioSource.time;

            // 타임라인이 인식된 오디오의 최대치에 도달하면 슬라이더를 최대치로 조정합니다.
            if (timelineSlider.value >= timelineSlider.maxValue)
            {
                timelineSlider.value = timelineSlider.maxValue;
            }

            // *애니메이션 파트*
            // 오디오가 시작되면 "IsTalk" 애니메이션 파라미터를 false
            if (!audioSource.isPlaying)
            {
                animator.SetBool("IsTalk", false);
            }
            // 오디오가 멈추면 "IsTalk" 애니메이션 파라미터를 true
            else
            {
                animator.SetBool("IsTalk", true);
            }
        }
    }

    // *이미지 인식 파트*
    void OnTrackedImagesEvent(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            //이미지가 인식되면 패널 온
            InstantiateObjectForTrackedImage(trackedImage);
            videoPanel.SetActive(true);
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
            if (trackedImage.trackingState == TrackingState.Limited)
            {
                //인식 안되면 패널 오프
                RemoveObjectForTrackedImage(trackedImage);
                videoPanel.SetActive(false);
            }
            else if (trackedImage.trackingState == TrackingState.Tracking)
            {
                //트래킹 중일때 패널 온
                InstantiateObjectForTrackedImage(trackedImage);
                videoPanel.SetActive(true);
            }
        }
    }

    //이미지 트래킹
    void InstantiateObjectForTrackedImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        //이미지 이름과 비교해 각각 맞는 프리팹 생성
        if (!instantiatedObjects.ContainsKey(imageName))
        {
            GameObject prefab = GetPrefabForImage(imageName);
            
            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                animator = obj.GetComponent<Animator>();
                obj.transform.SetParent(trackedImage.transform);

                //좌표 수정, 캐릭터 흔들린다고 해서 재수정
                obj.transform.localPosition = new Vector3(0f, -0.2f, 0f);
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

    //track중인 이미지가 track되지 않으면 3d오브젝트 삭제
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
    // 인식한 이미지 이름과 맞는 프리팹 연결
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

    //Replay버튼 누르면 처음부터 다시 재생
    public void ReplayAudio()
    {
        audioSource.Stop();
        audioSource.Play();
        timelineSlider.value = 0f;

        // * 애니메이션 파트*
        animator.Rebind();
    }

    //씬 탈출시 이벤트 제거
    public void OnBackBtnClickedEvent()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesEvent;
        aRSession.Reset();
    }
}

