#if UNITY_EDITOR || UNITY_ANDROID
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Data;

public class NativeCameraManager : MonoBehaviour
{
    public Button camOnBtn;
    public Image captureImage;
    public Texture2D captureTexture;
    

    int CaptureCounter = 0;

     void Start()
    {
        camOnBtn.onClick.AddListener(NativeCameraOpen);
    }

    public void NativeCameraOpen()
    {
        if (NativeCamera.IsCameraBusy())
        {
            return;
        }
        if (NativeCamera.CheckPermission() == NativeCamera.Permission.Granted)
        {
            TakePicture();
        }
        else
        {
            NativeCamera.RequestPermission();
        }
    }

    void RequestPermissionAsync(Action callback)
    {
        NCPermissionCallbackAsyncAndroid callbackAndroid = new NCPermissionCallbackAsyncAndroid(callback);

        using (AndroidJavaClass nativeCamera = new AndroidJavaClass("com.yasirkula.unity.NativeCamera"))
        {
            nativeCamera.CallStatic("RequestPermission", callbackAndroid);
        }
    }

    void TakePicture()
    {
        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
     {
         if (path != null)
         {
             Texture2D texture = NativeCamera.LoadImageAtPath(path, 2048);
             if (texture == null)
             {
                 Debug.Log("Couldn't load texture from " + path);
                 return;
             }

             GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
             quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
             quad.transform.forward = Camera.main.transform.forward;
             quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

             Material material = quad.GetComponent<Renderer>().material;

             if (!material.shader.isSupported)
             {
                 material.shader = Shader.Find("Legacy Shaders/Diffuse");
             }
             material.mainTexture = texture;
             captureTexture = texture;
             var rect = new Rect(0, 0, captureTexture.width, captureTexture.height);
             captureImage.sprite = Sprite.Create(captureTexture, rect, new Vector2(0.5f, 0.5f));

             Texture2D readableTexture = GetReadableTexture(texture);
             Texture2D snap = new Texture2D(readableTexture.width, readableTexture.height);
             snap.SetPixels(readableTexture.GetPixels());
             snap.Apply();

             string time = DateTime.Now.ToString("yyyymmddhhmmssfff");
             string galaryPath = Path.Combine(Application.persistentDataPath, "원하는 파일이름" + time + CaptureCounter.ToString() + ".png");
             File.WriteAllBytes(galaryPath, snap.EncodeToPNG());
             ++CaptureCounter;
             Destroy(quad, 5f);

         }


     }, 2048, true, NativeCamera.PreferredCamera.Front);
    }

    private Texture2D GetReadableTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
            source.width,
            source.height,
            0,
            RenderTextureFormat.Default,
            RenderTextureReadWrite.Linear
            );
        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableTexture = new Texture2D(source.width, source.height);
        readableTexture.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0)
       readableTexture.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableTexture;
    }
}
#endif
