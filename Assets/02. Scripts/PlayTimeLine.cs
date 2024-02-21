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

    public List<AudioSource> audioSources;
    public Slider timelineSlider;
    public Button replayButton;
    private bool isPaused = true;
    private bool isTouching = false;

    void Start()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.loop = false;
            audioSource.playOnAwake = false;
        }

        replayButton.onClick.AddListener(ReplayAudio);
        timelineSlider.minValue = 0f;
        timelineSlider.maxValue = GetLongestClipLength();
    }

    void ReplayAudio()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
            audioSource.Play();
            isPaused = false;
        }
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
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip != null && audioSource.clip.length > maxLength)
            {
                maxLength = audioSource.clip.length;
            }
        }
        return maxLength;
    }

    private bool IsAnyAudioPlaying()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.isPlaying)
            {
                return true;
            }
        }
        return false;
    }

    private void PauseAllAudio()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Pause();
        }
    }

    private void PlayAllAudio()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Play();
        }
    }

    private float GetMaxAudioTime()
    {
        float maxTime = 0f;
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.isPlaying && audioSource.time > maxTime)
            {
                maxTime = audioSource.time;
            }
        }
        return maxTime;
    }

    private void SetAllAudioTime(float time)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.time = time;
        }
    }
}
