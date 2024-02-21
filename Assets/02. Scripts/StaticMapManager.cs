using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StaticMapManager : MonoBehaviour
{
    [SerializeField] string baseUrl = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster";
    [SerializeField] string clientID = "w9g29xtrwz";
    [SerializeField] string clientSecret = "WespgDc0DDelU0pN69HzZgyj5ByEBwhDGU0gxxTB";
    public RawImage mapRawImage;

    public string latitude = "37.713675";
    public string longitude = "126.743572";
    public int level = 17;
    public int mapWidth = 720;
    public int mapHeight = 1600;

    public Text debug;

    public void Start()
    {
        StartCoroutine(MapLoader());
    }

    IEnumerator MapLoader()
    {
        string apiRequestURL = $"{baseUrl}?w={mapWidth}&h={mapHeight}&center={longitude},{latitude}&level={level}";
        debug.text = "Hello";
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(apiRequestURL);
        debug.text = "Heck";
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", clientSecret);
        yield return request.SendWebRequest();
        debug.text = "Hi";
        // 데이터 로드 실패시
        switch (request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                debug.text = "CE";
                Debug.Log("CE");
                yield break;
            case UnityWebRequest.Result.ProtocolError:
                debug.text = "PE";
                Debug.Log("PE");
                yield break;
            case UnityWebRequest.Result.DataProcessingError:
                debug.text = "DP";
                Debug.Log("DP");
                yield break;
            case UnityWebRequest.Result.Success:
                debug.text = "S";
                Debug.Log("S");
                break;
        }
        // 데이터 로드 성공시
        if (request.isDone)
        {
            //string json = request.downloadHandler.text;
            //print(json);
            debug.text = "YA";
            Debug.Log("Ya");


            mapRawImage.texture = DownloadHandlerTexture.GetContent(request);
        }
    }
}
