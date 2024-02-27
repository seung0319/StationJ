using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayTimeLine : MonoBehaviour
{
    /// ��ġ �ڵ� ///
    /*public VideoPlayer videoPlayer;
    public Button replayButton;
    private bool isPaused = true;
    private bool isTouching = false;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        replayButton.onClick.AddListener(ReplayVideo);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoPlayer.Stop();
        isPaused = false;
    }

    void ReplayVideo()
    {
        videoPlayer.Stop();
        videoPlayer.Play();
        isPaused = false;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                isTouching = true;
            }
        }
        else
        {
            isTouching = false;
        }

        if (isTouching)
        {
            if (videoPlayer.isPlaying && !isPaused)
            {
                videoPlayer.Pause();
                isPaused = true;
            }
            else if (isPaused)
            {
                videoPlayer.Play();
                isPaused = false;
            }
        }

        //�����̴� ���� ���� Ÿ�Ӷ��� ����
        if (!isPaused)
        {
           // timelineSlider.value = (float)videoPlayer.time;
        }
    }*/
    ///��ġ�ڵ� ��

    //public VideoPlayer videoPlayer;
    //public Slider timelineSlider;
    //public Button replayButton;
    //private bool isPaused = true;

    //void Start()
    //{
    //    videoPlayer.loopPointReached += OnVideoEnd;
    //    replayButton.onClick.AddListener(ReplayVideo);

    //    timelineSlider.minValue = 0f;
    //    timelineSlider.maxValue = (float)videoPlayer.clip.length;
    //}

    //void OnVideoEnd(VideoPlayer vp)
    //{
    //    videoPlayer.Stop();
    //    isPaused = false;
    //}

    //void ReplayVideo()
    //{
    //    videoPlayer.Stop();
    //    videoPlayer.Play();
    //    isPaused = false;
    //}

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (videoPlayer.isPlaying && !isPaused)
    //        {
    //            videoPlayer.Pause();
    //            isPaused = true;
    //        }
    //        else if (isPaused)
    //        {
    //            videoPlayer.Play();
    //            isPaused = false;
    //        }
    //    }

    //    // �����̴� ���� ���� Ÿ�Ӷ��� ����
    //    if (!isPaused)
    //    {
    //        timelineSlider.value = (float)videoPlayer.time;
    //    }
    //}

    //public void OnSliderValueChanged()
    //{
    //    if (!isPaused)
    //    {
    //        videoPlayer.time = timelineSlider.value;
    //    }
    //}

    //���� �ϼ���
    /*public Slider timelineSlider;
    public Button replayButton;
    private bool isPaused = true;
    private bool isTouching = false;
    public AudioSource audioSource; // ������� ����� AudioSource ������Ʈ
    private string recognizedImageName; // �νĵ� �̹����� �̸�
    Dictionary<string, AudioClip> imageAudioDictionary = new Dictionary<string, AudioClip>();

    void Start()
    {
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        timelineSlider.minValue = 0f;
        timelineSlider.maxValue = audioSource.clip ? audioSource.clip.length : 0f;
        replayButton.onClick.AddListener(ReplayAudio);
        GameObject panel = GameObject.Find("pn_Play Tap Panel");
        if (panel != null && panel.activeSelf)
        {
            // �г��� Ȱ��ȭ�Ǿ� ������ ����� ���
            StartAudio("Image");
            isPaused = false;
        }

    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            isTouching = touch.phase == TouchPhase.Began;
        }
        else
        {
            isTouching = false;
        }

        if (isTouching)
        {
            if (IsAnyAudioPlaying() && !isPaused)
            {
                PauseAudio();
            }
            else if (isPaused)
            {
                PlayAudio();
            }
        }

        if (!isPaused)
        {
            timelineSlider.value = GetAudioTime();
        }
    }

    /// <summary>
    /// ����� ����
    /// </summary>
    /// <returns></returns>
    private bool IsAnyAudioPlaying()
    {
        //if (audioSource.isPlaying)
        //{
        //    return true;
        //}
        //return false;
        return audioSource.isPlaying;
    }

    private void PauseAudio()
    {
        audioSource.Pause();
    }

    private void PlayAudio()
    {
        audioSource.Play();
    }

    void ReplayAudio()
    {
        audioSource.Stop();
        audioSource.Play();
        isPaused = false;
    }

    void StartAudio(string imageName)
    {
        recognizedImageName = imageName;
        Debug.Log("�νĵ� �̹��� �̸�: " + recognizedImageName); // �νĵ� �̹��� �̸� �α�
        // �̹����� ������� ��Ī
        imageAudioDictionary["Image"] = Resources.Load<AudioClip>("Sounds/���Ǹ����");
        imageAudioDictionary["Image2"] = Resources.Load<AudioClip>("Sounds/��������");

        foreach (var item in imageAudioDictionary)
        {
            Debug.Log("Key: " + item.Key + " Value: " + item.Value);
        }

        // �̹��� �ν� �� ��Ī�� ����� ��������
        AudioClip audioClip;
        if (imageAudioDictionary.TryGetValue(recognizedImageName, out audioClip))
        {
            // ������� ���
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.Log("�νĵ� �̹����� ��Ī�Ǵ� ������� ã�� �� �����ϴ�.");
        }
    }

    /// <summary>
    /// Ÿ�Ӷ���
    /// </summary>
    /// <returns></returns>
    public void OnSliderValueChanged()
    {
        if (!isPaused)
        {
            SetAllAudioTime(timelineSlider.value);
        }
    }

    private float GetAudioTime()
    {
        return audioSource.isPlaying ? audioSource.time : 0f;
    }

    private void SetAllAudioTime(float time)
    {
        audioSource.time = time;
    }*/ //���� �ϼ�?��

    public Slider timelineSlider;
    public Button replayButton;
    public AudioSource audioSource;
    private bool isPaused = true;
    public Dictionary<string, AudioClip> imageAudioDictionary;

    void Start()
    {
        // ����� Ŭ���� �̸� �ε��ϵ��� �����մϴ�.
        LoadAudioClips();

        InitializeAudioSource();
        InitializeSlider();
        InitializeReplayButton();

        // ���⼭ "pn_Play Tap Panel"�� ���¸� üũ�ϰ� ����� ����� ������ �� �ֽ��ϴ�.
        GameObject panel = GameObject.Find("pn_Play Tap Panel");
        if (panel != null && panel.activeSelf)
        {
            // �г��� Ȱ��ȭ�Ǿ� ������ ����� ���
            // StartAudio �Լ��� �ùٸ� �̹��� �̸��� �����ؾ� �մϴ�.
            StartAudio("Image1");  // ���� �̹��� �̸�
            StartAudio("Image2");  // ���� �̹��� �̸�
            isPaused = false;
        }
        else
        {
            InitializeSliderWithNullClip(); // �����̴��� �ʱ�ȭ�մϴ� (�ӽ÷� Ŭ���� Null�� ��)
        }
    }

    void Update()
    {
        CheckTouchInput();

        if (!isPaused)
        {
            timelineSlider.value = audioSource.time;
        }
    }

    private void LoadAudioClips()
    {
        imageAudioDictionary = new Dictionary<string, AudioClip>
        {
            { "Image1", Resources.Load<AudioClip>("Sounds/���Ǹ����") },
            { "Image2", Resources.Load<AudioClip>("Sounds/��������") }
            // Ŭ���� �� �ִٸ� ���⿡ �߰�...
        };
    }

    private void InitializeAudioSource()
    {
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    private void InitializeSlider()
    {
        if (audioSource.clip != null)
        {
            timelineSlider.minValue = 0f;
            timelineSlider.maxValue = audioSource.clip.length;
        }
        else
        {
            InitializeSliderWithNullClip(); // Ŭ���� Null�� �� �����̴��� �ʱ�ȭ�մϴ�.
        }
    }

    private void InitializeSliderWithNullClip()
    {
        // ����� Ŭ���� Null�� �� �⺻ �����̴� �� ����
        timelineSlider.minValue = 0f;
    }

    private void InitializeReplayButton()
    {
        replayButton.onClick.AddListener(ReplayAudio);
    }

    private void CheckTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                isPaused = !isPaused;
                if (isPaused)
                {
                    audioSource.Pause();
                }
                else
                {
                    audioSource.Play();
                }
            }
        }
    }

    private void ReplayAudio()
    {
        if (audioSource.clip != null)
        {
            audioSource.Stop();
            audioSource.Play();
            isPaused = false;
        }
    }

    public void StartAudio(string imageName)
    {
        // �̹����� ������� ��Ī�Ͽ� �ش� ����� Ŭ���� ����մϴ�.
        if (imageAudioDictionary.TryGetValue(imageName, out AudioClip audioClip))
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("�νĵ� �̹����� ��Ī�Ǵ� ������� ã�� �� �����ϴ�: " + imageName);
        }
        ///
        //AudioClip audioClip = null;
        //switch (imageName)
        //{
        //    case "Image1":
        //        audioClip = Resources.Load<AudioClip>("Sounds/���Ǹ����");
        //        break;
        //    case "Image2":
        //        audioClip = Resources.Load<AudioClip>("Sounds/��������");
        //        break;
        //    // �߰� �̹����� ���� case�� ���⿡ �߰�...
        //    default:
        //        Debug.LogError("�νĵ� �̹����� ��Ī�Ǵ� ������� ã�� �� �����ϴ�: " + imageName);
        //        return; // ��Ī�Ǵ� ������� �����Ƿ� �Լ��� �����մϴ�.
        //}

        //if (audioClip != null)
        //{
        //    audioSource.clip = audioClip;
        //    audioSource.Play();
        //}
    }

    public void OnSliderValueChanged()
    {
        if (audioSource.isPlaying)
        {
            audioSource.time = timelineSlider.value;
        }
    }
}
