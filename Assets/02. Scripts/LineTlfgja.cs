using Google.XR.ARCoreExtensions;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;

public class LineTlfgja : MonoBehaviour
{
    [SerializeField] AREarthManager earthManager;

    GeospatialPose poiGPS = new GeospatialPose();
    Pose poiPose;

    [SerializeField] GameObject poiInfoPrefab;
    [SerializeField] GameObject pathPrefab;

    Vector3[] pathObjects;

    public GameObject couponPanel;
    public Text poiNameText;
    public Text poiAddressText;
    public Text poiOpeningHoursText;
    public Text poiCouponText;

    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] GameObject RO;
    [SerializeField] GameObject LO;

    public (double latitude, double longitude)[] paths = new (double latitude, double longitude)[]
    {
        (37.714147,126.744462),
        (37.714394,126.743880),
        (37.714053,126.743606),
        (37.714277,126.740820),
    };

    [SerializeField] Transform player;

    void Start()
    {
        StartCoroutine(PathPoiCreat());
    }

    IEnumerator PathPoiCreat()
    {
        yield return new WaitUntil(() => earthManager.EarthTrackingState == TrackingState.Tracking);

        yield return new WaitForSeconds(3f);

        //POI 생성 위치배치

        poiGPS.Latitude = 37.714278;
        poiGPS.Longitude = 126.744409;

        poiPose = earthManager.Convert(poiGPS);
        poiPose.position.y = 0;
        GameObject poiObject = Instantiate(poiInfoPrefab, poiPose.position, poiPose.rotation);
        poiObject.GetComponent<PoiRotationSet>().SetPlayerTransform(player);
        

        pathObjects = new Vector3[paths.Length + 1];
        pathObjects[0] = new Vector3(0, -1.6f, 0);

        //계산한 경로들 하나하나 오브젝트 생성해서 배열에 추가
        for (int i = 1; i < pathObjects.Length; i++)
        {
            poiGPS.Latitude = paths[i - 1].latitude;
            poiGPS.Longitude = paths[i - 1].longitude;

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

            // 첫 번째 점에서 두 번째 점을 향하는 벡터
            Vector3 forward = secondPoint - firstPoint;
            // 첫 번째 점에서 세 번째 점을 향하는 벡터
            Vector3 toThirdPoint = thirdPoint - firstPoint;

            // 두 벡터의 외적
            Vector3 cross = Vector3.Cross(forward, toThirdPoint);

            if (cross.y > 0)
            {
                GameObject L = Instantiate(LO, 
                    new Vector3(pathObjects[i].x, 1.6f, pathObjects[i].z),Quaternion.identity);
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
