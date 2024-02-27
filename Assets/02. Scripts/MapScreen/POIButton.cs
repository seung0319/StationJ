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