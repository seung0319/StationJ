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

    public Slider timelineSlider;
    public Button replayButton;
    private bool isPaused = true;
    private bool isTouching = false;
    public Button startButton;
    public List<string> imageNames; // �̹��� ������ �̸��� ���� ����Ʈ
    public List<AudioClip> audioClips; // ����� Ŭ���� ���� ����Ʈ
    public AudioSource audioSource; // ������� ����� AudioSource ������Ʈ
    private string recognizedImageName; // �νĵ� �̹����� �̸�


    void Start()
    {
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        startButton.onClick.AddListener(StartAudio);

        replayButton.onClick.AddListener(ReplayAudio);
        timelineSlider.minValue = 0f;
        timelineSlider.maxValue = GetLongestClipLength();

    }

    void StartAudio()
    {
        recognizedImageName = RecognizeImage();

        // �̹����� �̸��� ��Ī�� ����� Ŭ���� ������
        int index = imageNames.IndexOf(recognizedImageName);
        if (index != -1)
        {
            AudioClip audioClip = audioClips[index];

            // ������� ���
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.Log("�νĵ� �̹����� �̸��� ��Ī�Ǵ� ����� Ŭ���� ã�� �� �����ϴ�.");
        }
    }

    // �̹��� �ν��� �����ϴ� �Լ�
    private string RecognizeImage()
    {
        //string imageName;
        /*if (imageName == "���Ǹ����")
        {
            PlayAAudio(0);
        }*/
        return recognizedImageName;
    }

    void ReplayAudio()
    {
        audioSource.Stop();
        audioSource.Play();
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
            if (IsAnyAudioPlaying() && !isPaused)
            {
                PauseAllAudio();
                isPaused = true;
            }
            else if (isPaused)
            {
                PlayAllAudio();
                isPaused = false;
            }
        }

        if (!isPaused)
        {
            timelineSlider.value = GetMaxAudioTime();
        }
    }

    public void OnSliderValueChanged()
    {
        if (!isPaused)
        {
            SetAllAudioTime(timelineSlider.value);
        }
    }

    private float GetLongestClipLength()
    {
        float maxLength = 0f;
        if (audioSource.clip != null && audioSource.clip.length > maxLength)
        {
            maxLength = audioSource.clip.length;
        }
        return maxLength;
    }

    private bool IsAnyAudioPlaying()
    {
        if (audioSource.isPlaying)
        {
            return true;
        }
        return false;
    }

    private void PauseAllAudio()
    {
        audioSource.Pause();
    }

    private void PlayAllAudio()
    {
        audioSource.Play();
    }

    private float GetMaxAudioTime()
    {
        float maxTime = 0f;
        if (audioSource.isPlaying && audioSource.time > maxTime)
        {
            maxTime = audioSource.time;
        }
        return maxTime;
    }

    private void SetAllAudioTime(float time)
    {
        audioSource.time = time;
    }

}
