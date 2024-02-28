using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapPathCreator : MonoBehaviour
{
    public RectTransform canvasRect;
    private GameObject path;
    public GameObject pathPrefab;
    public Image linePrefab;
    public GameObject parentMap;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform lastPoint = null;
        foreach (var points in DataManager.instance.paths)
        {
            path = Instantiate(pathPrefab, DataManager.instance.MapRatioAR(points.latitude, points.longitude), Quaternion.identity);
            path.transform.SetParent(parentMap.transform, false);
            if (lastPoint != null)
            {
                // 새로운 Image 오브젝트를 생성
                Image lineImage = Instantiate(linePrefab, canvasRect);
                lineImage.transform.SetParent(parentMap.transform, false);
                // 새롭게 생성된 Image 오브젝트의 RectTransform을 설정
                Vector2 differenceVector = path.transform.position - lastPoint.position;
                lineImage.rectTransform.sizeDelta = new Vector2(differenceVector.magnitude, lineImage.rectTransform.sizeDelta.y);
                lineImage.rectTransform.pivot = new Vector2(0, 0.5f);
                lineImage.rectTransform.position = lastPoint.position;
                float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
                lineImage.rectTransform.rotation = Quaternion.Euler(0, 0, angle);
            }
            lastPoint = path.GetComponent<RectTransform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
