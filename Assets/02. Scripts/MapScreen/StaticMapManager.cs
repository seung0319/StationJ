using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// Naver StaticMap API 에서 데이터를 받아오는 클래스
/// </summary>
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

    // MapScreen 씬이 실행되면 코루틴을 시작한다.
    public void Start()
    {
        StartCoroutine(MapLoader());
    }

    // Naver StaticMap API에서 데이터를 이미지 텍스쳐로 받아오는 코루틴
    // UI Image 의 Raw Image 컴포넌트에 삽입 됨.
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
