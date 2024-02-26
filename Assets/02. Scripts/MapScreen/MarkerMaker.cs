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
    public GameObject buttonPrefab; // Button Prefab을 Inspector에서 할당해주세요.

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

        Vector3 position3D = new Vector3((float)position.x, (float)position.y, 0); // Vector3 생성
        button = Instantiate(buttonPrefab, position3D, Quaternion.identity);

        if (type == "포토존" || type == "도슨트") // Map 게임오브젝트가 존재하는 경우에만 버튼 생성
        {
            button.transform.SetParent(photoParent.transform, false);
            if (type == "포토존")
                button.GetComponent<Image>().sprite = photozonSprite;
            else if (type == "도슨트")
                button.GetComponent<Image>().sprite = docentSprite;
        }
        else if (type == "병원")
        {
            button.transform.SetParent(hospitalParent.transform, false);
            button.GetComponent<Image>().sprite = hospitalSprite;
        }
        else
        {
            button.transform.SetParent(publicParent.transform, false);
            if (type == "공원")
                button.GetComponent<Image>().sprite = parkSprite;
            else if (type == "지하철")
                button.GetComponent<Image>().sprite = subwaySprite;
            else if (type == "화장실")
                button.GetComponent<Image>().sprite = toiletSprite;
            else if (type == "주차장")
                button.GetComponent<Image>().sprite = parkingLotSprite;
        }
            
    }

    private Vector2D ConvertGeoToUnityCoordinate(double latitude, double longitude)
    {
        // 기준 위도, 경도
        double originLatitude = 37.713675f;
        double originLongitude = 126.743572f;
        // 경기인력개발원 37.713675f; 126.743572f;

        // 기준 x, y
        double originX = 0;
        double originY = 0;

        // 위도, 경도에 대한 x, y의 변화 비율
        double xRatio = 559092.4f;
        double yRatio = 714178.2f;
        //Debug.Log(xRatio + " " + yRatio);
        //559092.4 714178.2
        // 위도, 경도를 x, y로 변환
        double x = originX + (longitude - originLongitude) * xRatio;
        double y = originY + (latitude - originLatitude) * yRatio;


        return new Vector2D(x, y);
    }
}
