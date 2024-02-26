using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class POIButton : MonoBehaviour
{
    public POIData poiData;
    private POIInfoPanelManager displayPOI;
    public GameObject selectedMarker;
    POIData sourceComponent;
    public GameObject directionManager;

    public Text debugger;

    private void Start()
    {
        displayPOI = FindObjectOfType<POIInfoPanelManager>();

        sourceComponent = gameObject.GetComponent<POIData>();
        selectedMarker = GameObject.Find("Selected").transform.Find("selectedMarker").gameObject;
        directionManager = GameObject.Find("DirectionManagerP").transform.Find("DirectionManager").gameObject;
    }
    public void OnClick()
    {
        displayPOI.SetPanel(poiData.GetData());
        MarkerSelected();
        directionManager.SetActive(true);
    }
    void MarkerSelected()
    {
        DataManager.instance.selectedPoi = poiData.poi;
        DirectionManager.destLatitude = poiData.poi.latitude.ToString();
        DirectionManager.destLongitude = poiData.poi.longitude.ToString();
        selectedMarker.transform.position = sourceComponent.transform.position;
        // POIData의 모든 필드를 복사합니다.
        //foreach (FieldInfo field in typeof(POIData).GetFields())
        //{
        //    field.SetValue(DataManager.instance.selectedPoi, field.GetValue(sourceComponent));
        //    selectedMarker.transform.position = sourceComponent.transform.position;
        //}
    }
}