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
        selectedMarker.GetComponent<RectTransform>().anchoredPosition = DataManager.instance.MapRatioAR(latitude, longitude);

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

            // GameObject�� ��ġ�� ����
            playerMarker.GetComponent<RectTransform>().anchoredPosition = DataManager.instance.MapRatioAR(latitude, longitude); // Y ��ǥ�� �ʿ信 ���� ����
            map.anchoredPosition = -playerMarker.GetComponent<RectTransform>().localPosition;

            yield return delay;
        }
    }
}
