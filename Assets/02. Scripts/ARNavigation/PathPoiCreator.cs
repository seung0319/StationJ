using Google.XR.ARCoreExtensions;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
/// <summary>
/// AR �׺���̼� ���� ��ο� POI ������Ʈ �����ϴ� Ŭ����
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
        //���� ���۵Ǹ� �ڷ�ƾ ����
        StartCoroutine(PathPoiCreat());
    }


    IEnumerator PathPoiCreat()
    {
        //earthManagerŬ���� �Լ� ��������� �����������°� TrackingState.Tracking���°� �ɶ����� ���
        yield return new WaitUntil(() => earthManager.EarthTrackingState == TrackingState.Tracking);

        //�����������°� TrackingState.Tracking�ӿ��� ���� �����ν�(?)�ؾ߸� ��ǥ�� �����Ǳ⿡ ���
        yield return new WaitForSeconds(3f);

        //POI ����Ʈ�� �ִ� poi���� �� ��ġ
        foreach (POI poi in DataManager.instance.poiList.pois)
        {
            //GeospatialPose ����ü�� poi ���� �浵 �ʱ�ȭ
            poiGPS.Latitude = poi.latitude;
            poiGPS.Longitude = poi.longitude;

            //earthManager.Convert(poiGPS); ��� �Լ��� ���� ���� �����浵�� pose�� ����(ġȯ)
            poiPose = earthManager.Convert(poiGPS);
            //�̶� ���� ��� y 0���� �ʱ�ȭ
            poiPose.position.y = 0;

            //������Ʈ �ν��Ͻ�
            GameObject poiInfoObject = Instantiate(poiInfoPrefab, poiPose.position, poiPose.rotation);
            //�ν��Ͻ��� ������Ʈ�� poi������ ����
            poiInfoObject.GetComponent<POIData>().SetData(poi);
            //poi ��ư�� ������ �˾�â�� ������ ���� (���� �� ������Ʈ�� �����س���)
            poiInfoObject.GetComponent<POIData>().SetpoiCouponButton(this);
            //poi ��ư�� �׻� ���� �ٶ󺸰� �ϱ����� ī�޶��� ������ ���� (���� �� ������Ʈ�� �����س���)
            poiInfoObject.GetComponent<PoiRotationSet>().SetPlayerTransform(playerTransform);
        }

        //��� ������Ʈ ������ ���� �迭 �ʱ�ȭ
        pathObjects = new Vector3[DataManager.instance.paths.Length + 1];
        //����� ù��ġ�� �� ���� ��ġ�� ����
        pathObjects[0] = new Vector3(0, -1.6f, 0);

        //����� ��ε� �ϳ��ϳ� ������Ʈ �����ؼ� �迭�� �߰�
        for (int i = 1; i < pathObjects.Length; i++)
        {
            //�̱��濡�� �޾ƿ� ��ε��� GeospatialPose ����ü�� �ʱ�ȭ
            poiGPS.Latitude = DataManager.instance.paths[i - 1].latitude;
            poiGPS.Longitude = DataManager.instance.paths[i - 1].longitude;

            //��ȯ
            poiPose = earthManager.Convert(poiGPS);
            //���� ���⿡ �߹��� ǥ���ϱ� ���� 160cm ����
            poiPose.position.y = -1.6f;

            //��� ������Ʈ(��Ŀ) ����
            Instantiate(pathPrefab, poiPose.position, poiPose.rotation);

            //������ ������Ʈ ��ġ����
            pathObjects[i] = poiPose.position;
        }

        //��� 2���� �ϳ��� ���η����� ���� ((n-1)�� ����) (�ϳ��� �ϸ� ���̻�)
        for (int i = 0; i < pathObjects.Length - 1; i++)
        {
            //����
            LineRenderer lineRendererObject = Instantiate(lineRenderer);
            //���η������� �������� ���� �ʱ�ȭ
            lineRendererObject.SetPosition(0, pathObjects[i]);
            lineRendererObject.SetPosition(1, pathObjects[i + 1]);
            //90�� ȸ�����Ѽ� �ٴڿ� �� ȿ���ֱ�(ī�޶�� ���η������� �����϶��� �ٴڿ� �򸰵� ����)
            lineRendererObject.transform.eulerAngles = new Vector3(90f, 0, 0);
        }

        //��� �ϳ��ϳ� ���� ��M�� ��ȸ��,��ȸ�� ������Ʈ ����
        for (int i = 1; i < pathObjects.Length - 1; i++)
        {
            //��ü�� ������ ����� ����,�����ʿ� �ִ��� �Ǻ�
            Vector3 firstPoint = pathObjects[i - 1];
            Vector3 secondPoint = pathObjects[i];
            Vector3 thirdPoint = pathObjects[i + 1];

            Vector3 forward = secondPoint - firstPoint;
            Vector3 toThirdPoint = thirdPoint - firstPoint;
            Vector3 cross = Vector3.Cross(forward, toThirdPoint);

            //��ΰ� ����̺� ��δ� ���ϱ� ����� ������ �ֱ����� ���� ª�� �׸��� ��¦ Ʋ���� ��ΰ� �־
            //�̰����� ȭ��ǥ�� �������� �ʱ����� ������ ����� ����
            Vector3 forward2 = secondPoint - firstPoint;
            Vector3 toThirdPoint2 = thirdPoint - secondPoint;

            float dot = Vector3.Dot(forward2.normalized, toThirdPoint2.normalized);
            float angle = Mathf.Acos(dot);
            float angleDegrees = angle * Mathf.Rad2Deg;

            //���ǹ�
            //������Ʈ ������ ��M�ĸ� ��Ÿ�������� ī�޶� ��ġ���� ����
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

            //������ ǥ�� ������Ʈ ����
            GameObject goalObject = Instantiate(goalPrefab,
                new Vector3(pathObjects[pathObjects.Length - 1].x, 1.6f, pathObjects[pathObjects.Length - 1].z)
                , Quaternion.identity);
            //�������� �׻� ����ڸ� �ٶ󺸰� �ϱ����� ī�޶���ġ ���� ����
            goalObject.GetComponent<PoiRotationSet>().SetPlayerTransform(playerTransform);
        }
    }

    //ARSession�� �������� �ʱ�ȭ �Լ�
    public void ArSessionDestroy()
    {
        //ARSession�� �������� �ʱ�ȭ
        arSession.Reset();
    }
}
