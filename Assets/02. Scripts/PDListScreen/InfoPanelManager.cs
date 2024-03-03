using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PDListScreen 씬의 POI 버튼들을 누르면 상세설명창을 업데이트 하는 클래스 
/// </summary>
public class InfoPanelManager : MonoBehaviour
{
    public GameObject photozoneInfoPanel;
    public GameObject docentInfoPanel;

    GameObject infoPanel;

    public GameObject photozoneImage;
    public GameObject photozoneName;
    public GameObject photozoneDes;
    
    public GameObject docentImage;
    public GameObject docentName;
    public GameObject docentDes;

    // 포토존일때 포토존의 상세설명창을,
    // 도슨트일때 도슨트의 상세설명창을 업데이트 하는 함수
    public void SetPanel(POI poi)
    {
        if(poi.type == "포토존")
        {
            string base64Image = poi.image;
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            photozoneImage.GetComponent<Image>().sprite = sprite;

            photozoneName.GetComponent<Text>().text = poi.name;
            photozoneDes.GetComponent<Text>().text = poi.description;

            infoPanel = photozoneInfoPanel;
        }

        else if(poi.type == "도슨트")
        {
            string base64Image = poi.image;
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            docentImage.GetComponent<Image>().sprite = sprite;

            docentName.GetComponent<Text>().text = poi.name;
            docentDes.GetComponent<Text>().text = poi.description;

            infoPanel = docentInfoPanel;
        }

        infoPanel.SetActive(!infoPanel.activeSelf);
    }
}
