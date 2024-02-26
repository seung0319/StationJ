using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMaker : MonoBehaviour
{
    public GameObject photozonePrefab;
    public GameObject docentPrefab;

    public Transform photozoneParent;
    public Transform docentParent;

    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var poi in DataManager.instance.poiList.pois)
        {
            if (poi.type == "포토존")
            {
                parent = Instantiate(photozonePrefab);
                parent.transform.SetParent(photozoneParent);
                POIData poiData = parent.GetComponent<POIData>();
                poiData.SetData(poi);
                

            }

            else if (poi.type == "도슨트")
            {
                parent = Instantiate(docentPrefab);
                parent.transform.SetParent(docentParent);
                POIData poiData = parent.GetComponent<POIData>();
                poiData.SetData(poi);
                
            }
            else
            {
                continue;
            }
            // 인스턴스화된 프리팹 내부의 게임오브젝트 찾기
            Text nameText = parent.transform.Find("NameText").GetComponent<Text>();
            Text desText = parent.transform.Find("DescriptionText").GetComponent<Text>();
            // 찾은 게임오브젝트의 Text 컴포넌트 편집
            nameText.text = poi.name;
            desText.text = poi.description;

            string base64Image = poi.image;
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            Image pimage = parent.transform.Find("Image").GetComponent<Image>();
            pimage.GetComponent<Image>().sprite = sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
