using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 마커의 버튼 컴포넌트에 들어가는 클래스
/// 목적지의 위도 경도를 초기화하거나
/// 상세설명창의 데이터를 업데이트 시키는 클래스
/// </summary>
public class POIButton : MonoBehaviour
{
    public POIData poiData;
    private POIInfoPanelManager displayPOI;
    public GameObject selectedMarker;
    POIData sourceComponent;
    public GameObject directionManager;

    public Text debugger;

    // 마커가 생성되면 마커에 도착 마커와 DirectionManager.cs 가 실행되는 directionManager 오브젝트를 초기화 시킨다.
    private void Start()
    {
        displayPOI = FindObjectOfType<POIInfoPanelManager>();

        sourceComponent = gameObject.GetComponent<POIData>();
        selectedMarker = GameObject.Find("Selected").transform.Find("selectedMarker").gameObject;
        directionManager = GameObject.Find("DirectionManagerP").transform.Find("DirectionManager").gameObject;
    }

    // 마커를 누르면 프로그램의 목적지가 설정되고, 상세설명창의 데이터를 업데이트하고, DirectionManager.cs를 활성화 시킨다.
    public void OnClick()
    {
        MarkerSelected();
        displayPOI.SetPanel(poiData.GetData());
        directionManager.SetActive(true);
    }
    void MarkerSelected()
    {
        DataManager.instance.selectedPoi = poiData.poi;
        DirectionManager.destLatitude = poiData.poi.latitude.ToString();
        DirectionManager.destLongitude = poiData.poi.longitude.ToString();
        selectedMarker.transform.position = sourceComponent.transform.position;
    }
}