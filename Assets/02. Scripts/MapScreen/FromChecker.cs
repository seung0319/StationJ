using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ HomeScreen���� MapScreen���� �Ѿ�Դ���,
/// PDListScreen���� MapScreen���� �Ѿ�Դ��� Ȯ���ϴ� Ŭ����.
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


    // PDListScreen���� �Ѿ�� ��쿡�� ����
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

    // MapScreen�� �󼼼��� ȭ�鿡�� ��ã�� ��ư�� ���� �Ͱ� ���� �۾��� ����.
    // (Driving API ���� �޾ƿ� �����͸� ����ؼ� �� ǥ��)
    IEnumerator LoadPath()
    {
        yield return new WaitUntil(() => DataManager.instance.paths != null);

        playerMarker.SetActive(true);
        routeManager.SetActive(true);
        routeFindPanel.SetActive(true);
    }


    // ���� ��Ŀ�� ����Ƽ���忡���� ��ǥ�� �������� �����ϴ� �ڵ�
    void DestinationMarkerMove()
    {
        double latitude = DataManager.instance.selectedPoi.latitude;
        double longitude = DataManager.instance.selectedPoi.longitude;

        DirectionManager.destLatitude = latitude.ToString();
        DirectionManager.destLongitude = longitude.ToString();

        // ���� ����, �浵
        double originLatitude = 37.713675f;
        double originLongitude = 126.743572f;
        // ����η°��߿� 37.713675f; 126.743572f;

        // ����, �浵�� ���� x, y�� ��ȭ ����
        double xRatio = 559092.4f;
        double yRatio = 714178.2f;

        // ����, �浵�� x, y�� ��ȯ
        double x = (longitude - originLongitude) * xRatio;
        double y = (latitude - originLatitude) * yRatio;

        destinationMarker.GetComponent<RectTransform>().anchoredPosition = new Vector2((float)x, (float)y);
    }
}
