using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayTimeLine : MonoBehaviour
{
    public RawImage videoImage; // ���� ����� ���� Raw Image
    public Text timelineText; // Ÿ�Ӷ����� ǥ���� Text

    public VideoClip videoClip; // ����� ���� Ŭ��

    private VideoPlayer videoPlayer; // ���� �÷��̾�
    private float videoDuration; // ������ ��ü ����

    private void Start()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>(); // ���� �÷��̾� ������Ʈ �߰�
        videoPlayer.playOnAwake = false; // ���� �� �ڵ� ������� �ʵ��� ����

        videoPlayer.source = VideoSource.VideoClip; // ���� �ҽ��� ���� Ŭ������ ����
        videoPlayer.clip = videoClip; // ����� ���� Ŭ�� ����

        videoPlayer.Prepare(); // ������ �غ��Ͽ� ��� �غ�

        videoPlayer.loopPointReached += OnVideoEnd; // ���� ����� ������ �� ȣ���� �޼��� ����

        videoPlayer.prepareCompleted += OnVideoPrepared; // ���� �غ� �Ϸ�Ǿ��� �� ȣ���� �޼��� ����
    }

    private void Update()
    {
        // Ÿ�Ӷ��� ������Ʈ
        if (videoPlayer.isPlaying)
        {
            float currentTime = (float)videoPlayer.time;
            timelineText.text = currentTime.ToString("F1") + " / " + videoDuration.ToString("F1");
        }
    }

    private void OnVideoPrepared(VideoPlayer player)
    {
        // ���� �غ� �Ϸ�Ǹ� ������ ����ϰ� ����մϴ�.
        videoPlayer.Play();

        // ������ ��ü ���̸� �����ɴϴ�.
        videoDuration = (float)videoPlayer.length;
    }

    private void OnVideoEnd(VideoPlayer player)
    {
        // ���� ����� ������ �� ȣ��Ǵ� �޼����Դϴ�.
        // �ʿ��� ó���� �߰��ϼ���.
    }
}
