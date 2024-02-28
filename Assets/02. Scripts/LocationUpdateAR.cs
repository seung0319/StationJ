using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationUpdateAR : MonoBehaviour
{
    public GameObject playerMarker;
    public RectTransform map;
    public GameObject selectedMarker;

    void Start()
    {
        double latitude = DataManager.instance.selectedPoi.latitude;
        double longitude = DataManager.instance.selectedPoi.longitude;

        // ���� ����, �浵
        double originLatitude = 37.713675f;
        double originLongitude = 126.743572f;
        // ����η°��߿� 37.713675f; 126.743572f;

        // ����, �浵�� ���� x, y�� ��ȭ ����
        double xRatio = 559092.4f;
        double yRatio = 714178.2f;


        double x = (longitude - originLongitude) * xRatio;
        double y = (latitude - originLatitude) * yRatio;

        selectedMarker.GetComponent<RectTransform>().anchoredPosition = new Vector2((float)x, (float)y);
        StartCoroutine(UpdateLocation());
    }

    IEnumerator UpdateLocation()
    {
        // ��ġ ���񽺸� ����
        Input.location.Start(1, 1);
        // �ʱ� ��ġ ���� ������Ʈ�� ���� �÷���

        YieldInstruction delay = new WaitForSeconds(1);
        yield return new WaitUntil(() => Input.location.lastData.latitude != 0);

        while (true)
        {
            // ��ġ ������ �޾ƿ�
            double latitude = Input.location.lastData.latitude;
            double longitude = Input.location.lastData.longitude;

            // ���� ����, �浵
            double originLatitude = 37.713675f;
            double originLongitude = 126.743572f;
            // ����η°��߿� 37.713675f; 126.743572f;

            // ����, �浵�� ���� x, y�� ��ȭ ����
            double xRatio = 559092.4f;
            double yRatio = 714178.2f;

            double x = (longitude - originLongitude) * xRatio;
            double y = (latitude - originLatitude) * yRatio;

            // GameObject�� ��ġ�� ����
            playerMarker.GetComponent<RectTransform>().anchoredPosition = new Vector2((float)x, (float)y); // Y ��ǥ�� �ʿ信 ���� ����
            map.anchoredPosition = -playerMarker.GetComponent<RectTransform>().localPosition;

            yield return delay;
        }
    }
}
