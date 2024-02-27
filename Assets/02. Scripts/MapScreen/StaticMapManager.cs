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
    [SerializeField] string clientSecret = "nTkuiJ3MlFuKyoVz2HXpnwXFR1J3vFgOYpIRJApm";
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
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(apiRequestURL);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", clientSecret);
        yield return request.SendWebRequest();
        // 데이터 로드 실패시
        switch (request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                yield break;
            case UnityWebRequest.Result.ProtocolError:
                yield break;
            case UnityWebRequest.Result.DataProcessingError:
                yield break;
            case UnityWebRequest.Result.Success:
                break;
        }
        // 데이터 로드 성공시
        if (request.isDone)
        {
            mapRawImage.texture = DownloadHandlerTexture.GetContent(request);
        }
    }
}
