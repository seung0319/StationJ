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
    bool isFirstLocationUpdate = true;

    void Start()
    {
        StartCoroutine(UpdateLocation());
    }

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
            
            // GameObject의 위치를 변경
            playerMarker.GetComponent<RectTransform>().anchoredPosition = DataManager.instance.MapRatio(latitude, longitude); // Y 좌표는 필요에 따라 변경
   
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
