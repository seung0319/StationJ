using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabButton : MonoBehaviour
{
    public POIData poiData;
    public InfoPanelManager panel;



    private void Start()
    {
        panel = FindObjectOfType<InfoPanelManager>();
        //button = GetComponent<Button>();
        //button.onClick.AddListener(TogglePanel);
    }

    public void OnClick()
    {
        panel.SetPanel(poiData.GetData());
        ButtonSelected();

    }
    void ButtonSelected()
    {
        DataManager.instance.selectedPoi = poiData.poi;
        DirectionManager.destLatitude = poiData.poi.latitude.ToString();
        DirectionManager.destLongitude = poiData.poi.longitude.ToString();
        PDListStateSaver.statesaverPOI = poiData.poi;
        if(poiData.poi.type == "µµ½¼Æ®")
            PDListStateSaver.docentOn = true;
    }
}
