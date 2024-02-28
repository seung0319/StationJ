using Google.XR.ARCoreExtensions;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
/// <summary>
/// AR 네비게이션 씬에 경로와 POI 오브젝트 생성하는 클래스
/// </summary>
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
    [SerializeField] GameObject goalPrefab;

    [SerializeField] ARSession arSession;

    void Start()
    {
        //씬이 시작되면 코루틴 실행
        StartCoroutine(PathPoiCreat());
    }


    IEnumerator PathPoiCreat()
    {
        //earthManager클래스 함수 사용조건인 지구추적상태가 TrackingState.Tracking상태가 될때까지 대기
        yield return new WaitUntil(() => earthManager.EarthTrackingState == TrackingState.Tracking);

        //지구추적상태가 TrackingState.Tracking임에도 실제 땅을인식(?)해야만 좌표가 생성되기에 대기
        yield return new WaitForSeconds(3f);

        //POI 리스트에 있는 poi생성 및 배치
        foreach (POI poi in DataManager.instance.poiList.pois)
        {
            //GeospatialPose 구조체에 poi 위도 경도 초기화
            poiGPS.Latitude = poi.latitude;
            poiGPS.Longitude = poi.longitude;

            //earthManager.Convert(poiGPS); 라는 함수를 통해 실제 위도경도를 pose로 리턴(치환)
            poiPose = earthManager.Convert(poiGPS);
            //이때 고도가 없어서 y 0으로 초기화
            poiPose.position.y = 0;

            //오브젝트 인스턴싱
            GameObject poiInfoObject = Instantiate(poiInfoPrefab, poiPose.position, poiPose.rotation);
            //인스턴싱한 오브젝트에 poi데이터 삽입
            poiInfoObject.GetComponent<POIData>().SetData(poi);
            //poi 버튼을 누르면 팝업창의 참조를 전달 (지금 이 오브젝트에 참조해놨음)
            poiInfoObject.GetComponent<POIData>().SetpoiCouponButton(this);
            //poi 버튼이 항상 나를 바라보게 하기위해 카메라의 참조를 전달 (지금 이 오브젝트에 참조해놨음)
            poiInfoObject.GetComponent<PoiRotationSet>().SetPlayerTransform(playerTransform);
        }

        //경로 오브젝트 저장을 위한 배열 초기화
        pathObjects = new Vector3[DataManager.instance.paths.Length + 1];
        //경로의 첫위치를 내 현재 위치로 저장
        pathObjects[0] = new Vector3(0, -1.6f, 0);

        //계산한 경로들 하나하나 오브젝트 생성해서 배열에 추가
        for (int i = 1; i < pathObjects.Length; i++)
        {
            //싱글톤에서 받아온 경로들을 GeospatialPose 구조체에 초기화
            poiGPS.Latitude = DataManager.instance.paths[i - 1].latitude;
            poiGPS.Longitude = DataManager.instance.paths[i - 1].longitude;

            //변환
            poiPose = earthManager.Convert(poiGPS);
            //고도가 없기에 발밑을 표현하기 위해 160cm 빼기
            poiPose.position.y = -1.6f;

            //경로 오브젝트(앵커) 생성
            Instantiate(pathPrefab, poiPose.position, poiPose.rotation);

            //생성한 오브젝트 위치저장
            pathObjects[i] = poiPose.position;
        }

        //경로 2개당 하나의 라인랜더러 생성 ((n-1)개 생성) (하나로 하면 안이쁨)
        for (int i = 0; i < pathObjects.Length - 1; i++)
        {
            //생성
            LineRenderer lineRendererObject = Instantiate(lineRenderer);
            //라인랜더러의 시작점과 끝점 초기화
            lineRendererObject.SetPosition(0, pathObjects[i]);
            lineRendererObject.SetPosition(1, pathObjects[i + 1]);
            //90도 회전시켜서 바닥에 깔린 효과주기(카메라와 라인랜더러가 수직일때만 바닥에 깔린듯 보임)
            lineRendererObject.transform.eulerAngles = new Vector3(90f, 0, 0);
        }

        //경로 하나하나 마다 몇M후 좌회전,우회전 오브젝트 생성
        for (int i = 1; i < pathObjects.Length - 1; i++)
        {
            //물체의 외적을 계산해 왼쪽,오른쪽에 있는지 판별
            Vector3 firstPoint = pathObjects[i - 1];
            Vector3 secondPoint = pathObjects[i];
            Vector3 thirdPoint = pathObjects[i + 1];

            Vector3 forward = secondPoint - firstPoint;
            Vector3 toThirdPoint = thirdPoint - firstPoint;
            Vector3 cross = Vector3.Cross(forward, toThirdPoint);

            //경로가 드라이빙 경로다 보니까 곡선같은 느낌을 주기위해 아주 짧은 그리고 살짝 틀어진 경로가 있어서
            //이곳에는 화살표를 생성하지 않기위해 내적을 계산해 제한
            Vector3 forward2 = secondPoint - firstPoint;
            Vector3 toThirdPoint2 = thirdPoint - secondPoint;

            float dot = Vector3.Dot(forward2.normalized, toThirdPoint2.normalized);
            float angle = Mathf.Acos(dot);
            float angleDegrees = angle * Mathf.Rad2Deg;

            //조건문
            //오브젝트 생성과 몇M후를 나타내기위한 카메라 위치참조 전달
            if (angleDegrees >= 75f && angleDegrees <= 105f)
            {
                if (cross.y > 0)
                {
                    GameObject L = Instantiate(LO,
                        new Vector3(pathObjects[i].x, 1.6f, pathObjects[i].z), Quaternion.identity);
                    L.transform.LookAt(firstPoint);
                    L.GetComponent<RemainingDistance>().PlayerTransformSet(playerTransform);
                }
                else if (cross.y < 0)
                {
                    GameObject R = Instantiate(RO,
                        new Vector3(pathObjects[i].x, 1.6f, pathObjects[i].z), Quaternion.identity);
                    R.transform.LookAt(firstPoint);
                    R.GetComponent<RemainingDistance>().PlayerTransformSet(playerTransform);
                }
            }

            //목적지 표시 오브젝트 생성
            GameObject goalObject = Instantiate(goalPrefab,
                new Vector3(pathObjects[pathObjects.Length - 1].x, 1.6f, pathObjects[pathObjects.Length - 1].z)
                , Quaternion.identity);
            //목적지도 항상 사용자를 바라보게 하기위해 카메라위치 참조 전달
            goalObject.GetComponent<PoiRotationSet>().SetPlayerTransform(playerTransform);
        }
    }

    //ARSession에 추적정보 초기화 함수
    public void ArSessionDestroy()
    {
        //ARSession에 추적정보 초기화
        arSession.Reset();
    }
}
