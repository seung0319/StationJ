using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class FromChecker : MonoBehaviour
{
    public GameObject markerPanel;
    public GameObject categoryPanel;
    public DirectionManager directionManager;
    public GameObject infoPanel;
    public POIInfoPanelManager displayPOI;
    public GameObject destinationMarker;
    public GameObject selectedMarker;
    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.instance.fromPhodocent)
        {
            //markerPanel.SetActive(false);
            //categoryPanel.SetActive(false);
            directionManager.gameObject.SetActive(true);
            displayPOI.SetPanel(DataManager.instance.selectedPoi);
            infoPanel.SetActive(true);
            DestinationMarkerMove();
            
            DataManager.instance.fromPhodocent = false;
        }
    }

    void DestinationMarkerMove()
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

        // 위도, 경도를 x, y로 변환
        double x = (longitude - originLongitude) * xRatio;
        double y = (latitude - originLatitude) * yRatio;

        destinationMarker.GetComponent<RectTransform>().anchoredPosition = new Vector2((float)x, (float)y);
    }
}
