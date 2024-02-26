using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Vector2D
{
    public double x;
    public double y;

    public Vector2D(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
}

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
        Vector2D position = ConvertGeoToUnityCoordinate(latitude, longitude);
        GameObject publicParent = GameObject.Find("PublicMarkers");
        GameObject hospitalParent = GameObject.Find("HospitalMarkers");
        GameObject photoParent = GameObject.Find("PhotoMarkers");

        Vector3 position3D = new Vector3((float)position.x, (float)position.y, 0); // Vector3 ����
        button = Instantiate(buttonPrefab, position3D, Quaternion.identity);

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

    private Vector2D ConvertGeoToUnityCoordinate(double latitude, double longitude)
    {
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
        double x = originX + (longitude - originLongitude) * xRatio;
        double y = originY + (latitude - originLatitude) * yRatio;


        return new Vector2D(x, y);
    }
}
