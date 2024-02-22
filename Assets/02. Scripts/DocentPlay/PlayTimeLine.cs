using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayTimeLine : MonoBehaviour
{
    /// 터치 코드 ///
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

        //슬라이더 값에 따라 타임라인 조절
        if (!isPaused)
        {
           // timelineSlider.value = (float)videoPlayer.time;
        }
    }*/
    ///터치코드 끝

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

    //    // 슬라이더 값에 따라 타임라인 조절
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
    public List<string> imageNames; // 이미지 파일의 이름을 담을 리스트
    public List<AudioClip> audioClips; // 오디오 클립을 담을 리스트
    public AudioSource audioSource; // 오디오를 재생할 AudioSource 컴포넌트
    private string recognizedImageName; // 인식된 이미지의 이름


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

        // 이미지의 이름과 매칭된 오디오 클립을 가져옴
        int index = imageNames.IndexOf(recognizedImageName);
        if (index != -1)
        {
            AudioClip audioClip = audioClips[index];

            // 오디오를 재생
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.Log("인식된 이미지의 이름과 매칭되는 오디오 클립을 찾을 수 없습니다.");
        }
    }

    // 이미지 인식을 수행하는 함수
    private string RecognizeImage()
    {
        //string imageName;
        /*if (imageName == "숭의목공예")
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
