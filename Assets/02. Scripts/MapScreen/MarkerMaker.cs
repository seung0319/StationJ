using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        // 기준 위도, 경도
        double originLatitude = 37.713675f;
        double originLongitude = 126.743572f;
        // 경기인력개발원 37.713675f; 126.743572f;

        // 위도, 경도에 대한 x, y의 변화 비율
        double xRatio = 559092.4f;
        double yRatio = 714178.2f;

        // 위도, 경도를 x, y로 변환
        double x = (longitude - originLongitude) * xRatio;
        double y = (latitude - originLatitude) * yRatio;

        GameObject publicParent = GameObject.Find("PublicMarkers");
        GameObject hospitalParent = GameObject.Find("HospitalMarkers");
        GameObject photoParent = GameObject.Find("PhotoMarkers");

        Vector2 position = new Vector2((float)x, (float)y); // Vector3 생성
        button = Instantiate(buttonPrefab, position, Quaternion.identity);

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
}
