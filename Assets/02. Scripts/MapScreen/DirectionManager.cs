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

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        if (Input.location.isEnabledByUser)
        {
            startLatitude = Input.location.lastData.latitude.ToString();
            startLongitude = Input.location.lastData.longitude.ToString();
        }
        StartCoroutine(OnEnableCo());
        // ��ġ ���񽺰� Ȱ��ȭ �Ǿ� �ִ��� Ȯ��
    }

    IEnumerator OnEnableCo()
    {
        string apiRequestURL = $"{baseUrl}?start={startLongitude},{startLatitude}&goal={destLongitude},{destLatitude}&option={option}";

        UnityWebRequest request = UnityWebRequest.Get(apiRequestURL);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", clientSecret);
        yield return request.SendWebRequest();
        // ������ �ε� ���н�
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
        // ������ �ε� ������
        if (request.isDone)
        {
            //debugger.text = "Success";
            json = request.downloadHandler.text;
            DataManager.instance.ParseJson(json);
        }
    }
}
