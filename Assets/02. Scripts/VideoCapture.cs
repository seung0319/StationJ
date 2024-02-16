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
        //�Կ� ���� �ڵ�
        recordingButton.SetActive(false);
        stopButton.SetActive(true);
       
    }

    public void StopRecording()
    {
        //�Կ� ���� �ڵ�

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
