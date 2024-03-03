using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// Naver Driving 5 API에서 데이터를 받아오는 클래스
/// </summary>
public class DirectionManager : MonoBehaviour
{
    [SerializeField] string baseUrl = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving";
    [SerializeField] string clientID = "w9g29xtrwz";
    [SerializeField] string clientSecret = "nTkuiJ3MlFuKyoVz2HXpnwXFR1J3vFgOYpIRJApm";
    [SerializeField] string startLatitude = "37.713675";
    [SerializeField] string startLongitude = "126.743572";
    [SerializeField] string option = "traoptimal";

    // 아래 두 항목은 POIButton.cs 에서 초기화 됨
    public static string destLatitude = "";
    public static string destLongitude = "";

    public static string json = "";

    public UnityEvent OnCompleteRead;

    public Text debugger;

    private void Start()
    {
        
    }

    // MapScreen 시작 시 Disable 되어 있는 상태, 마커를 누르면 Enable이 되고 코루틴이 실행됨.
    // 위치권한 허용 상태일 시 유저의 위치정보를 사용, 거부 상태일 시 경기인력개발원 기준.
    private void OnEnable()
    {
        if (Input.location.isEnabledByUser)
        {
            startLatitude = Input.location.lastData.latitude.ToString();
            startLongitude = Input.location.lastData.longitude.ToString();
        }
        StartCoroutine(OnEnableCo());
    }


    // Naver Driving 5 API에서 데이터를 받아오는 코루틴
    IEnumerator OnEnableCo()
    {
        string apiRequestURL = $"{baseUrl}?start={startLongitude},{startLatitude}&goal={destLongitude},{destLatitude}&option={option}";
        print(startLatitude + " " + startLongitude + " , " + destLatitude + " " + destLongitude);
        UnityWebRequest request = UnityWebRequest.Get(apiRequestURL);
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
            json = request.downloadHandler.text;
            DataManager.instance.ParseJson(json);
        }
    }
}
