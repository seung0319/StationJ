using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POIInfoPanelManager : MonoBehaviour
{
    public static POIInfoPanelManager instance = null;
    public GameObject infoPanel;
    public Text nameText;
    public Text addressText;
    public Text descriptionText;

    //public Text imageText;

    public void SetPanel(POI poi)
    {
        nameText.text = poi.name;

        addressText.text = poi.address;
        descriptionText.text = poi.description;

        //imageText.text = poi.image;

        //typeText.text = poi.type;
        //latitudeText.text = poi.latitude.ToString();
        //longitudeText.text = poi.longitude.ToString();

        infoPanel.SetActive(true);
    }
}
