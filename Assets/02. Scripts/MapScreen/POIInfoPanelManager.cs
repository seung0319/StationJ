using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POIInfoPanelManager : MonoBehaviour
{
    public GameObject infoPanel;
    public Image imageS;
    public Text nameText;
    public Text addressText;
    public Text descriptionText;
    public Text startPointText;
    public Text endPointText;
    public Text durationText;
    public Text distanceText;

    public void SetPanel(POI poi)
    {
        nameText.text = poi.name;
        addressText.text = poi.address;
        descriptionText.text = poi.description;
        endPointText.text = poi.name;
        infoPanel.SetActive(true);

        string base64Image = poi.image;
        byte[] imageBytes = Convert.FromBase64String(base64Image);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        imageS.sprite = sprite;

        startPointText.text = "현재 위치"; // 위치 허용이면 현재 위치, 거부상태면 경기인력개발원
        durationText.text = Mathf.RoundToInt(DataManager.instance.duration / 6000).ToString() + " 분";
        distanceText.text = DataManager.instance.distance.ToString() + " m";
    }
}
