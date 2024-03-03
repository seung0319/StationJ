using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Recording : MonoBehaviour
{
    private const float SCREEN_WIDTH = 720f;
    private const string VIDEO_NAME = "Record", GALLERY_PATH = "/../../../../DCIM/VideoRecorders";
    public UnityAction onStartRecord, onStopRecord;
    public static UnityAction onAllowCallback, onDenyCallback, onDenyAndNeverAskAgainCallback;

//#if UNITY_ANDROID && !UNITY_EDITOR
    private AndroidJavaObject androidRecorder;
//#endif
    public enum VideoEncoder
    {
        DEFAULT,
        H263,
        H264,
        HEVC,
        MPEG_4_SP,
        VP8
    }
    // Start is called before the first frame update
    void Start()
    {
//#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            androidRecorder = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
			androidRecorder.Call("setUpSaveFolder","Tee");//custom your save folder to Movies/Tee, by defaut it will use Movies/AndroidUtils
            int width = (int)(Screen.width > SCREEN_WIDTH ? SCREEN_WIDTH : Screen.width);
            int height = Screen.width > SCREEN_WIDTH ? (int)(Screen.height * SCREEN_WIDTH / Screen.width) : Screen.height;
            int bitrate = (int)(1f * width * height / 100 * 240 * 7);
            int fps = 30;
            bool audioEnable=true;
            androidRecorder.Call("setupVideo", width, height,bitrate, fps,audioEnable,VideoEncoder.H264.ToString());//this line manual sets the video record setting. You can use the defaut setting by comment this code block
        }
//#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRecording()
    {
//#if UNITY_ANDROID && !UNITY_EDITOR
        //if (!AndroidUtils.IsPermitted(AndroidPermission.RECORD_AUDIO))//RECORD_AUDIO is declared inside plugin manifest but we need to request it manualy
        //{
        //    AndroidUtils.RequestPermission(AndroidPermission.RECORD_AUDIO);
        //    onAllowCallback = () =>
        //    {
        //        androidRecorder.Call("startRecording");
        //    };
        //    onDenyCallback = () => { ShowToast("Need RECORD_AUDIO permission to record voice");};
        //    onDenyAndNeverAskAgainCallback = () => { ShowToast("Need RECORD_AUDIO permission to record voice");};
        //}
        //else
            androidRecorder.Call("startRecording");
//#endif
    }

    public void StopRecording()
    {
//#if UNITY_ANDROID && !UNITY_EDITOR
	androidRecorder.Call("stopRecording");
//#endif
    }

    public void VideoRecorderCallback(string message)//this function will be call when record status change
    {
        switch (message)
        {
            case "init_record_error":
                break;
            case "start_record":
                if (onStartRecord != null) onStartRecord.Invoke();
                break;
            case "stop_record":
                if (onStopRecord != null) onStopRecord.Invoke();
                break;
        }
    }
}
