using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerMaker : MonoBehaviour
{
    public GameObject buttonPrefab; // Button Prefab�� Inspector���� �Ҵ����ּ���.

    public Sprite parkSprite;
    public Sprite parkingLotSprite;
    public Sprite subwaySprite;
    public Sprite toiletSprite;
    public Sprite hospitalSprite;
    public Sprite photozonSprite;
    public Sprite docentSprite;

    GameObject button;

    private void Start()
    {
        foreach (var poi in DataManager.instance.poiList.pois)
        {
            CreateButtonAtLocation(poi.latitude, poi.longitude, poi.type);
            POIData poiData = button.GetComponent<POIData>();
            poiData.SetData(poi);
            //Debug.Log(poi.name);
        }
    }
    public void CreateButtonAtLocation(double latitude, double longitude, string type)
    {
        // ���� ����, �浵
        double originLatitude = 37.713675f;
        double originLongitude = 126.743572f;
        // ����η°��߿� 37.713675f; 126.743572f;

        // ����, �浵�� ���� x, y�� ��ȭ ����
        double xRatio = 559092.4f;
        double yRatio = 714178.2f;

        // ����, �浵�� x, y�� ��ȯ
        double x = (longitude - originLongitude) * xRatio;
        double y = (latitude - originLatitude) * yRatio;

        GameObject publicParent = GameObject.Find("PublicMarkers");
        GameObject hospitalParent = GameObject.Find("HospitalMarkers");
        GameObject photoParent = GameObject.Find("PhotoMarkers");

        Vector2 position = new Vector2((float)x, (float)y); // Vector3 ����
        button = Instantiate(buttonPrefab, position, Quaternion.identity);

        if (type == "������" || type == "����Ʈ") // Map ���ӿ�����Ʈ�� �����ϴ� ��쿡�� ��ư ����
        {
            button.transform.SetParent(photoParent.transform, false);
            if (type == "������")
                button.GetComponent<Image>().sprite = photozonSprite;
            else if (type == "����Ʈ")
                button.GetComponent<Image>().sprite = docentSprite;
        }
        else if (type == "����")
        {
            button.transform.SetParent(hospitalParent.transform, false);
            button.GetComponent<Image>().sprite = hospitalSprite;
        }
        else
        {
            button.transform.SetParent(publicParent.transform, false);
            if (type == "����")
                button.GetComponent<Image>().sprite = parkSprite;
            else if (type == "����ö")
                button.GetComponent<Image>().sprite = subwaySprite;
            else if (type == "ȭ���")
                button.GetComponent<Image>().sprite = toiletSprite;
            else if (type == "������")
                button.GetComponent<Image>().sprite = parkingLotSprite;
        }
    }
}
