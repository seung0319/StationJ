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

        //FFmpeg 실행 파일 경로와 옵션 설정
        ffmpegProcess.StartInfo.FileName = "ffmpeg";
        ffmpegProcess.StartInfo.Arguments = $"-f gdigrab -framerate 30 -i desktop {outputPath}";
        ffmpegProcess.StartInfo.UseShellExecute = false;
        ffmpegProcess.StartInfo.RedirectStandardOutput = true;

        //FFmpeg프로세스 시작
        ffmpegProcess.Start();


    }

  public void StopRecording()
    {
        //FFmpeg 프로세스 종료
        ffmpegProcess.CloseMainWindow();
        ffmpegProcess.Dispose();
        ffmpegProcess = null;

    }
}
