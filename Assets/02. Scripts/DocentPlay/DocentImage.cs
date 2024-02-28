using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
/// <summary>
/// arī�޶� �̿��� �̹����� �Կ��Ͽ� ����Ʈ �̹����� �ν��ϰ� 
/// Ÿ�Ӷ��� �� ��ư�� �ִ� ����Ʈ �÷��� �г��� ������ ���ÿ�
/// �̹����� �̸��� �´� ����� Ŭ���� ����ϰ� ����ð��� �´� Ÿ�Ӷ����� �۵��ϸ�
/// �̸��� �´� 3dĳ���Ͱ� ������ ������ �����Ѵ�.
/// 
/// ������� ������ �ִϸ��̼��� �����ϰ� ���÷��� ��ư���� ������� �ٽ� ����� �� ������
/// �̶� �ִϸ��̼��� ���µǸ� ������� �÷��� ���ӿ��� ���÷��� ��ư�� ������
/// ����� �� ĳ���Ͱ� ó������ �ٽ� ���۵ȴ�.
/// 
/// �̹��� Ʈ��ŷ�� ������ ���� �г��� �¿����� �����ǰ� 3d������Ʈ ���� ���� �� �������.
/// </summary>
public class DocentImage : MonoBehaviour
{

    // *�̹��� �ν� ��Ʈ*
    public GameObject videoPanel;
    public ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> instantiatedObjects = new Dictionary<string, GameObject>();

    // *�������Ʈ*
    public Slider timelineSlider;
    public Button replayButton;
    AudioSource audioSource;

    // *�ִϸ��̼� ��Ʈ*
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
        if (audioSource != null)
        {
            timelineSlider.value = audioSource.time;

            // Ÿ�Ӷ����� �νĵ� ������� �ִ�ġ�� �����ϸ� �����̴��� �ִ�ġ�� �����մϴ�.
            if (timelineSlider.value >= timelineSlider.maxValue)
            {
                timelineSlider.value = timelineSlider.maxValue;
            }

            // *�ִϸ��̼� ��Ʈ*
            // ������� ���۵Ǹ� "IsTalk" �ִϸ��̼� �Ķ���͸� false
            if (!audioSource.isPlaying)
            {
                animator.SetBool("IsTalk", false);
            }
            // ������� ���߸� "IsTalk" �ִϸ��̼� �Ķ���͸� true
            else
            {
                animator.SetBool("IsTalk", true);
            }
        }
    }

    // *�̹��� �ν� ��Ʈ*
    void OnTrackedImagesEvent(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            //�̹����� �νĵǸ� �г� ��
            InstantiateObjectForTrackedImage(trackedImage);
            videoPanel.SetActive(true);
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
            if (trackedImage.trackingState == TrackingState.Limited)
            {
                //�ν� �ȵǸ� �г� ����
                RemoveObjectForTrackedImage(trackedImage);
                videoPanel.SetActive(false);
            }
            else if (trackedImage.trackingState == TrackingState.Tracking)
            {
                //Ʈ��ŷ ���϶� �г� ��
                InstantiateObjectForTrackedImage(trackedImage);
                videoPanel.SetActive(true);
            }
        }
    }

    //�̹��� Ʈ��ŷ
    void InstantiateObjectForTrackedImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        //�̹��� �̸��� ���� ���� �´� ������ ����
        if (!instantiatedObjects.ContainsKey(imageName))
        {
            GameObject prefab = GetPrefabForImage(imageName);
            
            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                animator = obj.GetComponent<Animator>();
                obj.transform.SetParent(trackedImage.transform);

                //��ǥ ����, ĳ���� ��鸰�ٰ� �ؼ� �����
                obj.transform.localPosition = new Vector3(0f, -0.2f, 0f);
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

    //track���� �̹����� track���� ������ 3d������Ʈ ����
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
    // �ν��� �̹��� �̸��� �´� ������ ����
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

    //Replay��ư ������ ó������ �ٽ� ���
    public void ReplayAudio()
    {
        audioSource.Stop();
        audioSource.Play();
        timelineSlider.value = 0f;

        // * �ִϸ��̼� ��Ʈ*
        animator.Rebind();
    }

    //�� Ż��� �̺�Ʈ ����
    public void OnBackBtnClickedEvent()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesEvent;
        aRSession.Reset();
    }
}

