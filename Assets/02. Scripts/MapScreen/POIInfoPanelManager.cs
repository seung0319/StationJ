using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// POIButton.cs Ŭ�������� ���Ǵ� �󼼼���â�� ������Ʈ ��Ű�� �Լ��� ���Ե� Ŭ����
/// POIButton.cs �� ���� ������Ʈ�� �������̱⿡ ����� ���
/// </summary>
public class POIInfoPanelManager : MonoBehaviour
{
    public GameObject infoPanel;
    public Image imageS;
    public Text nameText;
    public Text addressText;
    public Text descriptionText;
    public Text endPointText;
    

    // �Լ��� ȣ��Ǹ� �󼼼���â�� ���õ� POI�� �����ͷ� ������Ʈ�ȴ�.
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
