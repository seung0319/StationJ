using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 유저가 HomeScreen에서 MapScreen으로 넘어왔는지,
/// PDListScreen에서 MapScreen으로 넘어왔는지 확인하는 클래스.
/// </summary>
public class FromChecker : MonoBehaviour
{
    public GameObject markerPanel;
    public GameObject categoryPanel;
    public DirectionManager directionManager;
    public GameObject infoPanel;
    public POIInfoPanelManager displayPOI;
    public GameObject destinationMarker;
    public GameObject selectedMarker;
    public GameObject locationUpdater;
    public GameObject playerMarker;
    public GameObject routeManager;
    public GameObject routeFindPanel;
    public Text endPointText;


    // PDListScreen에서 넘어온 경우에만 실행
    void Start()
    {
        if (DataManager.instance.fromPhodocent)
        {
            DestinationMarkerMove();
            directionManager.gameObject.SetActive(true);
            locationUpdater.SetActive(true);
            markerPanel.SetActive(false);
            categoryPanel.SetActive(false);
            endPointText.text = DataManager.instance.selectedPoi.name;

            StartCoroutine(LoadPath());

            DataManager.instance.fromPhodocent = false;
        }
    }

    // MapScreen의 상세설명 화면에서 길찾기 버튼을 누른 것과 같은 작업을 실행.
    // (Driving API 에서 받아온 데이터를 사용해서 길 표시)
    IEnumerator LoadPath()
    {
        yield return new WaitUntil(() => DataManager.instance.paths != null);

        playerMarker.SetActive(true);
        routeManager.SetActive(true);
        routeFindPanel.SetActive(true);
    }


    // 도착 마커의 유니티월드에서의 좌표를 도착지로 변경하는 코드
    void DestinationMarkerMove()
    {
        double latitude = DataManager.instance.selectedPoi.latitude;
        double longitude = DataManager.instance.selectedPoi.longitude;

        DirectionManager.destLatitude = latitude.ToString();
        DirectionManager.destLongitude = longitude.ToString();

        // 기준 위도, 경도
        double originLatitude = 37.713675f;
        double originLongitude = 126.743572f;
        // 경기인력개발원 37.713675f; 126.743572f;

        // 위도, 경도에 대한 x, y의 변화 비율
        double xRatio = 559092.4f;
        double yRatio = 714178.2f;

        // 위도, 경도를 x, y로 변환
        double x = (longitude - originLongitude) * xRatio;
        double y = (latitude - originLatitude) * yRatio;

        destinationMarker.GetComponent<RectTransform>().anchoredPosition = new Vector2((float)x, (float)y);
    }
}
