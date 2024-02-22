using System.Reflection;
using UnityEngine;

public class POIButton : MonoBehaviour
{
    public POIData poiData;
    private POIInfoPanelManager displayPOI;
    public GameObject selectedMarker;
    POIData sourceComponent;
    POIData selectedComponent;

    private void Start()
    {
        displayPOI = FindObjectOfType<POIInfoPanelManager>();

        sourceComponent = gameObject.GetComponent<POIData>();
        selectedMarker = GameObject.Find("selectedMarker");
        selectedComponent = selectedMarker.GetComponent<POIData>(); 
        
    }
    public void OnClick()
    {
        displayPOI.SetPanel(poiData.GetData());
        MarkerSelected();
    }
    void MarkerSelected()
    {
        // MyComponent의 모든 필드를 복사합니다.
        foreach (FieldInfo field in typeof(POIData).GetFields())
        {
            field.SetValue(selectedComponent, field.GetValue(sourceComponent));
        }
    }

    public void MoveSelectedMarker()
    {
        selectedMarker.transform.position = sourceComponent.transform.position;
    }
}