using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouteManager : MonoBehaviour
{
    public GameObject directionManager;
    public GameObject path;
    public GameObject pathPrefab;
    public GameObject parentMap;
    public RectTransform canvasRect; // 변환 대상인 UI Canvas의 RectTransform
    public Image linePrefab;
    RectTransform lastPoint = null;

    public Text debugger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnRouteFindButtonClick()
    {
        GameObject.Find("Selected").transform.Find("selectedMarker").gameObject.SetActive(true);
        //directionManager.SetActive(true);
        DataManager.instance.LoadPath();
        //debugger.text = "Read";
        foreach (var points in DataManager.instance.paths)
        {
            //Debug.Log($"Longitude: {points.longitude}, Latitude: {points.latitude}");

            // 기준 위도, 경도
            double originLatitude = 37.713675f;
            double originLongitude = 126.743572f;
            // 경기인력개발원 37.713675f; 126.743572f;

            // 기준 x, y
            double originX = 0;
            double originY = 0;

            // 위도, 경도에 대한 x, y의 변화 비율
            double xRatio = 559092.4f;
            double yRatio = 714178.2f;
            //Debug.Log(xRatio + " " + yRatio);
            //559092.4 714178.2
            // 위도, 경도를 x, y로 변환
            double x = originX + (points.longitude - originLongitude) * xRatio;
            double y = originY + (points.latitude - originLatitude) * yRatio;

            path = Instantiate(pathPrefab, new Vector3((float)x, (float)y, 0), Quaternion.identity);
            path.transform.SetParent(parentMap.transform, false);

            if (lastPoint != null)
            {
                // 새로운 Image 오브젝트를 생성
                Image lineImage = Instantiate(linePrefab, canvasRect);
                lineImage.transform.SetParent(parentMap.transform, false);
                // 새롭게 생성된 Image 오브젝트의 RectTransform을 설정
                Vector2 differenceVector = path.transform.position - lastPoint.position;
                lineImage.rectTransform.sizeDelta = new Vector2(differenceVector.magnitude, lineImage.rectTransform.sizeDelta.y);
                lineImage.rectTransform.pivot = new Vector2(0, 0.5f);
                lineImage.rectTransform.position = lastPoint.position;
                float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
                lineImage.rectTransform.rotation = Quaternion.Euler(0, 0, angle);
            }

            lastPoint = path.GetComponent<RectTransform>();
        }
    }
}
