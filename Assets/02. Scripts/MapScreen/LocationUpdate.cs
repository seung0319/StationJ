using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

/// <summary>
/// 유저의 위치를 1초마다 업데이트 해주는 클래스
/// </summary>
public class LocationUpdate : MonoBehaviour
{
    public GameObject playerMarker;
    public RectTransform map;
    public Text deb1;
    public Text deb2;
    bool isFirstLocationUpdate = true;

    // 위치 권한이 허용 상태일 때 오브젝트가 활성화 되고, 코루틴이 실행 됨
    void OnEnable()
    {
        StartCoroutine(UpdateLocation());
    }


    // 유저의 위치를 1초마다 업데이트하고 유저마커의 위치를 1초마다 움직이게 하는 코루틴
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
   
            if (isFirstLocationUpdate)
            {
                map.anchoredPosition = -playerMarker.GetComponent<RectTransform>().localPosition;
                deb2.text = map.anchoredPosition.ToString();
                isFirstLocationUpdate = false;
            }

            yield return delay;
        }
    }
}
