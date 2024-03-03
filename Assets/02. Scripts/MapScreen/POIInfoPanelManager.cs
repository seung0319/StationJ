using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// POIButton.cs 클래스에서 사용되는 상세설명창을 업데이트 시키는 함수가 포함된 클래스
/// POIButton.cs 가 들어가는 오브젝트가 프리팹이기에 사용한 방법
/// </summary>
public class POIInfoPanelManager : MonoBehaviour
{
    public GameObject infoPanel;
    public Image imageS;
    public Text nameText;
    public Text addressText;
    public Text descriptionText;
    public Text endPointText;
    

    // 함수가 호출되면 상세설명창이 선택된 POI의 데이터로 업데이트된다.
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
    }
}
