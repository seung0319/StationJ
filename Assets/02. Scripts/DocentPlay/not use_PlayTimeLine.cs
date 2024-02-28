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

    /*public VideoPlayer videoPlayer;
    public Slider timelineSlider;
    public Button replayButton;
    private bool isPaused = true;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        replayButton.onClick.AddListener(ReplayVideo);

        timelineSlider.minValue = 0f;
        timelineSlider.maxValue = (float)videoPlayer.clip.length;
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
        if (Input.GetMouseButtonDown(0))
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

        // �����̴� ���� ���� Ÿ�Ӷ��� ����
        if (!isPaused)
        {
            timelineSlider.value = (float)videoPlayer.time;
        }
    }

    public void OnSliderValueChanged()
    {
        if (!isPaused)
        {
            videoPlayer.time = timelineSlider.value;
        }
    }*/

    /*public Slider timelineSlider;
    public Button replayButton;
    public AudioSource audioSource;
    private bool isPaused = true;

    void Start()
    {
        InitializeAudioSource();
        InitializeSlider();
        InitializeReplayButton();
    }

    void Update()
    {
        CheckTouchInput();

        if (!isPaused)
        {
            timelineSlider.value = audioSource.time;
        }
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
         //����� Ŭ���� Null�� �� �⺻ �����̴� �� ����
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

    public void OnSliderValueChanged()
    {
        if (audioSource.isPlaying)
        {
            audioSource.time = timelineSlider.value;
        }
    }*/

    public AudioSource audioSource;
    public Button replayButton;
    private bool isPaused = true;
    private bool isTouching = false;
    public Slider timelineSlider;

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
            if (audioSource.isPlaying && !isPaused)
            {
                audioSource.Pause();
                isPaused = true;
            }
            else if (isPaused)
            {
                audioSource.Play();
                isPaused = false;
            }
        }

        //�����̴� ���� ���� Ÿ�Ӷ��� ����
        if (!isPaused)
        {
            timelineSlider.value = (float)audioSource.time;
        }
    }
}
