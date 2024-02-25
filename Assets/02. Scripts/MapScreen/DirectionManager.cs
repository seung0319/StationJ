using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class DirectionManager : MonoBehaviour
{
    [SerializeField] string baseUrl = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving";
    [SerializeField] string clientID = "w9g29xtrwz";
    [SerializeField] string clientSecret = "nTkuiJ3MlFuKyoVz2HXpnwXFR1J3vFgOYpIRJApm";
    [SerializeField] string startLatitude = "37.713675";
    [SerializeField] string startLongitude = "126.743572";
    
    [SerializeField] string option = "traoptimal";

    public static string destLatitude = "";
    public static string destLongitude = "";

    private void OnEnable()
    {
        StartCoroutine(OnEnableCo());
    }

    IEnumerator OnEnableCo()
    {
        //"https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving?start={출발지}&goal={목적지}&option={탐색옵션}" \
        string apiRequestURL = $"{baseUrl}?start={startLongitude},{startLatitude}&goal={destLongitude},{destLatitude}&option={option}";

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
            string json = request.downloadHandler.text;
            //print(json);
            // Resources 폴더에 json.txt 파일로 저장
            string path = Path.Combine(Application.dataPath, "04. Resources/path.json");
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(json);
            }
        }
    }
}
