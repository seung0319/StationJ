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
        // ��ġ ���񽺰� Ȱ��ȭ �Ǿ� �ִ��� Ȯ��
        if (!Input.location.isEnabledByUser)
            yield break;

        // ��ġ ���񽺸� ����
        Input.location.Start(1, 1);
        // �ʱ� ��ġ ���� ������Ʈ�� ���� �÷���
        bool isFirstLocationUpdate = true;

        while (true)
        {
            // ��ġ ������ �޾ƿ�
            double latitude = Input.location.lastData.latitude;
            double longitude = Input.location.lastData.longitude;
            deb1.text = latitude + " " + longitude;
            // ���� ����, �浵
            double originLatitude = 37.713675f;
            double originLongitude = 126.743572f;
            // ����η°��߿� 37.713675f; 126.743572f;

            // ����, �浵�� ���� x, y�� ��ȭ ����
            double xRatio = 559092.4f;
            double yRatio = 714178.2f;
            //Debug.Log(xRatio + " " + yRatio);
            //559092.4 714178.2
            // ����, �浵�� x, y�� ��ȯ
            double x = (longitude - originLongitude) * xRatio;
            double y = (latitude - originLatitude) * yRatio;
            deb2.text = x + " " + y;

            // GameObject�� ��ġ�� ����
            playerMarker.GetComponent<RectTransform>().anchoredPosition = new Vector2((float)x, (float)y); // Y ��ǥ�� �ʿ信 ���� ����

            if (isFirstLocationUpdate)
            {
                map.anchoredPosition = -playerMarker.GetComponent<RectTransform>().anchoredPosition;
                isFirstLocationUpdate = false;
            }
                

            // 1�� ���
            yield return new WaitForSeconds(1);
        }
    }
}
