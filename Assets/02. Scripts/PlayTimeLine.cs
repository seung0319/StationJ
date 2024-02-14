using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayTimeLine : MonoBehaviour
{
    public RawImage videoImage; // 영상 출력을 위한 Raw Image
    public Text timelineText; // 타임라인을 표시할 Text

    public VideoClip videoClip; // 재생할 비디오 클립

    private VideoPlayer videoPlayer; // 비디오 플레이어
    private float videoDuration; // 비디오의 전체 길이

    private void Start()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>(); // 비디오 플레이어 컴포넌트 추가
        videoPlayer.playOnAwake = false; // 시작 시 자동 재생하지 않도록 설정

        videoPlayer.source = VideoSource.VideoClip; // 비디오 소스를 비디오 클립으로 설정
        videoPlayer.clip = videoClip; // 재생할 비디오 클립 설정

        videoPlayer.Prepare(); // 비디오를 준비하여 재생 준비

        videoPlayer.loopPointReached += OnVideoEnd; // 비디오 재생이 끝났을 때 호출할 메서드 설정

        videoPlayer.prepareCompleted += OnVideoPrepared; // 비디오 준비가 완료되었을 때 호출할 메서드 설정
    }

    private void Update()
    {
        // 타임라인 업데이트
        if (videoPlayer.isPlaying)
        {
            float currentTime = (float)videoPlayer.time;
            timelineText.text = currentTime.ToString("F1") + " / " + videoDuration.ToString("F1");
        }
    }

    private void OnVideoPrepared(VideoPlayer player)
    {
        // 비디오 준비가 완료되면 영상을 출력하고 재생합니다.
        videoPlayer.Play();

        // 영상의 전체 길이를 가져옵니다.
        videoDuration = (float)videoPlayer.length;
    }

    private void OnVideoEnd(VideoPlayer player)
    {
        // 비디오 재생이 끝났을 때 호출되는 메서드입니다.
        // 필요한 처리를 추가하세요.
    }
}
