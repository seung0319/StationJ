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

    //거의 완성본
    /*public Slider timelineSlider;
    public Button replayButton;
    private bool isPaused = true;
    private bool isTouching = false;
    public AudioSource audioSource; // 오디오를 재생할 AudioSource 컴포넌트
    private string recognizedImageName; // 인식된 이미지의 이름
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
            // 패널이 활성화되어 있으면 오디오 재생
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
    /// 오디오 조절
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
        Debug.Log("인식된 이미지 이름: " + recognizedImageName); // 인식된 이미지 이름 로깅
        // 이미지와 오디오를 매칭
        imageAudioDictionary["Image"] = Resources.Load<AudioClip>("Sounds/숭의목공예");
        imageAudioDictionary["Image2"] = Resources.Load<AudioClip>("Sounds/영스퀘어");

        foreach (var item in imageAudioDictionary)
        {
            Debug.Log("Key: " + item.Key + " Value: " + item.Value);
        }

        // 이미지 인식 후 매칭된 오디오 가져오기
        AudioClip audioClip;
        if (imageAudioDictionary.TryGetValue(recognizedImageName, out audioClip))
        {
            // 오디오를 재생
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.Log("인식된 이미지와 매칭되는 오디오를 찾을 수 없습니다.");
        }
    }

    /// <summary>
    /// 타임라인
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
    }*/ //거의 완성?ㅠ

    public Slider timelineSlider;
    public Button replayButton;
    public AudioSource audioSource;
    private bool isPaused = true;
    public Dictionary<string, AudioClip> imageAudioDictionary;

    void Start()
    {
        // 오디오 클립을 미리 로드하도록 수정합니다.
        LoadAudioClips();

        InitializeAudioSource();
        InitializeSlider();
        InitializeReplayButton();

        // 여기서 "pn_Play Tap Panel"의 상태를 체크하고 오디오 재생을 시작할 수 있습니다.
        GameObject panel = GameObject.Find("pn_Play Tap Panel");
        if (panel != null && panel.activeSelf)
        {
            // 패널이 활성화되어 있으면 오디오 재생
            // StartAudio 함수에 올바른 이미지 이름을 전달해야 합니다.
            StartAudio("Image1");  // 예시 이미지 이름
            StartAudio("Image2");  // 예시 이미지 이름
            isPaused = false;
        }
        else
        {
            InitializeSliderWithNullClip(); // 슬라이더를 초기화합니다 (임시로 클립이 Null일 때)
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
            { "Image1", Resources.Load<AudioClip>("Sounds/숭의목공예") },
            { "Image2", Resources.Load<AudioClip>("Sounds/영스퀘어") }
            // 클립이 더 있다면 여기에 추가...
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
            InitializeSliderWithNullClip(); // 클립이 Null일 때 슬라이더를 초기화합니다.
        }
    }

    private void InitializeSliderWithNullClip()
    {
        // 오디오 클립이 Null일 때 기본 슬라이더 값 설정
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
        // 이미지와 오디오를 매칭하여 해당 오디오 클립을 재생합니다.
        if (imageAudioDictionary.TryGetValue(imageName, out AudioClip audioClip))
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("인식된 이미지와 매칭되는 오디오를 찾을 수 없습니다: " + imageName);
        }
        ///
        //AudioClip audioClip = null;
        //switch (imageName)
        //{
        //    case "Image1":
        //        audioClip = Resources.Load<AudioClip>("Sounds/숭의목공예");
        //        break;
        //    case "Image2":
        //        audioClip = Resources.Load<AudioClip>("Sounds/영스퀘어");
        //        break;
        //    // 추가 이미지에 대한 case를 여기에 추가...
        //    default:
        //        Debug.LogError("인식된 이미지와 매칭되는 오디오를 찾을 수 없습니다: " + imageName);
        //        return; // 매칭되는 오디오가 없으므로 함수를 종료합니다.
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
