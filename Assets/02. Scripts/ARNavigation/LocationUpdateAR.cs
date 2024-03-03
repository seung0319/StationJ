using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ARNavigation 씬의 미니맵에 유저의 위치정보를 업데이트 하기 위한 클래스
/// </summary>
public class LocationUpdateAR : MonoBehaviour
{
    public GameObject playerMarker;
    public RectTransform map;
    public GameObject selectedMarker;

    // ARNavigation 씬 진입 시 DataManager의 selectedPoi(목적지)의 정보를 받아 목적지를 미니맵에 출력
    // 이후 유저 위치를 업데이트 하는 코루틴 실행
    void Start()
    {
        double latitude = DataManager.instance.selectedPoi.latitude;
        double longitude = DataManager.instance.selectedPoi.longitude;

        // 기준 위도, 경도
        double originLatitude = 37.713675f;
        double originLongitude = 126.743572f;
        // 경기인력개발원 37.713675f; 126.743572f;

        // 위도, 경도에 대한 x, y의 변화 비율
        double xRatio = 559092.4f;
        double yRatio = 714178.2f;


        double x = (longitude - originLongitude) * xRatio;
        double y = (latitude - originLatitude) * yRatio;

        selectedMarker.GetComponent<RectTransform>().anchoredPosition = new Vector2((float)x, (float)y);
        StartCoroutine(UpdateLocation());
    }

    // 유저 위치는 허용이 되어있는것으로 간주하기 때문에,
    // 코루틴을 활용하여 1초마다 유저 위치 업데이트
    IEnumerator UpdateLocation()
    {
        // 위치 서비스를 시작
        Input.location.Start(1, 1);
        // 초기 위치 정보 업데이트를 위한 플래그

        YieldInstruction delay = new WaitForSeconds(1);
        yield return new WaitUntil(() => Input.location.lastData.latitude != 0);

        while (true)
        {
            // 위치 정보를 받아옴
            double latitude = Input.location.lastData.latitude;
            double longitude = Input.location.lastData.longitude;

            // 기준 위도, 경도
            double originLatitude = 37.713675f;
            double originLongitude = 126.743572f;
            // 경기인력개발원 37.713675f; 126.743572f;

            // 위도, 경도에 대한 x, y의 변화 비율
            double xRatio = 559092.4f;
            double yRatio = 714178.2f;

            double x = (longitude - originLongitude) * xRatio;
            double y = (latitude - originLatitude) * yRatio;

            // GameObject의 위치를 변경
            playerMarker.GetComponent<RectTransform>().anchoredPosition = new Vector2((float)x, (float)y); // Y 좌표는 필요에 따라 변경
            map.anchoredPosition = -playerMarker.GetComponent<RectTransform>().localPosition;

            yield return delay;
        }
    }
}
