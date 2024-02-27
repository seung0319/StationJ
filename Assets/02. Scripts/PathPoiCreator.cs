using Google.XR.ARCoreExtensions;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;

public class PathPoiCreator : MonoBehaviour
{
    [SerializeField] AREarthManager earthManager;

    GeospatialPose poiGPS = new GeospatialPose();
    Pose poiPose;

    [SerializeField] GameObject poiInfoPrefab;
    [SerializeField] GameObject pathPrefab;

    Vector3[] pathObjects;
    [SerializeField] LineRenderer lineRenderer;

    public GameObject couponPanel;
    public Text poiNameText;
    public Text poiAddressText;
    public Text poiOpeningHoursText;
    public Text poiCouponText;

    [SerializeField] Transform playerTransform;

    [SerializeField] GameObject RO;
    [SerializeField] GameObject LO;

    void Start()
    {
        if(DataManager.instance.poiList.pois != null)
        {
            StartCoroutine(PathPoiCreat());
        }
    }

    IEnumerator PathPoiCreat()
    {
        yield return new WaitUntil(() => earthManager.EarthTrackingState == TrackingState.Tracking);

        yield return new WaitForSeconds(3f);

        //POI ���� ��ġ��ġ
        foreach (POI poi in DataManager.instance.poiList.pois)
        {
            poiGPS.Latitude = poi.latitude;
            poiGPS.Longitude = poi.longitude;

            poiPose = earthManager.Convert(poiGPS);
            poiPose.position.y = 0;

            GameObject poiInfoObject = Instantiate(poiInfoPrefab, poiPose.position, poiPose.rotation);
            poiInfoObject.GetComponent<POIData>().SetData(poi);
            poiInfoObject.GetComponent<POIData>().SetpoiCouponButton(this);
            poiInfoObject.GetComponent<PoiRotationSet>().SetPlayerTransform(playerTransform);
        }

        pathObjects = new Vector3[DataManager.instance.paths.Length+1];
        pathObjects[0] = new Vector3(0, -1.6f, 0);

        //����� ��ε� �ϳ��ϳ� ������Ʈ �����ؼ� �迭�� �߰�
        for (int i = 1; i < pathObjects.Length; i++)
        {
            poiGPS.Latitude = DataManager.instance.paths[i-1].latitude;
            poiGPS.Longitude = DataManager.instance.paths[i-1].longitude;

            poiPose = earthManager.Convert(poiGPS);
            poiPose.position.y = -1.6f;

            Instantiate(pathPrefab, poiPose.position, poiPose.rotation);

            pathObjects[i] = poiPose.position;
        }

        for (int i = 0; i < pathObjects.Length - 1; i++)
        {
            LineRenderer lineRendererObject = Instantiate(lineRenderer);
            lineRendererObject.SetPosition(0, pathObjects[i]);
            lineRendererObject.SetPosition(1, pathObjects[i + 1]);
            lineRendererObject.transform.eulerAngles = new Vector3(90f, 0, 0);
        }

        for (int i = 1; i < pathObjects.Length - 1; i++)
        {
            Vector3 firstPoint = pathObjects[i - 1];
            Vector3 secondPoint = pathObjects[i];
            Vector3 thirdPoint = pathObjects[i + 1];

            // ù ��° ������ �� ��° ���� ���ϴ� ����
            Vector3 forward = secondPoint - firstPoint;
            // ù ��° ������ �� ��° ���� ���ϴ� ����
            Vector3 toThirdPoint = thirdPoint - firstPoint;

            // �� ������ ����
            Vector3 cross = Vector3.Cross(forward, toThirdPoint);

            // �� ������ ����
            float dot = Vector3.Dot(forward.normalized, toThirdPoint.normalized);
            // �� ���Ͱ� �̷�� �� (����)
            float angle = Mathf.Acos(dot);
            // ������ ���� ��ȯ
            float angleDegrees = angle * Mathf.Rad2Deg;

            if (angleDegrees >= 75f && angleDegrees <= 105f)
            {
                if (cross.y > 0)
                {
                    GameObject L = Instantiate(LO,
                        new Vector3(pathObjects[i].x, 1.6f, pathObjects[i].z), Quaternion.identity);
                    L.transform.LookAt(firstPoint);
                }
                else if (cross.y < 0)
                {
                    GameObject R = Instantiate(RO,
                        new Vector3(pathObjects[i].x, 1.6f, pathObjects[i].z), Quaternion.identity);
                    R.transform.LookAt(firstPoint);
                }
            }
        }
    }
}
