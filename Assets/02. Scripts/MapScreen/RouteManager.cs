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
    public RectTransform canvasRect; // ��ȯ ����� UI Canvas�� RectTransform
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

            // ���� ����, �浵
            double originLatitude = 37.713675f;
            double originLongitude = 126.743572f;
            // ����η°��߿� 37.713675f; 126.743572f;

            // ���� x, y
            double originX = 0;
            double originY = 0;

            // ����, �浵�� ���� x, y�� ��ȭ ����
            double xRatio = 559092.4f;
            double yRatio = 714178.2f;
            //Debug.Log(xRatio + " " + yRatio);
            //559092.4 714178.2
            // ����, �浵�� x, y�� ��ȯ
            double x = originX + (points.longitude - originLongitude) * xRatio;
            double y = originY + (points.latitude - originLatitude) * yRatio;

            path = Instantiate(pathPrefab, new Vector3((float)x, (float)y, 0), Quaternion.identity);
            path.transform.SetParent(parentMap.transform, false);

            if (lastPoint != null)
            {
                // ���ο� Image ������Ʈ�� ����
                Image lineImage = Instantiate(linePrefab, canvasRect);
                lineImage.transform.SetParent(parentMap.transform, false);
                // ���Ӱ� ������ Image ������Ʈ�� RectTransform�� ����
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
