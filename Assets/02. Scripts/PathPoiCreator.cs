using Google.XR.ARCoreExtensions;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
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

        //��� ����Ѱ� ����

        //POI ���� ��ġ��ġ
        foreach(POI poi in DataManager.instance.poiList.pois)
        {
            poiGPS.Latitude = poi.latitude;
            poiGPS.Longitude = poi.longitude;

            poiPose = earthManager.Convert(poiGPS);
            poiPose.position.y = 0;

            GameObject poiInfoObject = Instantiate(poiInfoPrefab,poiPose.position,poiPose.rotation);
            poiInfoObject.GetComponent<POIData>().SetData(poi);
            poiInfoObject.GetComponent<POIData>().SetpoiCouponButton();
        }

        //Ȥ�� �� ���� ����
        pathObjects = new Vector3[DataManager.instance.paths.Length+1];
        pathObjects[0] = new Vector3(0, 0, 0);

        //foreach ((double latitude, double longitude) path in DataManager.instance.paths)
        //{
        //    poiGPS.Latitude = path.latitude;
        //    poiGPS.Longitude = path.longitude;

        //    poiPose = earthManager.Convert(poiGPS);
        //    poiPose.position.y = -1.6f;

        //    Vector3 pathObject = Instantiate(pathPrefab, poiPose.position, poiPose.rotation).transform.position;
        //    pathObjects.Add(pathObject);
        //}

        //����� ��ε� �ϳ��ϳ� ������Ʈ �����ؼ� �迭�� �߰�
        for (int i = 1; i < pathObjects.Length; i++)
        {
            poiGPS.Latitude = DataManager.instance.paths[i-1].latitude;
            poiGPS.Longitude = DataManager.instance.paths[i-1].longitude;

            poiPose = earthManager.Convert(poiGPS);
            poiPose.position.y = -1.6f;

            Instantiate(pathPrefab, poiPose.position, poiPose.rotation);
            Vector3 pathObject = poiPose.position;

            pathObjects[i] = pathObject;
        }

        LineRenderer lineRendererObject = Instantiate(lineRenderer);
        lineRendererObject.SetPositions(pathObjects);
        lineRendererObject.transform.eulerAngles = new Vector3 (90f, 0, 0);
    }
}
