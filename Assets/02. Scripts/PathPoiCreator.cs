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

    [SerializeField] GameObject couponPanel;
    [SerializeField] Text poiNameText;
    [SerializeField] Text poiAddressText;
    [SerializeField] Text poiOpeningHoursText;
    [SerializeField] Text poiCouponText;

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

        //POI 생성 위치배치
        foreach(POI poi in DataManager.instance.poiList.pois)
        {
            poiGPS.Latitude = poi.latitude;
            poiGPS.Longitude = poi.longitude;

            poiPose = earthManager.Convert(poiGPS);
            poiPose.position.y = 0;

            GameObject poiInfoObject = Instantiate(poiInfoPrefab,poiPose.position,poiPose.rotation);
            poiInfoObject.GetComponent<POIData>().SetData(poi);
            poiInfoObject.GetComponent<POIData>().SetpoiCouponButton(
                couponPanel, poiNameText, poiAddressText, poiOpeningHoursText, poiCouponText);
        }

        pathObjects = new Vector3[DataManager.instance.paths.Length+1];
        pathObjects[0] = new Vector3(0, -1.6f, 0);

        //계산한 경로들 하나하나 오브젝트 생성해서 배열에 추가
        for (int i = 1; i < pathObjects.Length; i++)
        {
            poiGPS.Latitude = DataManager.instance.paths[i-1].latitude;
            poiGPS.Longitude = DataManager.instance.paths[i-1].longitude;

            poiPose = earthManager.Convert(poiGPS);
            poiPose.position.y = -1.6f;

            Instantiate(pathPrefab, poiPose.position, poiPose.rotation);

            pathObjects[i] = poiPose.position;
        }

        for (int i = 0;i < pathObjects.Length-1; i++)
        {
            LineRenderer lineRendererObject = Instantiate(lineRenderer);
            lineRenderer.SetPosition(0, pathObjects[i]);
            lineRenderer.SetPosition(1, pathObjects[i + 1]);
            lineRendererObject.transform.eulerAngles = new Vector3(90f, 0, 0);
        }
    }
}
