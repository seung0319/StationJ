using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capture : MonoBehaviour
{
    public WebCamTexture webCamTexture;
    RawImage rawImage;

    public bool initialized = false;
    private Color32[] color32;

    public void initialize()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        webCamTexture = new WebCamTexture();
        rawImage = GetComponent<RawImage>();
        rawImage.texture = webCamTexture;
        webCamTexture.Play();

        StartCoroutine(WaitWebCamInitialized());
        

    }

    IEnumerator WaitWebCamInitialized()
    {

        //webcamTexture.isPlaying 은 카메라에서 영상이 나오고 있는지 판정할 수 없었다.
        yield return new WaitForSeconds(1f);

        initialized = true;

    }

    public void Shutter(Action<Texture2D> callback)
    {
      
        color32 = webCamTexture.GetPixels32();

        Texture2D normalTexture = new Texture2D(webCamTexture.width, webCamTexture.height);
        {
            normalTexture.SetPixels32(color32);
            normalTexture.Apply();
        }

        callback(normalTexture);
    }
    
}
