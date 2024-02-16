using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class VideoCapture : MonoBehaviour
{
    public ARCameraManager arCameraManager;
    public GameObject recordingButton;
    public GameObject stopButton;
    

    void Start()
    {
        recordingButton.SetActive(true);
        stopButton.SetActive(false);
        
    }

    public void StartRecording()
    {
        //촬영 시작 코드
        recordingButton.SetActive(false);
        stopButton.SetActive(true);
       
    }

    public void StopRecording()
    {
        //촬영 중지 코드

        PlayRecording();
        recordingButton.SetActive(true);
        stopButton.SetActive(false);
       
    }

    public void PlayRecording()
    {
        recordingButton.SetActive(true);
        stopButton.SetActive(false);
        
    }
}
