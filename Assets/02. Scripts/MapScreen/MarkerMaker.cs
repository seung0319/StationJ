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
        }
    }
    public void CreateButtonAtLocation(double latitude, double longitude, string type)
    {
        GameObject publicParent = GameObject.Find("PublicMarkers");
        GameObject hospitalParent = GameObject.Find("HospitalMarkers");
        GameObject photoParent = GameObject.Find("PhotoMarkers");

        Vector2 position = DataManager.instance.MapRatio(latitude, longitude);
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
