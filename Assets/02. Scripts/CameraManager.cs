using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine;
using UnityEngine.XR.ARCore;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Google.XR.ARCoreExtensions;

public class CameraManager : MonoBehaviour
{
    public ARRecordingManager recordingManager;
    public ARCoreRecordingConfig recordingConfig;
    public string datasetPath = "file:///path/to/your/dataset.mp4";
    ARCoreSessionSubsystem arCoreSessionSubsystem;
    public Camera cam;

    private void Awake()
    {
        var session = FindObjectOfType<ARSession>();
        arCoreSessionSubsystem = session.subsystem as ARCoreSessionSubsystem;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Capture());
        }
    }

    public void TakePicture()
    {
        StartCoroutine(Capture());
    }

    public void StartRecord()
    {
        recordingManager.StartRecording(recordingConfig);
        string filePath = Path.Combine(Application.persistentDataPath, "myVideo.mp4");

        ArRecordingConfig arRecordingConfig = new ArRecordingConfig();
        

        ArStatus status = arCoreSessionSubsystem.StartRecording(arRecordingConfig);
        Debug.Log("Recording started with status: " + status);
    }

    public void StopRecord()
    {
        recordingManager.StopRecording();
        ArStatus status = arCoreSessionSubsystem.StopRecording();
        Debug.Log("Recording stopped with status: " + status);
    }

    IEnumerator Capture()
    {
        yield return new WaitForEndOfFrame();

        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        cam.targetTexture = rt;

        var currentRT = RenderTexture.active; // 현재 렌더링 되고 있는 텍스쳐
        RenderTexture.active = rt;

        cam.Render();

        Texture2D image = new Texture2D(Screen.width, Screen.height);
        image.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        image.Apply(); //활성 텍스쳐를 적용

        cam.targetTexture = null;

        RenderTexture.active = currentRT; //초기화

        byte[] bytes = image.EncodeToPNG(); //저장

        string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllBytes(filePath, bytes);

        Destroy(rt);
        Destroy(image);
    }

}
