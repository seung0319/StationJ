using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.instance.fromPhodocent)
        {
            //markerPanel.SetActive(false);
            //categoryPanel.SetActive(false);
            //infoPanel.SetActive(true);
            //displayPOI.SetPanel(DataManager.instance.selectedPoi);


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

    IEnumerator LoadPath()
    {
        yield return new WaitUntil(() => DataManager.instance.paths != null);

        playerMarker.SetActive(true);
        routeManager.SetActive(true);
        routeFindPanel.SetActive(true);
    }

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
