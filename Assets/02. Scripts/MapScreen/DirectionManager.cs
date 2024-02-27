using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

    public static string json = "";

    public Text debugger;

    public RouteManager routeManager;

    private void OnEnable()
    {
        //debugger.text = "Enabled";
        StartCoroutine(OnEnableCo());
    }

    IEnumerator OnEnableCo()
    {
        string apiRequestURL = $"{baseUrl}?start={startLongitude},{startLatitude}&goal={destLongitude},{destLatitude}&option={option}";

        UnityWebRequest request = UnityWebRequest.Get(apiRequestURL);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", clientSecret);
        yield return request.SendWebRequest();
        // 데이터 로드 실패시
        switch (request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                //debugger.text = "CE";
                yield break;
            case UnityWebRequest.Result.ProtocolError:
                //debugger.text = "PE";
                yield break;
            case UnityWebRequest.Result.DataProcessingError:
                //debugger.text = "DP";
                yield break;
            case UnityWebRequest.Result.Success:
                break;
        }
        // 데이터 로드 성공시
        if (request.isDone)
        {
            //debugger.text = "Success";
            json = request.downloadHandler.text;
            DataManager.instance.ParseJson(json);


            print(json);
            ///
            /// Json파일로 저장하는 함수
            ///
            // Resources 폴더에 json.txt 파일로 저장
            //string path;
            //if (Application.platform == RuntimePlatform.Android)
            //{
            //    path = Path.Combine(Application.persistentDataPath, "path.json");
            //}
            //else
            //{
            //    path = Path.Combine(Application.dataPath, "04. Resources/path.json");
            //}
            //using (StreamWriter sw = File.CreateText(path))
            //{
            //    //debugger.text = "Wrote";
            //    sw.Write(json);
            //    //debugger.text = "Write";
            //}
        }
    }
}
