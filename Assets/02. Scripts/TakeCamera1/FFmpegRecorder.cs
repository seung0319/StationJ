using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class FFmpegRecorder : MonoBehaviour
{
    private Process ffmpegProcess;
    
   public void StartRecording (string outputPath)
    {

        ffmpegProcess = new Process();

        //FFmpeg ���� ���� ��ο� �ɼ� ����
        ffmpegProcess.StartInfo.FileName = "ffmpeg";
        ffmpegProcess.StartInfo.Arguments = $"-f gdigrab -framerate 30 -i desktop {outputPath}";
        ffmpegProcess.StartInfo.UseShellExecute = false;
        ffmpegProcess.StartInfo.RedirectStandardOutput = true;

        //FFmpeg���μ��� ����
        ffmpegProcess.Start();


    }

  public void StopRecording()
    {
        //FFmpeg ���μ��� ����
        ffmpegProcess.CloseMainWindow();
        ffmpegProcess.Dispose();
        ffmpegProcess = null;

    }
}
