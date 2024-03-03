using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// DataManager���� �޾ƿ� ��ο� �°� ������ �����ϴ� Ŭ����
/// </summary>
public class RouteManager : MonoBehaviour
{
    public GameObject directionManager;
    public GameObject path;
    public GameObject pathPrefab;
    public GameObject parentMap;
    public RectTransform canvasRect; // ��ȯ ����� UI Canvas�� RectTransform
    public Image linePrefab;
    private List<Image> lines = new List<Image>(); // ������ ���ε��� ����Ʈ
    private List<GameObject> paths = new List<GameObject>();
    public Text debugger;
    public GameObject selectedMarker;
    public Text durationText;
    public Text distanceText;

    // RouteManger ������Ʈ�� Ȱ��ȭ �Ǹ� ����Ǵ� �Լ�
    // Map �� �ڽĿ� ���� �̹������� �����Ѵ�.
    private void OnEnable()
    {
        RectTransform lastPoint = null;

        selectedMarker.gameObject.SetActive(true);
        durationText.text = Mathf.RoundToInt(DataManager.instance.duration / 60000).ToString() + " ��";
        distanceText.text = DataManager.instance.distance.ToString() + " m";
        foreach (var points in DataManager.instance.paths)
        {
            // ���� ����, �浵
            double originLatitude = 37.713675f;
            double originLongitude = 126.743572f;
            // ����η°��߿� 37.713675f; 126.743572f;

            // ����, �浵�� ���� x, y�� ��ȭ ����
            double xRatio = 559092.4f;
            double yRatio = 714178.2f;

            // ����, �浵�� x, y�� ��ȯ
            double x = (points.longitude - originLongitude) * xRatio;
            double y = (points.latitude - originLatitude) * yRatio;

            path = Instantiate(pathPrefab, new Vector3((float)x, (float)y, 0), Quaternion.identity);
            path.transform.SetParent(parentMap.transform, false);
            paths.Add(path);
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

                lines.Add(lineImage);
            }
            lastPoint = path.GetComponent<RectTransform>();
        }
    }

    // RouteManager ������Ʈ�� ��Ȱ��ȭ�Ǹ�
    // �����Ǿ��� ��ο� ���� �̹����� �����ȴ�.
    private void OnDisable()
    {
        selectedMarker.gameObject.SetActive(false);
        foreach (var path in paths)
        {
            Destroy(path.gameObject);
        }
        paths.Clear();
        foreach (var line in lines)
        {
            Destroy(line.gameObject);
        }
        lines.Clear();
    }
}
