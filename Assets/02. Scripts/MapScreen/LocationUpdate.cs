using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class LocationUpdate : MonoBehaviour
{
    public GameObject playerMarker;
    public RectTransform map;
    public Text deb1;
    public Text deb2;

    void Start()
    {
        StartCoroutine(UpdateLocation());
    }

    IEnumerator UpdateLocation()
    {
        // 위치 서비스가 활성화 되어 있는지 확인
        if (!Input.location.isEnabledByUser)
            yield break;

        // 위치 서비스를 시작
        Input.location.Start(1, 1);
        // 초기 위치 정보 업데이트를 위한 플래그
        bool isFirstLocationUpdate = true;

        while (true)
        {
            // 위치 정보를 받아옴
            double latitude = Input.location.lastData.latitude;
            double longitude = Input.location.lastData.longitude;
            deb1.text = latitude + " " + longitude;
            // 기준 위도, 경도
            double originLatitude = 37.713675f;
            double originLongitude = 126.743572f;
            // 경기인력개발원 37.713675f; 126.743572f;

            // 위도, 경도에 대한 x, y의 변화 비율
            double xRatio = 559092.4f;
            double yRatio = 714178.2f;
            //Debug.Log(xRatio + " " + yRatio);
            //559092.4 714178.2
            // 위도, 경도를 x, y로 변환
            double x = (longitude - originLongitude) * xRatio;
            double y = (latitude - originLatitude) * yRatio;
            deb2.text = x + " " + y;

            // GameObject의 위치를 변경
            playerMarker.GetComponent<RectTransform>().anchoredPosition = new Vector2((float)x, (float)y); // Y 좌표는 필요에 따라 변경

            if (isFirstLocationUpdate)
            {
                map.anchoredPosition = -playerMarker.GetComponent<RectTransform>().anchoredPosition;
                isFirstLocationUpdate = false;
            }
                

            // 1초 대기
            yield return new WaitForSeconds(1);
        }
    }
}
