using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POIData : MonoBehaviour
{
    public POI poi;
    Button poiCouponButton;

    public void SetData(POI newPOI)
    {
        poi = newPOI;
    }

    public POI GetData()
    {
        return poi;
    }

    public void SetpoiCouponButton()
    {
        poiCouponButton = GetComponent<Button>();
        poiCouponButton.onClick.RemoveAllListeners();
        poiCouponButton.onClick.AddListener(CouponSet);
    }

    void CouponSet()
    {
        //ÄíÆù ÆË¾÷ ÃÊ±âÈ­
    }
}
